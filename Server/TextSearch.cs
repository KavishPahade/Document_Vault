/////////////////////////////////////////////////////////////////////
///  TextSearch.cs - Search the keyword in the vault files.       ///
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
 * TextSearch class performs keyword search on the vault files. 
 * It searches the keywords based on the swithces like /A /O /R, it scans the input folder,
 * It reads the files from in the vault and returns those files which has the matching keywords
 
 
 * Public Interface
 * =================
 * public bool partial_search(string contents, List<string> tokens, string file);
 * Input:Contents of the file,keywords and the file name 
 * Output:Decision whether the file contains the keyword or not
 * Returns:Boolean value
 * 
 * public bool full_search(string contents, List<string> tokens, string file)
 * Input:Contents of the file,keywords and the file name
 * Output:Decision whether the file contains every keyword or not
 * Returns:Boolean value
 * 
 * public List<string> non_recursivesearch(List<string> filepattern, string path, List<string> tokens, bool full_flag, bool partial_flag,string categories)
 * Input:Filepattern and categories in which files should be found, path of the file, keyowrds to be searched,flag indicating the type of search
 * Output:List of files the containing the keyword
 * Returns:Boolean value
 * 
 * Build Process
 * =============
 * Required Files:
 *   CommService.cs, Receiver.cs, Sender.cs
 *  
 * Maintenance History
 * ===================
 * ver 1.0 : Oct 4 2013
 *         - Created Class Text Search
 * ver 2.0 : Nov 17,2013
 *         - Added the text search based on categories
 *         - Removed the recursive search feature as no longer required
 * 
 * */

//#define TESTING_TEXT_SEARCH
#undef TESTING_TEXT_SEARCH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
namespace DocumentVault
{    //---<This module is responsible for carrying out the actual search in files >------
    //---Depending on switches it has functions for each kind of search
    class TextSearch
    {
        //------This function carries out the partial search in text files and displays the file names which have even a single query contents--
        public bool partial_search(string contents, List<string> tokens, string file)
        {
            List<string> filelist = new List<string>();
            foreach (string str in tokens)
            {
                string temp = str.Remove(0, 2);
                if (contents.Contains(temp))
                {
                    return true;
                }
            }
            return false;
            
        }
        
        //------This function carries out the full search in text files and displays the file names which possess all the query's cotents---
        public bool full_search(string contents, List<string> tokens, string file)
        {
            int key_count = 0;
            foreach (string str in tokens)
            {
                string temp = str.Remove(0, 2);
                if (contents.Contains(temp))
                {
                    key_count++;
                }
            }
            if (key_count == tokens.Count)
                return true;
            return false;
        }
        
        //This function is responsible for handling the docx format documents using the library provided by Microsoft interop for handling wor docs----------
        

        //This function only searches the required queries in the vault and returns the list of file which does
        public List<string> non_recursivesearch(List<string> filepattern, string path, List<string> tokens, bool full_flag, bool partial_flag,string categories)
        {
            List<string> files = new List<string>();
            string[] multiplecategory = categories.Split(',');
            XDocument doc = XDocument.Load(@"..\..\Category_Map.xml");
            Console.WriteLine(doc.ToString());
            foreach (string singlecategory in multiplecategory)
            {
                IEnumerable<XElement> filenames = doc.Descendants("category")
                .Where(s => s.FirstAttribute.Value.Equals(singlecategory))
                .SelectMany(s => s.Elements("filename"));                 //Extracting the files associated with the category
                foreach (var str in filenames)
                {
                    files.Add(str.Value.ToString());
                }
            }
            List<string> resultfilelist = new List<string>();
            foreach (string file in files)
            {
                try
            {
                string contents = "";
                TextReader tr = File.OpenText(@"..\..\DocumentVault\"+file);  //uses textreader object to acces the file contents
                    contents = tr.ReadToEnd();
                    tr.Close();
                    if (partial_flag)
                    {
                        bool outcome = partial_search(contents, tokens, file);
                        if (outcome) resultfilelist.Add(file);
                    }
                    else
                    {
                        if (full_flag)
                        {
                            bool outcome = full_search(contents, tokens, file);
                            if (outcome)
                                resultfilelist.Add(file);
                        }
                        else
                            full_search(contents, tokens, file);//ie if no flag is set then call default full search
                    }
                }//try
            catch (Exception except)
            { Console.Write("\n {0}\n\n", except.Message); }
        }//foreach
            return resultfilelist;
    }//func

        //test stub function for testing the individual class
#if TESTING_TEXT_SEARCH

        public static void Main(string[] args)
        {
            List<string> pattern = new List<string>();
            pattern.Add("*.txt");
            string categories="Source"
            List<string> textquery = new List<string>();
            textquery.Add("/Tvoid");
            string path = "../../testfiles";
            TextSearch TS = new TextSearch();
            Console.Write("\nI m testing Text Search");
            TS.recursivesearch(pattern, path, textquery,false, true,categories);
            //Console.Write("n  path = {0}\n  file pattern = {1}\n query string = {2}", path, pattern, queryString);
        }

#endif
    }
}
 
   
