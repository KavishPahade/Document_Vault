/////////////////////////////////////////////////////////////////////
///  Metadata_Creation.cs - Creation of new metadata file         ///
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
 * Metadata Creation class adds the contents of various tags and creates a new XML file. 
 * It accepts the comma separated tags and its corresponding values
 
 * Public Interface
 * =================
 * public void createXML(string qualifiedfilename, string[] categories, string description, string[] childs, string[] keywords)
 * Input:Filename to be created,categories,description child dependencies and keywords to be added 
 * Output:Newly generated XML file
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
namespace DocumentVault
{
    class Metadata_Creation
    {
        public void createXML(string qualifiedfilename, string[] categories, string description, string[] childs, string[] keywords)
        {
            string filename = Path.GetFileName(qualifiedfilename);
            string xmlFile = @"..\..\DocumentVault\" + Path.GetFileNameWithoutExtension(filename) + ".xml";
            XmlTextWriter textWrite = null;
            try
            {
                textWrite = new XmlTextWriter(xmlFile, null);
                textWrite.Formatting = Formatting.Indented;
                textWrite.WriteStartDocument();
                textWrite.WriteStartElement(Path.GetFileNameWithoutExtension(xmlFile));
                textWrite.WriteStartElement("FileInfo");
                textWrite.WriteElementString("Name", filename);
                textWrite.WriteElementString("Version", "Version-1.1");
                textWrite.WriteElementString("FileSize", xmlFile.Length.ToString());
                textWrite.WriteEndElement();
                textWrite.WriteStartElement("Description");
                textWrite.WriteString(description);
                textWrite.WriteEndElement();
                textWrite.WriteStartElement("Categories");
                foreach (string cat in categories)
                {   textWrite.WriteElementString("Category", cat);  }
                textWrite.WriteEndElement();

                textWrite.WriteStartElement("Dependencies");
                foreach (string dep in childs)
                {   textWrite.WriteElementString("Dependency", dep); }
                textWrite.WriteEndElement();

                textWrite.WriteStartElement("Keywords");
                foreach (string key in keywords)
                {  textWrite.WriteElementString("Keyword", key);  }
                textWrite.WriteEndElement();
                textWrite.WriteEndDocument();
                textWrite.Flush();
                textWrite.Close();
            }
            catch (XmlException xmlExp)
            {
                Console.WriteLine(xmlExp.Message);

            }
        }
            
    }
}
