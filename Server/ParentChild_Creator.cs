/////////////////////////////////////////////////////////////////////
///  ParentChild_Creator.cs - Updation of the child parent dependencies///
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
 * ParentChild_Creator class updates two dictionaries
 * child dictionary which contains the filenames and its asscoaited child dependencies. 
 * parent dictionary which contains the filenames and its asscoaited parent dependencies. 
 * Goes thorugh all the metadata files in the vault and checks the depencdency tag for getting the parent and chil dependencies 
 * It accepts the comma separated tags and its corresponding values
 
 * Public Interface
 * =================
 * public void UpdateMap(Dictionary<string, List<string>> child_dictionary, Dictionary<string, List<string>> parent_dictionary, string filename)
 * Input: filename for which the dictionary needs to be updated
 * Output: Updated dictionaries of the parent and child dependencies 
 * Build Process
 * =============
 * Required Files:
 *  
 * Maintenance History
 * ===================
 * ver 1.0 : Nov 25,2013
 *         - Added the  parentchild_creator class
 *         - added the functionally working linq queries
 * 
 * */
//#define TESTING_ParentChild_Creator
#undef TESTING_ParentChild_Creator
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
    class ParentChildMap
    {
        public void UpdateMap(Dictionary<string, List<string>> child_dictionary, Dictionary<string, List<string>> parent_dictionary, string filename)
        {
            List<string> child_files = new List<string>();
            List<string> parent_files = new List<string>();
            string child_filename = null;
            string parent_filename = null;
            XDocument doc = XDocument.Load(@"..\DocumentVault\"+filename);
            Console.WriteLine(doc.ToString());
            var q = from x in doc.Elements(Path.GetFileNameWithoutExtension(filename)).Elements("Dependencies").Descendants() select x;
            var name = from x in doc.Elements(Path.GetFileNameWithoutExtension(filename)).Descendants() where (x.Name == "Name") select x;
                        //==============Creating Child Map=============================//
            foreach (var file in name)
            {   
                child_filename = file.Value;
            }
            foreach (var child in q)
            {
                //Console.WriteLine("{0}", child.Value);
                parent_filename = child.Value;
                child_files.Add(parent_filename);
            }
            child_dictionary.Add(child_filename, child_files);
           Console.WriteLine("===================Creating Parent Map===================");
            foreach (var parent in name)
            {
                parent_files.Add(parent.Value);
                foreach (var file in q)
                {
                    List<string> temp_list = new List<string>();
                    parent_filename = file.Value;
                    temp_list.Add(parent.Value);
                    if (!parent_dictionary.ContainsKey(parent_filename))
                        parent_dictionary.Add(parent_filename, parent_files);
                    else
                        parent_dictionary[parent_filename] = parent_dictionary[parent_filename].Concat(temp_list).ToList();
                }
            }
        }
#if TESTING_ParentChild_Creator

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
