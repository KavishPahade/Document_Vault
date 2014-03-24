/////////////////////////////////////////////////////////////////////
///  Client.cs - Provides prototype behavior for the client      ///
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
 *  This package defines two classes:
 *  Client:
 *    Provides prototype behavior for the client.
 *  EchoCommunicator:
 *    Handles the dequeing and handling of different responses from the server
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
 * ver 4.1 :Modified the approriate code to handle the various messages from the server 
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
using System.IO;

namespace DocumentVault
{
    
    class EchoCommunicator:AbstractCommunicator
    {
        protected override void ProcessMessages()
        {
            List<string> CategoryList=new List<string>();
            while (true)
            {
                ServiceMessage msg = bq.deQ();
                Console.Write("\n  {0} Recieved Message:\n", msg.TargetCommunicator);
                msg.ShowMessage();
                if (msg.ResourceName == "getcategories")
                {
                    string reply = msg.Contents;
                    string[] result = reply.Split(',');
                    foreach (string str in result)
                    {
                        CategoryList.Add(str);
                    }
                    MainWindow.winob.dispatch_categories(CategoryList);
                    if (Verbose)
                        Console.Write("Shutting down");
                    msg.TargetCommunicator = "dispatcher";
                    AbstractMessageDispatcher.GetInstance().PostMessage(msg);
                }
                if (msg.ResourceName == "Edit_XML")
                {
                    MainWindow.winob.dispatch_editXML(msg.Contents);
                    if (Verbose)
                        Console.Write("Shutting down");
                    msg.TargetCommunicator = "dispatcher";
                    AbstractMessageDispatcher.GetInstance().PostMessage(msg);
                }
                if (msg.ResourceName == "getcategoryfiles")
                {
                    string reply = msg.Contents;
                    string[] result = reply.Split(',');
                    foreach (string str in result)
                    {
                        CategoryList.Add(str);
                    }
                    MainWindow.winob.dispatch_categoryfiles(CategoryList);
                    if (Verbose)
                        Console.Write("Shutting down");
                    msg.TargetCommunicator = "dispatcher";
                    AbstractMessageDispatcher.GetInstance().PostMessage(msg);
                }
                if (msg.ResourceName == "search_text")
                {
                    List<string> FileList = new List<string>();
                    string reply = msg.Contents;
                    string[] result = reply.Split(',');
                    foreach (string str in result)
                    {
                        FileList.Add(str);
                    }
                    MainWindow.winob.dispatch_searchreults(FileList);
                    if (Verbose)
                        Console.Write("Shutting down");
                    msg.TargetCommunicator = "dispatcher";
                    AbstractMessageDispatcher.GetInstance().PostMessage(msg);
                }
                if (msg.ResourceName == "upload_failed")
                {
                    if (msg.Contents == "Files_Present")
                        MainWindow.winob.upload_failed("Child dependencies are present");
                    else
                        MainWindow.winob.upload_failed("Child Dependencies not present");

                        if (Verbose)
                        Console.Write("Shutting down");
                    msg.TargetCommunicator = "dispatcher";
                    AbstractMessageDispatcher.GetInstance().PostMessage(msg);
                }
                if (msg.ResourceName == "uploadfile")
                {
                    
                    string reply = msg.Contents;
                    string[] result = reply.Split(',');
                    foreach (string str in result)
                    {
                        CategoryList.Add(str);
                    }
                    MainWindow.winob.dispatch_categoryfiles(CategoryList);
                    if (Verbose)
                        Console.Write("Shutting down");
                    msg.TargetCommunicator = "dispatcher";
                    AbstractMessageDispatcher.GetInstance().PostMessage(msg);
                }
                if (msg.ResourceName == "update_infoview")
                {
                    List<string> ChildList = new List<string>();
                    List<string> ParentList = new List<string>();
                    string reply = msg.Contents;
                    string[] midresult = reply.Split(';');
                    string[] childs = midresult[0].Split(',');
                    string[] parents = midresult[1].Split(',');
                    foreach (string str in childs)
                    {
                        ChildList.Add(str);
                    }
                    foreach (string str in parents)
                    {
                        ParentList.Add(str);
                    }
                    MainWindow.winob.dispatch_parentchild(ChildList,ParentList);
                    if (Verbose)
                        Console.Write("Shutting down");
                    msg.TargetCommunicator = "dispatcher";
                    AbstractMessageDispatcher.GetInstance().PostMessage(msg);
                }
                if (msg.ResourceName.Substring(0,8)=="download")
                {
                    string contents = "";
                    string filePath = "..//..//..//server//downloads//";
                    string[] str = msg.ResourceName.Split(',');
                    string fileSpec = filePath + "//" + str[1];
                    FileStream fs = null;
                    try
                    {
                        if (File.Exists(fileSpec))
                        {
                            byte[] block = Encoding.ASCII.GetBytes(msg.Contents);
                            fs = File.Open(fileSpec, FileMode.Append, FileAccess.Write);
                            fs.Write(block, 0, block.Length);
                            fs.Flush();

                        }
                        else
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
                        
                    finally
                    {
                        
                        TextReader tr = File.OpenText("..//..//..//server//downloads//"+str[1]);   //uses textreader object to access the file contents
                        contents = tr.ReadToEnd();
                        tr.Close();
                    }
                    MainWindow.winob.dispatch_filecontents(contents,str[1]);
                    if (Verbose)
                        Console.Write("Shutting down");
                    msg.TargetCommunicator = "dispatcher";
                    AbstractMessageDispatcher.GetInstance().PostMessage(msg);
                }
            }
        }
    }
         class Client
        {
            string ServerUrl = "http://localhost:8000/CommService";
            Sender sender = new Sender();
            static string ClientUrl = "http://localhost:8001/CommService";
            Receiver receiver = new Receiver(ClientUrl);
             public void serverconnect()
             {
                 sender.Connect(ServerUrl);
                 sender.Start();
             }
             public void createXML(string filename,string category, string description, string childs, string keywords)
             {
                 
                ServiceMessage msg1 = 
                ServiceMessage.MakeMessage("upload", "ServiceClient", category + ";" + description + ";" + childs + ";" + keywords, "createXML" + "," + filename);
                msg1.SourceUrl = ClientUrl;
                 msg1.TargetUrl = ServerUrl;
                 //Console.Write("\n  Posting message to \"{0}\" component", msg1.TargetCommunicator);
                 sender.PostMessage(msg1);
             }
             public void editXML(string tags,string contents,string filename)
             {
                 ServiceMessage msg1 =
                ServiceMessage.MakeMessage("editXML", "ServiceClient", tags+";"+contents,"editXML" + "," + filename);
                 msg1.SourceUrl = ClientUrl;
                 msg1.TargetUrl = ServerUrl;
                 sender.PostMessage(msg1);
             }
             public void Search(string keywords, string category, string mode,string Searchtype)
             {
                 ServiceMessage msg1 =
                 ServiceMessage.MakeMessage("search", "ServiceClient", Searchtype + ";" + keywords + ";" + mode + ";" + category, "search_text");
                 msg1.SourceUrl = ClientUrl;
                 msg1.TargetUrl = ServerUrl;
                 //Console.Write("\n  Posting message to \"{0}\" component", msg1.TargetCommunicator);
                 sender.PostMessage(msg1);
             }
             public void checkdependencies(string child_files)
             {
                 ServiceMessage msg =
                 ServiceMessage.MakeMessage("upload", "ServiceClient", child_files, "dependencycheck");
                 msg.SourceUrl = ClientUrl;
                 msg.TargetUrl = ServerUrl;
                 //Console.Write("\n  Posting message to \"{0}\" component", msg1.TargetCommunicator);
                 sender.PostMessage(msg);
             }
             public bool uploadfile(string qualifiedfilename,string child_files)
             {
                 
               
                long blockSize = 512;
                try
                {
                    string filename = Path.GetFileName(qualifiedfilename);
                    //service.OpenFileForWrite(filename);
                    FileStream fs = File.Open(qualifiedfilename, FileMode.Open, FileAccess.Read);
                    int bytesRead = 0;
                    while (true)
                    {
                        long remainder = (int)(fs.Length - fs.Position);
                        if (remainder == 0)
                        {
                            break;
                        }
                        long size = Math.Min(blockSize, remainder);
                        byte[] block = new byte[size];
                        bytesRead = fs.Read(block, 0, block.Length);
                        //service.WriteFileBlock(block);
                        sender.Connect(ServerUrl);

                ServiceMessage msg1 =
                ServiceMessage.MakeMessage("upload", "ServiceClient", Encoding.ASCII.GetString(block), "uploadfile"+","+filename);
                msg1.SourceUrl = ClientUrl;
                msg1.TargetUrl = ServerUrl;
                //Console.Write("\n  Posting message to \"{0}\" component", msg1.TargetCommunicator);
                sender.PostMessage(msg1);
                    }
                    fs.Close();
                    
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write("\n  can't open {0} for writing - {1}", qualifiedfilename, ex.Message);
                    return false;
                }
                

                 
                 
             }
             public void get_file_contents(string contents, string resourcename)
             {
                ServiceMessage msg1 =
                ServiceMessage.MakeMessage("get_file_contents", "ServiceClient", contents, resourcename);
                 msg1.SourceUrl = ClientUrl;
                 msg1.TargetUrl = ServerUrl;
                 //Console.Write("\n  Posting message to \"{0}\" component", msg1.TargetCommunicator);
                 sender.PostMessage(msg1);
                 receiver.Close();
             }
              public void update_infoview(string contents, string resourcename)
            {
                ServiceMessage msg1 =
                ServiceMessage.MakeMessage("update_infoview", "ServiceClient", contents, resourcename);
                msg1.SourceUrl = ClientUrl;
                msg1.TargetUrl = ServerUrl;
                //Console.Write("\n  Posting message to \"{0}\" component", msg1.TargetCommunicator);
                sender.PostMessage(msg1);
                receiver.Close();
                Console.Write("\n\n");
            }
              public void category_click(string contents, string resourcename)
              {
                  EchoCommunicator echo = new EchoCommunicator();
                  echo.Name = "client-echo";
                  receiver.Register(echo);
                  echo.Start();
                  ServiceMessage msg1 =
                  ServiceMessage.MakeMessage("echo", "ServiceClient", contents, resourcename);
                  msg1.SourceUrl = ClientUrl;
                  msg1.TargetUrl = ServerUrl;
                  //Console.Write("\n  Posting message to \"{0}\" component", msg1.TargetCommunicator);
                  sender.PostMessage(msg1);
                  receiver.Close();
                  Console.Write("\n\n");
              }
             public void testmethod(string contents, string resourcename)
            {          
                EchoCommunicator echo = new EchoCommunicator();
                echo.Name = "client-echo";
                receiver.Register(echo);
                echo.Start();
                //sender.Connect(ServerUrl);
                
                ServiceMessage msg1 =
                ServiceMessage.MakeMessage("echo", "ServiceClient", contents, resourcename);
                msg1.SourceUrl = ClientUrl;
                msg1.TargetUrl = ServerUrl;
                //Console.Write("\n  Posting message to \"{0}\" component", msg1.TargetCommunicator);
                sender.PostMessage(msg1);
                receiver.Close();
                Console.Write("\n\n");
            }
        }
    }

   
