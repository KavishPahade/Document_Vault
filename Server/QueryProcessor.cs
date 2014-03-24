/////////////////////////////////////////////////////////////////////
///  QueryProcessor.cs - Analysis of the query and calling appropriate functions.   ///
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
 * QueryProcessor class performs analysis of the query and splits the tokens into switches and keywords
 * Calls the apropriate functions based on the switches 
 
 * Public Interface
 * =================
 * public List <string> tokenAnalyzer(List <string> filepattern, string path, string queryString,string categories)
 * Input:Filepattern to look into, path of the document fault, the queries seaparated by a sentinel and categories 
 * Output:Decision based on switches the function of text search to be called
 * Returns:List of files that contains the keyword
 * 
 * 
 * Build Process
 * =============
 * Required Files:
 *   CommService.cs, Receiver.cs, Sender.cs
 *  
 * Maintenance History
 * ===================
 * ver 1.0 : Oct 4 2013
 *         - Created Class QueryProcessor
 * ver 2.0 : Nov 17,2013
 *         -Removed the recursive search functionality as no longer required
 * 
 * */
//#define TESTING_QUERY_PROCESSOR
#undef TESTING_QUERY_PROCESSOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DocumentVault
{  //---<This module is responsible for breaking the query into switches and data to be searched>
   //---It is also responsible for calling the appropiate function based on switches
    public class QueryProcessor
    {
        public List <string> tokenAnalyzer(List <string> filepattern, string path, string queryString,string categories)
        {
            bool recursive_flag = false, partial_flag = false, full_flag = false,text_search=false, metadata_search=false;
            List<string> tokenize = new List<string>(), textquery = new List<string>(), metaquery = new List<string>();
            string input = queryString,pattern = "(/)";
            string[] substrings = Regex.Split(input, pattern);    // Split on hyphens 
            for (int i = 1,k=0; i < substrings.Length - 1; i++)
            {
                tokenize.Add(substrings[i] + substrings[i + 1]);
                i++; k++;
            }
            foreach (string str in tokenize)
            {
                if (str.StartsWith("/T"))
                {
                    textquery.Add(str);
                    text_search = true;       //setting the flag for text search if the query has /T switch
                }
               if (str.StartsWith("/M"))
                {
                    string stringsplitter=str.Remove(0,2);
                    string []temparray=stringsplitter.Split(',');
                    foreach (string tempstring in temparray)
                    {
                        metaquery.Add(tempstring);
                    }
                    metadata_search = true;   //setting the flag for text search if the query has /T switch
                }
                if (str.StartsWith("/A"))
                 full_flag = true;        //setting the flag for full search if the query has /A switch 
                if (str.StartsWith("/O"))  
                 partial_flag = true;     //setting the flag for partial search if the queey has /O flag  
            }//foreach
            TextSearch TS = new TextSearch();
            MetadataSearch MS= new MetadataSearch();
            List<string> resultfilelist = new List<string>();
            if (text_search)      //based on switches calling the appropriate function and passing the query and flags
            {
                    resultfilelist = TS.non_recursivesearch(filepattern, path, textquery, full_flag, partial_flag, categories);
            }
            else
                if(metadata_search)
                   resultfilelist= MS.XMLSearch(filepattern,path, metaquery, recursive_flag,categories);
           return resultfilelist;
        }

        //test stub function for testing the individual class
#if TESTING_QUERY_PROCESSOR

        public static void Main(string[] args)
        {
            List<string> pattern = new List<string>();
            pattern.Add("*.txt");
            string path = "../../testfiles";
            string querystring = "/Tvoid";
            QueryProcessor QP = new QueryProcessor();
            Console.Write("\nI m testing Query Processor");
            QP.tokenAnalyzer(pattern,path,querystring);
            //Console.Write("n  path = {0}\n  file pattern = {1}\n query string = {2}", path, pattern, queryString);
        }

#endif
    }
}


