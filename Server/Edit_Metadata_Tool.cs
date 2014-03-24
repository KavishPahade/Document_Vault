/////////////////////////////////////////////////////////////////////
///  Edit_Metadata_Tool.cs - Editing the existing metadata file  ///
///  ver 1.0                                                      ///
///  Language:      C#                                            ///
///  Platform:      Windows 8                                     ///
///  Application:   Remote Document Vault                         ///
///  Author:       Kavish Pahade,CSE 681  Syracuse University      ///
///                                                               ///
/////////////////////////////////////////////////////////////////////
/*
 * Module Operations
 * =================
 * Edit_Metadata class adds the new tags and contents required by the users int he existing the metadata files. 
 * It accepts the comma separated tags and its corresponding values
 
 * Public Interface
 * =================
 * public bool Edit_Metadata(Dictionary<string, string> Tag_Content_Dictionary, string filename)
 * Input:Dictionary containing the tags and its contents, filename to be edited 
 * Output:Updated metadata file
 * Returns:If edit is succesful returns true otherwise false 
 * 
 * Build Process
 * =============
 * Required Files:
 *  
 * Maintenance History
 * ===================
 * ver 1.0 : Nov 25,2013
 *         - Added the edit metadata class
 *         - added the functionally working linq queries
 * 
 * */
//#define TESTING_EDIT_METADATA_TOOL
#undef TESTING_EDIT_METADATA_TOOL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Xml.Linq;
namespace DocumentVault
{
    class Edit_Metadata_Tool
    {
        public bool Edit_Metadata(Dictionary<string, string> Tag_Content_Dictionary, string filename)
        {
            filename = Path.GetFileNameWithoutExtension(filename);
            try
            {
             XDocument doc = XDocument.Load(@"..\..\DocumentVault\"+filename+".xml");
            Console.WriteLine(doc.ToString());
            foreach (var str in Tag_Content_Dictionary)
            {
                if (str.Key == "Dependency")
                {
                    XElement xelement = new XElement(str.Key, str.Value);
                    doc.Elements(filename).Elements("Dependencies").FirstOrDefault().Add(xelement);
                }
                if (str.Key == "Keyword")
                {
                    XElement xelement = new XElement(str.Key, str.Value);
                    doc.Elements(filename).Elements("Keywords").FirstOrDefault().Add(xelement);
                }
                if (str.Key == "Category")
                {
                    XElement xelement = new XElement(str.Key, str.Value);
                    doc.Elements(filename).Elements("Categories").FirstOrDefault().Add(xelement);
                }
                if (str.Key == "Description")
                {
                    doc.Element(filename).Element("Description").Value=str.Value;
                }
                if (str.Key != "Dependency" && str.Key != "Category" && str.Key != "Keyword" && str.Key != "Description")
                    return false;

            }
           doc.Save(@"..\..\DocumentVault\" + filename + ".xml");
           return true;
         }
            catch
            {
                Console.Write("Error Occured whie loading the XML File");
                return false;
            }

        }
#if TESTING_EDIT_METADATA_TOOL

        public static void Main(string[] args)
        {
            Dictionary<string, List<string>> child_dictionary, Dictionary<string, List<string>> parent_dictionary;
            child_dictionary.Add(File1.txt,File2.txt);
            parent_dictionary.Add(File1.txt,File2.txt);
            string filename="file1.txt";
            ParentChildMap PCB = new ParentChildMap();
            Console.Write("\nI m testing Metadata Search");
            PCB.UpdateMap(child_dictionary,parent_dictionary,filename)
            //Console.Write("n  path = {0}\n  file pattern = {1}\n query string = {2}", path, pattern, queryString);
        }

#endif
    }
}