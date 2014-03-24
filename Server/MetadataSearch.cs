/////////////////////////////////////////////////////////////////////
///  Metadata_Search.cs - Creation of new metadata file         ///
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
 * Metadata Search class searches the contents of various tags and returns the associiated contents. 
 * It accepts the comma separated tags and its corresponding values
 
 * Public Interface
 * =================
 * public List<string> XMLSearch(List<string>filepattern,string path, List<string> tags,bool recursive_flag,string categories)
 * Input:Filename to be created,categories,tags and switches to be added 
 * Output:Corresponding tag content pairs
 * 
 * Build Process
 * =============
 * Required Files:
 *  
 * Maintenance History
 * ===================
 * ver 1.0 : Nov 25,2013
 *         - Added the  metadata class
 *         - added the functionally working linq queries
 * 
 * */
//#define TESTING_METADATA_SEARCH
#undef TESTING_Metadata_SEARCH
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
{    //--------<This module is responsible for traversing through the xml file and return the given tags contents
    class MetadataSearch
    {
       public List<string> XMLSearch(List<string>filepattern,string path, List<string> tags,bool recursive_flag,string categories)
        {
            List<string> files = new List<string>(), results=new List<string>();
            results.Add("");
            string[] multiplecategory = categories.Split(',');
            XDocument doc = XDocument.Load(@"..\..\Category_Map.xml");
            foreach (string singlecategory in multiplecategory)
            {
                IEnumerable<XElement> filenames = doc.Descendants("category")
                .Where(s => s.FirstAttribute.Value.Equals(singlecategory)).SelectMany(s => s.Elements("filename"));                   
                foreach (var str in filenames)
                {  files.Add(str.Value.ToString());   }
            }
          foreach (string file in files)
          {
              string dir = Path.GetDirectoryName(@"..\..\DocumentVault\"+file),filename = Path.GetFileNameWithoutExtension(file) + ".xml";
              filename = Path.Combine(dir, filename);
              XmlDocument doc1 = new XmlDocument();
              doc1.Load(filename);
              foreach (string tag in tags)
              {
                    XmlElement root = doc1.DocumentElement;
                    XmlNodeList lst = root.GetElementsByTagName(tag);
                    foreach (XmlNode n in lst)
                    {   results.Add("Filename:"+Path.GetFileName(filename)+","+"Tag:"+tag+",Contents:"+n.InnerText);
                        results.Add(" ");  }
              }             
            }
            return results;
          }//function ends       
        
        //test stub function for testing the individual class
#if TESTING_METADATA_SEARCH

        public static void Main(string[] args)
        {
            List<string> tags = new List<string>();
            tags.Add("title");
            string path = "../../testfiles";
            MetadataSearch MS = new MetadataSearch();
            Console.Write("\nI m testing Metadata Search");
            MS.XMLSearch(path, tags,true);
            //Console.Write("n  path = {0}\n  file pattern = {1}\n query string = {2}", path, pattern, queryString);
        }

#endif
    }
}
