/////////////////////////////////////////////////////////////////////
///  CategoryMap.cs - Updates the category 
///                   map whenever a new file enters the vault       ///
///  ver 2.0                                                      ///
///  Language:      C#                                            ///
///  Platform:      Windows 8                                     ///
///  Application:   Remote Document Vault                         ///
///  Author:       Kavish Pahade,CSE 681  Syracuse University      ///
///                                                               ///
/////////////////////////////////////////////////////////////////////
/*
 * Module Operations
 * =================
 * Category class updates the category map xml whenever a new file is uploaded in the vault. 
 * It searches the keywords based on the swithces like /A /O /R, it scans the input folder,
 * It reads the files from in the vault and returns those files which has the matching keywords
 
 
 * Public Interface
 * =================
 * public void Update_Category_Map(string []selected_category,string qualifiedfilename)
 * Input:Categories that needs to be updated,file name  to be added, User does not have authority to add new categories
 * Output:The updated category map with the added new file
 * 
 * Build Process
 * =============
 * Required Files:
 *   CommService.cs, Receiver.cs, Sender.cs
 *  
 * Maintenance History
 * ===================
 * ver 1.0 : Oct 4 2013
 *         - Created Class Category
 * */
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
    class CategoryMap
    {
        public void Update_Category_Map(string []selected_category,string qualifiedfilename)
        {
          
            string filename = Path.GetFileName(qualifiedfilename);
            XDocument doc = XDocument.Load(@"..\..\Category_Map.xml");

            Console.WriteLine(doc.ToString());
         
              var query = from x in doc.Descendants("category")
                        where (string)x.Attribute("name").Value == selected_category[0]
                        select x;
            foreach (var position in query)
            {
                position.Add(new XElement("filename", filename));  //adding the new file in the corresponding categories
            }
            doc.Save(@"..\..\Category_Map.xml");
        }
    }
}
