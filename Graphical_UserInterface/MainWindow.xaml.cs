/////////////////////////////////////////////////////////////////////
///  MAinWindow.xaml.cs -Defines the various events on the GUI and 
///                      its associated functionality             ///
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
 * MainWindow.xaml.cs class defined the various events fromthe GUI and Dispatches them to the GUI
 
 * 
 * Build Process
 * =============
 * Required Files:
 *   CommService.cs
 *  
 * Maintenance History
 * ===================
 * ver 1.0 : Nov 17 4 2013
 *         - Created Class
 *         -Added various events
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
namespace DocumentVault
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static MainWindow winob;
        Client c = new Client();
        public MainWindow()
        {
            InitializeComponent();
            winob = this;
            c.serverconnect();
            c.testmethod("","getcategories");
            //c.update_infoview(CatFile_Listbox.SelectedItem.ToString(), "getparentchild");
        }
        
       protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            const string str = "Graphical";
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.StartsWith(str))
                {
                    Console.WriteLine(@"Killing process " + str);
                    process.Kill();
                }
            }  
        }
        

        public void dispatch_categoryfiles(List<string> CategoryList)
        {
            CatFile_Listbox.Dispatcher.Invoke(

                    (Action)(() => { CatFile_Listbox.ItemsSource = CategoryList.Select(i => i); })
                    );

        }
        public void dispatch_searchreults(List<string> FileList)
        {
            results.Dispatcher.Invoke(

                    (Action)(() => { results.ItemsSource = FileList.Select(i => i); })
                    );
                          
           
        }
        public void dispatch_parentchild(List<string> ChildList, List<string> ParentList)
        {
           
                Child.Dispatcher.Invoke(

                    (Action)(() => { Child.ItemsSource = ChildList.Select(i => i); })
                    );
                Parents.Dispatcher.Invoke(

                        (Action)(() => { Parents.ItemsSource = ParentList.Select(i => i); })
                        );
                

        }

        public void dispatch_editXML(string outcome)
        {
           edit_text.Dispatcher.Invoke(

                        (Action)(() => { edit_text.Text = outcome; })
                        );

        }
        public void dispatch_filecontents(string contents,string filename)
        {

            if(filename.Contains(".xml"))
            {
               /* Metadata_Contents.Dispatcher.Invoke(
                    (Action)(() => { Metadata_Contents.Text = contents; })
                   );*/
                Metadata_Content.Dispatcher.Invoke(
                    (Action)(() =>
                    {
                        Metadata_Content.Document.Blocks.Clear();
                        Metadata_Content.Document.Blocks.Add(new Paragraph(new Run(contents)));
                    })
                   );
                Metadata_File.Dispatcher.Invoke(
                    (Action)(() =>
                    {
                        Metadata_File.Document.Blocks.Clear();
                        Metadata_File.Document.Blocks.Add(new Paragraph(new Run(contents)));
                    })
                   ); 
            }
            else
            {
            TextBox.Dispatcher.Invoke(

                   (Action)(() =>
                   {
                       TextBox.Document.Blocks.Clear();
                       TextBox.Document.Blocks.Add(new Paragraph(new Run(contents)));
                   })
                   );              
            }
        }
        public void upload_failed(string result)
        {

            dependency.Dispatcher.Invoke(

                    (Action)(() => { dependency.Text = result; }
                    )); 
        }
        public void dispatch_categories(List<string> CategoryList)
        {
            Category_Listbox.Dispatcher.Invoke(

                    (Action)(() => { Category_Listbox.ItemsSource = CategoryList.Select(i=>i); })
                    );
            Categories_Listbox.Dispatcher.Invoke(

                    (Action)(() => { Categories_Listbox.ItemsSource = CategoryList.Select(i => i); })
                    );
            category_list.Dispatcher.Invoke((Action)(() => { category_list.ItemsSource = CategoryList.Select(i => i); }));

        }
       /* internal string reply
        {
            get { return ""; }
            set
            {
                Dispatcher.Invoke(

                    (Action)(() => { Metadata_Contents.Text = value; })
                    );
            }
        }*/
        
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
            
           
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            Partial_Radio.Visibility = System.Windows.Visibility.Visible;
            Full_Radio.Visibility = System.Windows.Visibility.Visible;
            Keyword_Label.Content = "Enter Text";
        }


        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            Keyword_Label.Content = "Enter Tags";            
            Partial_Radio.Visibility = System.Windows.Visibility.Hidden;
            Full_Radio.Visibility = System.Windows.Visibility.Hidden;
            
        }

        private void Keyword_Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            if (dependency.Text == "Child dependencies are present")
            {
                string str = File_Textbox.Text;
                dependency_status.Visibility = System.Windows.Visibility.Hidden;
                status_upload.Visibility = System.Windows.Visibility.Visible;
                bool result = c.uploadfile(str, childs.Text);
                // string filename = System.IO.Path.GetFileName(File_Textbox.Text);
                var selected_categories = new List<string>();
                foreach (var temp in category_list.SelectedItems)
                {
                    selected_categories.Add(temp.ToString());
                }
                string categories = String.Join(",", selected_categories);
                c.createXML(File_Textbox.Text, categories, description.Text, childs.Text, keywords.Text);

                if (result)
                {
                    status.Text = "File uploaded successfully";
                    File_Textbox.Text = String.Empty;
                    description.Text = String.Empty;
                    childs.Text = String.Empty;
                    keywords.Text = String.Empty;
                }
                else
                    status.Text = "File did not uploaded successfully";
            }
            else
            {
                MessageBox.Show("Please enter correct dependent files", "Wrong Entry", MessageBoxButton.OK);

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Files (*.txt)|*.txt|Code Files(*.cs)|*.cs|CPP|*.cpp";
            dlg.FilterIndex = 3;
            dlg.Multiselect = false;
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                File_Textbox.Text = filename;
            }
        }

        private void Category_Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            c.category_click(Category_Listbox.SelectedItem.ToString(), "getcategoryfiles");
        }

        private void Files_Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CatFile_Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CatFile_Listbox.SelectedItem != null)
            {
                c.update_infoview(CatFile_Listbox.SelectedItem.ToString(), "getparentchild");
                c.get_file_contents(CatFile_Listbox.SelectedItem.ToString(), "get_file_contents");
                Current_File_TextBox.Text = CatFile_Listbox.SelectedItem.ToString();
                InfoView_CurrentFile_Textbox.Text = CatFile_Listbox.SelectedItem.ToString();
                Information_View.IsSelected = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selected_categories = new List<string>();
            if (Categories_Listbox.SelectedItem != null)
            {
                foreach (var temp in Categories_Listbox.SelectedItems)
                {
                    selected_categories.Add(temp.ToString());
                }
                string categories = String.Join(",", selected_categories);
                if (Text_Radio.IsChecked == true)
                {
                    string mode = "";
                    if (Partial_Radio.IsChecked == true)
                        mode = "/O";
                    else if (Full_Radio.IsChecked == true)
                        mode = "/A";
                    else
                        MessageBox.Show("Please select the search choices", "Wrong Entry", MessageBoxButton.OK);

                    c.Search(Keyword_Textbox.Text, categories, mode, "/T");

                }
                else
                {
                    if (Meta_Radio.IsChecked == true)
                    {
                        c.Search(Keyword_Textbox.Text, categories, "/Z", "/M");
                    }
                    else
                    {
                        MessageBox.Show("Please select the search category", "Wrong Entry", MessageBoxButton.OK);
                    }
                }
            }//null if
        }

        private void results_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (results.SelectedItem.ToString().Contains(".txt"))
            {
                c.update_infoview(results.SelectedItem.ToString(), "getparentchild");
                c.get_file_contents(results.SelectedItem.ToString(), "get_file_contents");
                Current_File_TextBox.Text = results.SelectedItem.ToString();
                InfoView_CurrentFile_Textbox.Text = results.SelectedItem.ToString();
                Information_View.IsSelected = true;
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Listview1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            c.update_infoview(CatFile_Listbox.SelectedItem.ToString(), "getparentchild");
            Information_View.IsSelected = true;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            c.editXML(Tags_textbox.Text, Contents_textbox.Text,Current_File_TextBox.Text);
        }

        private void Metadata_Content_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Document_Vault_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if (Category_Tab.IsSelected == true)
            {
                CatFile_Listbox.SelectedItems.Clear();
            }
        }

        private void Categories_Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            dependency.Visibility = System.Windows.Visibility.Visible;
            status_upload.Visibility = System.Windows.Visibility.Hidden;
            c.checkdependencies(childs.Text);

        }

        private void Child_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Child.SelectedItem != null)
            {
                c.update_infoview(Child.SelectedItem.ToString(), "getparentchild");
                c.get_file_contents(Child.SelectedItem.ToString(), "get_file_contents");
                Current_File_TextBox.Text = Child.SelectedItem.ToString();
                InfoView_CurrentFile_Textbox.Text = Child.SelectedItem.ToString();
                Information_View.IsSelected = true;
            }
        }

        private void Parents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Parents.SelectedItem != null)
            {
                c.update_infoview(Parents.SelectedItem.ToString(), "getparentchild");
                c.get_file_contents(Parents.SelectedItem.ToString(), "get_file_contents");
                Current_File_TextBox.Text = Parents.SelectedItem.ToString();
                InfoView_CurrentFile_Textbox.Text = Parents.SelectedItem.ToString();
                Information_View.IsSelected = true;
            }
        }

    }
}
