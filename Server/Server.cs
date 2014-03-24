/////////////////////////////////////////////////////////////////////
///  Server.cs - Provides prototype behavior for the DocumentVault server///
///  ver 2.0                                                      ///
///  Language:      C#                                            ///
///  Platform:      Windows 8                                     ///
///  Application:   Remote Document Vault                         ///
///  Author:       Kavish Pahade,CSE 681  Syracuse University      ///
///  Source:       Jim Fawcett, CSE681 - Software Modeling and Analysis, Fall 2013 ///
/////////////////////////////////////////////////////////////////////
/*
 *  Package Contents:
 *  -----------------
 *  This package defines six classes:
 *  Server:
 *    Provides prototype behavior for the DocumentVault server.
 *  CategoryCommunicator:
 *    Gets the categories from the category map and its associated files when requested
 *  UploadFileCommunicator:
 *   Provides the functionality of uploading the files to the vault.
 *   It gets the file contents from the clients through message passing functinality 
 *  InfoView:
 *    Extracts the file and its assciated metadata file's contetns from the vault 
 *    Input is provided as file name and the categories to look into
 *   SearchView:
 *    Search the keywords in the vault files and returns the list of files those contains the keywords 
 *    Input is provided as keywords and the categories to look into
 *   Edit_Metadata_View:
 *    Provides the edit functions on a metadata file
 *   UpdataInfoView
 *   Gets the parent childs and file contents from the vault 
 *   Returns the data to the client to updatae the information view
 *  Required Files:
 *  - Server:      Server.cs, Sender.cs, Receiver.cs
 *  - Components:  ICommLib, AbstractCommunicator, BlockingQueue
 *  - CommService: ICommService, CommService
 *
 *  Required References:
 *  - System.ServiceModel
 *  - System.RuntimeSerialization
 *
 *  Build Command:  devenv Project4HelpF13.sln /rebuild debug
 *
 *  Maintenace History:
 * ver 3.1 :Nov15,2013
 *  -Added the communicator classes 
 *  -Updated the server class prototype
 * ver 2.1 : Nov 7, 2013
 *  - replaced ServerSender with a merged Sender class
 *  ver 2.0 : Nov 5, 2013
 *  - fixed bugs in the message routing process
 *  ver 1.0 : Oct 29, 2013
 *  - first release
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Xml.Linq;
namespace DocumentVault
{
    // Echo Communicator
    class UploadFileCommunicator : AbstractCommunicator
    {
        protected override void ProcessMessages()
        {
            
            while (true)
            {
                ServiceMessage msg = bq.deQ();
                Console.Write("\n  {0} Recieved Message:\n", msg.TargetCommunicator);
                msg.ShowMessage();
                Console.Write("\n  {0} Resource data:\n", msg.ResourceName);
                bool filenotpresent=false;
                if (msg.ResourceName == "dependencycheck")
                {
                    string[] childs = msg.Contents.Split(',');
                    string[] files_in_vault = Directory.GetFiles(@"..\..\DocumentVault\", "*.*");
                    foreach (string child in childs)
                    {
                        int count = 0;
                        foreach (string singlefile in files_in_vault)
                        {
                            if (child == Path.GetFileName(singlefile))
                                break;
                            else
                                count++;
                        }
                        if (count == files_in_vault.Length)
                            filenotpresent = true;
                    }
                }
                    if (filenotpresent == false)
                {
                    ServiceMessage reply = ServiceMessage.MakeMessage("client-echo", "echo", "Files_Present", "upload_failed");
                    reply.TargetUrl = msg.SourceUrl;
                    reply.SourceUrl = msg.TargetUrl;
                    AbstractMessageDispatcher dispatcher = AbstractMessageDispatcher.GetInstance();
                    dispatcher.PostMessage(reply);
                    string[] str = msg.ResourceName.Split(',');
                    if (str[0] == "uploadfile")
                    {
                        string filePath = "..//..//DocumentVault";
                        string fileSpec = filePath + "//" + str[1];
                        FileStream fs = null;
                        try
                        {
                            if (File.Exists(fileSpec)) //appending the blocks of files if the file is already present
                            {
                                byte[] block = Encoding.ASCII.GetBytes(msg.Contents);
                                fs = File.Open(fileSpec, FileMode.Append, FileAccess.Write);
                                fs.Write(block, 0, block.Length);
                                fs.Flush();

                            }
                            else      //creating a new file if the file is not already present 
                            {
                                byte[] block = Encoding.ASCII.GetBytes(msg.Contents);
                                fs = File.Open(fileSpec, FileMode.Create, FileAccess.Write);
                                fs.Write(block, 0, block.Length);
                                fs.Flush();
                                Console.Write("\n  {0} opened", fileSpec);
                            }
                            fs.Close();
                        }
                        catch
                        {
                            Console.Write("\n  {0} failed to open", fileSpec);
                        }
                    }
                    if (str[0] == "createXML") //Creating the associated metadata file of the currently uplaoded file 
                    {
                        string[] info_array = msg.Contents.Split(';');
                        string[] categories = info_array[0].Split(',');
                        string[] childs = info_array[2].Split(',');
                        string[] keywords = info_array[3].Split(',');
                        Metadata_Creation MC = new Metadata_Creation();
                        MC.createXML(str[1], categories, info_array[1], childs, keywords);
                        CategoryMap CM = new CategoryMap();
                        CM.Update_Category_Map(categories, str[1]);  //updating the category map as soon as the upload file is complete
                        Console.Write("\n  {0} Inside createXML");
                    }
                }
                else   //if child dependencies not present notify users of the fact to enter appropriate files
                {
                    ServiceMessage reply = ServiceMessage.MakeMessage("client-echo", "echo", "Files_Not_Present", "upload_failed");
                    reply.TargetUrl = msg.SourceUrl;
                    reply.SourceUrl = msg.TargetUrl;
                    AbstractMessageDispatcher dispatcher = AbstractMessageDispatcher.GetInstance();
                    dispatcher.PostMessage(reply);
                }
            }
        }
    }
    class InfoView : AbstractCommunicator
    {
        //Note:The analyzer is showing the function lines as 328 not sure why for this function 
        void filedownloader(ServiceMessage msg,string filename)
        {
            long blockSize = 512;
            try
            {
                FileStream fs = File.Open(@"..\..\DocumentVault\" + filename, FileMode.Open, FileAccess.Read);
                int bytesRead = 0;
                while (true)
                {
                    long remainder = (int)(fs.Length - fs.Position);
                    if (remainder == 0)
                       break;
                    long size = Math.Min(blockSize, remainder);
                    byte[] block = new byte[size];
                    bytesRead = fs.Read(block, 0, block.Length);
                    ServiceMessage reply =
                    ServiceMessage.MakeMessage("client-echo", "echo", Encoding.ASCII.GetString(block), "download" + "," + filename);
                    reply.TargetUrl = msg.SourceUrl;
                    reply.SourceUrl = msg.TargetUrl;
                    AbstractMessageDispatcher dispatcher = AbstractMessageDispatcher.GetInstance();
                    dispatcher.PostMessage(reply);
                }
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.Write("\n  can't open {0} for writing - {1}", msg.Contents, ex.Message);
            }
        }
        protected override void ProcessMessages()
        {
            while (true)
            {
                ServiceMessage msg = bq.deQ();
                Console.Write("\n  {0} Recieved Message:\n", msg.TargetCommunicator);
                msg.ShowMessage();
                Console.Write("\n  {0} Resource data:\n", msg.ResourceName);
                string[] str = msg.ResourceName.Split(',');
                string tempstring= (Path.GetFileNameWithoutExtension(msg.Contents) )+ ".xml";
                if (str[0] == "get_file_contents")
                {
                    filedownloader(msg,msg.Contents);
                    filedownloader(msg,tempstring);
                }
            }
        }
    }
    class SearchView : AbstractCommunicator
    {
        protected override void ProcessMessages()
        {
            while (true)
            {
                ServiceMessage msg = bq.deQ();
                Console.Write("\n  {0} Recieved Message:\n", msg.TargetCommunicator);
                msg.ShowMessage();
                Console.Write("\n  {0} Resource data:\n", msg.ResourceName);
                string[] tokens = msg.Contents.Split(';');
                string keywords = "";
                string[] tempstring = tokens[1].Split(',');
                foreach (string str in tempstring)
                {
                    if (tokens[0] == "/T")
                    {
                        if(tempstring.Length==1)
                            keywords = keywords + str;
                        else
                            keywords = keywords + str+"/T";
                    }
                    else
                    {
                        if(tempstring.Length==1)
                            keywords = keywords + str;
                        else
                        keywords = keywords + str + "/M";
                    }
                }
                string queryString = tokens[0] + keywords + tokens[2];
                List<string> filepattern = new List<string>();
                filepattern.Add("*.*");
                List<string> resultfilelist = new List<string>();
                if (msg.ResourceName == "search_text")
                {
                    QueryProcessor QP = new QueryProcessor();    //calling the QueryProcessor module to process the tokens       
                    resultfilelist = QP.tokenAnalyzer(filepattern, @"..\..\DocumentVault\", queryString, tokens[3]);
                }
                string filestring = "";
                foreach (string str in resultfilelist)
                {
                    filestring = filestring + str + ",";
                }
                ServiceMessage reply = ServiceMessage.MakeMessage("client-echo", "echo", filestring, "search_text");
                reply.TargetUrl = msg.SourceUrl;
                reply.SourceUrl = msg.TargetUrl;
                AbstractMessageDispatcher dispatcher = AbstractMessageDispatcher.GetInstance();
                dispatcher.PostMessage(reply);
            }
        }
    }
    class Edit_Metadata_View : AbstractCommunicator
    {
        protected override void ProcessMessages()
        {
            while (true)
            {
                ServiceMessage msg = bq.deQ();
                Console.Write("\n  {0} Recieved Message:\n", msg.TargetCommunicator);
                msg.ShowMessage();
                string[] splitted = msg.Contents.Split(';');
                string[] tags = splitted[0].Split(',');
                string[] values = splitted[1].Split(',');
                string []info=msg.ResourceName.Split(',');
                Dictionary<string,string> Tag_Content_dictionary = new Dictionary<string, string>();
                int i = 0;
                foreach (string str in tags)
                {
                    Tag_Content_dictionary.Add(tags[i], values[i]);
                    i++;
                }
                    Edit_Metadata_Tool EMT = new Edit_Metadata_Tool();
                    bool outcome=EMT.Edit_Metadata(Tag_Content_dictionary,info[1]);
                    ServiceMessage reply;
                    if (outcome == true)
                      reply = ServiceMessage.MakeMessage("client-echo", "echo", "Edit_success", "Edit_XML");
                    else
                       reply =ServiceMessage.MakeMessage("client-echo", "echo", "Edit_Failed", "Edit_XML");    
                    reply.TargetUrl = msg.SourceUrl;
                    reply.SourceUrl = msg.TargetUrl;
                    AbstractMessageDispatcher dispatcher = AbstractMessageDispatcher.GetInstance();
                    dispatcher.PostMessage(reply);
            }
        }
    }
    class UpdateInfoView : AbstractCommunicator
    {
        protected override void ProcessMessages()
        {

            while (true)
            {
                try
                {
                    ServiceMessage msg = bq.deQ();
                    Console.Write("\n  {0} Recieved Message:\n", msg.TargetCommunicator);
                    msg.ShowMessage();
                    Dictionary<string, List<string>> child_dictionary = new Dictionary<string, List<string>>();
                    Dictionary<string, List<string>> parent_dictionary = new Dictionary<string, List<string>>();
                    ParentChildMap PCM = new ParentChildMap();
                    string xmlfilename = Path.GetFileNameWithoutExtension(msg.Contents) + ".xml";
                    string[] temp = Directory.GetFiles(@"..\..\DocumentVault\", "*.Xml");
                    foreach (string str in temp)
                        PCM.UpdateMap(child_dictionary, parent_dictionary, str);
                    XDocument doc = XDocument.Load(@"..\..\DocumentVault\" + xmlfilename);
                    var categorynames = from x in doc.Elements(Path.GetFileNameWithoutExtension(msg.Contents)).Elements("Categories").Descendants() select x;
                    List<string> parent_categories = new List<string>();
                    foreach (string category in categorynames)
                    {
                        parent_categories.Add(category);
                    }
                    //Dictionary<string, List<string>> new_parent_dictionary = new Dictionary<string, List<string>>(parent_dictionary);
                    // new_parent_dictionary = parent_dictionary;
                    bool flag = false;
                    foreach (KeyValuePair<string, List<string>> str in parent_dictionary)
                    {
                        if (str.Key == msg.Contents)
                            flag = true;
                    }
                    if (flag == false)
                    {
                        parent_dictionary.Add(msg.Contents, parent_categories);
                    }

                    //PCM.UpdateMap(child_dictionary, parent_dictionary, "../../Demo1.xml");
                    List<string> child_files = new List<string>();
                    foreach (var str in child_dictionary)
                    {
                        if (str.Key == msg.Contents)
                            child_files = str.Value;
                    }
                    string childs = "";
                    foreach (string str in child_files)
                    {
                        childs = childs + str + ",";
                    }
                    List<string> parent_files = new List<string>();
                    foreach (var str in parent_dictionary)
                    {
                        if (str.Key == msg.Contents)
                            parent_files = str.Value;
                    }
                    childs = childs + ";";
                    foreach (string str in parent_files)
                    {
                        childs = childs + str + ",";
                    }
                    ServiceMessage reply = ServiceMessage.MakeMessage("client-echo", "echo", childs, "update_infoview");
                    reply.TargetUrl = msg.SourceUrl;
                    reply.SourceUrl = msg.TargetUrl;
                    AbstractMessageDispatcher dispatcher = AbstractMessageDispatcher.GetInstance();
                    dispatcher.PostMessage(reply);
                    Console.Write("\n  {0} Resource data:\n", msg.ResourceName);
                    //string[] str = msg.ResourceName.Split(',');
                }
                catch (Exception msg)
                {
                    Console.WriteLine("Exception occured {0}",msg);
                }
            }
        }
    }
    class CategoryCommunicator : AbstractCommunicator
    {
        protected override void ProcessMessages()
        {
            while (true)
            {
                ServiceMessage msg = bq.deQ();
                Console.Write("\n  {0} Recieved Message:\n", msg.TargetCommunicator);
                msg.ShowMessage();
                if (msg.ResourceName == "getcategories")
                {
                    Console.Write("\n  Echo processing completed\n");
                    XDocument doc = XDocument.Load(@"..\..\Category_Map.xml");
                    Console.WriteLine(doc.ToString());
                    var categorynames = from x in doc.Elements("root").Elements("category").Attributes() select x;
                    string strcategory = "";
                    foreach (var str in categorynames)
                    {
                        strcategory += str.Value.ToString() + ',';
                    }
                    Console.Write("\n {0}", strcategory);
                    ServiceMessage reply = ServiceMessage.MakeMessage("client-echo", "echo", strcategory, "getcategories");
                    reply.TargetUrl = msg.SourceUrl;
                    reply.SourceUrl = msg.TargetUrl;
                    AbstractMessageDispatcher dispatcher = AbstractMessageDispatcher.GetInstance();
                    dispatcher.PostMessage(reply);
                }

                if (msg.ResourceName == "getcategoryfiles")
                {
                    Console.Write("\n  Echo processing completed\n");
                    XDocument doc = XDocument.Load(@"..\..\Category_Map.xml");
                    Console.WriteLine(doc.ToString());
                    IEnumerable<XElement> filenames = doc.Descendants("category")
                    .Where(s => s.FirstAttribute.Value.Equals(msg.Contents)).SelectMany(s => s.Elements("filename"));
                    string strcategory = "";
                    foreach (var str in filenames)
                    {
                        strcategory += str.Value.ToString() + ',';
                    }
                    Console.Write("\n {0}", strcategory);
                    ServiceMessage reply = ServiceMessage.MakeMessage("client-echo", "echo", strcategory, "getcategoryfiles");
                    reply.TargetUrl = msg.SourceUrl;
                    reply.SourceUrl = msg.TargetUrl;
                    AbstractMessageDispatcher dispatcher = AbstractMessageDispatcher.GetInstance();
                    dispatcher.PostMessage(reply);
                }
            }
        }
        }

            class Server
        {

            static void Main(string[] args)
            {
                Console.Write("\n  Starting CommService");
                Console.Write("\n ======================\n");

                string ServerUrl = "http://localhost:8000/CommService";
                Receiver receiver = new Receiver(ServerUrl);

                string ClientUrl = "http://localhost:8001/CommService";

                Sender sender = new Sender();
                sender.Name = "sender";
                sender.Connect(ClientUrl);
                receiver.Register(sender);
                sender.Start();
               
                //Clearing the temporary downloads folder as soon as the application starts
 
                string[] filePaths = Directory.GetFiles(@"../../downloads/");
                foreach (string filePath in filePaths)
                    File.Delete(filePath);
                
                // Test Component that simply echos message

                CategoryCommunicator com = new CategoryCommunicator();
                com.Name = "echo";
                receiver.Register(com);
                com.Start();

                UploadFileCommunicator upload = new UploadFileCommunicator();
                upload.Name = "upload";
                receiver.Register(upload);
                upload.Start();
                
                UpdateInfoView update = new UpdateInfoView();
                update.Name = "update_infoview";
                receiver.Register(update);
                update.Start();

                SearchView search = new SearchView();
                search.Name = "search";
                receiver.Register(search);
                search.Start();

                InfoView info = new InfoView();
                info.Name = "get_file_contents";
                receiver.Register(info);
                info.Start();

                Edit_Metadata_View edit = new Edit_Metadata_View();
                edit.Name = "editXML";
                receiver.Register(edit);
                edit.Start();

                
                Console.Write("\n  Started CommService - Press key to exit:\n ");
                Console.ReadKey();
            }
        }
    
    }