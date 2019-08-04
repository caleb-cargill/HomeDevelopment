using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Home_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ToDoList : Window
    {

        #region "Constructor"

        public ToDoList()
        {
            InitializeComponent();

            this.DataContext = this;
            DueDate = DateTime.Today.ToShortDateString();

            // Initialize _toDoItems, check excel spreadsheet 
            SetListViewProps();
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Name object of to do item
        /// </summary>
        public string ToDoName
        {
            get
            {
                return _toDoName;
            }
            set
            {
                _toDoName = value;
            }
        }
        private string _toDoName;

        /// <summary>
        /// Priority object of to do item
        /// </summary>
        public string Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                _priority = value;
            }
        }
        private string _priority;

        /// <summary>
        /// List for the combo box of priority options
        /// </summary>
        public List<String> Priorities
        {
            get
            {
                List<string> _priorities = new List<string>
                {
                    "None",
                    "Low",
                    "Medium",
                    "High"
                };
                return _priorities;
            }
        }

        /// <summary>
        /// Date object to assign due date of to do item
        /// </summary>
        public string DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                _dueDate = value;
            }
        }
        private string _dueDate;

        /// <summary>
        /// List of item properties to set listview 
        /// </summary>
        public List<ToDoProperties> ToDoItems
        {
            get
            {
                return _toDoItems;
            }
            set
            {
                _toDoItems = value;
            }
        }
        private List<ToDoProperties> _toDoItems;

        public string TextDocFilePath
        {
            get
            {
                return @"C:\Home_UI\ToDoList\ToDoList.txt";
            }
        }

        #endregion

        #region "Methods"

        #region "List View Methods"

        /// <summary>
        /// Initializes list view by checking for text document
        /// reads the text document if it exists, creates new list if it does not
        /// </summary>
        private void SetListViewProps()
        {
            ToDoItems = new List<ToDoProperties>();
            if (!(File.Exists(TextDocFilePath)))
            {
                File.Create(TextDocFilePath);
            } else
            {
                ReadItems();
            }
        }

        #endregion

        #region "Text Document Methods"

        /// <summary>
        /// Adds an item to the text document using ToDoItems
        /// </summary>
        private void WriteItems()
        {            
            List<string> lines = new List<string>();

            // Clear the text document
            File.WriteAllLines(TextDocFilePath, lines.ToArray());

            // Set list of string to add to text document
            foreach (ToDoProperties prop in ToDoItems)
            {
                lines.Add(prop.Name + ":" + prop.Priority + ":" + prop.Date);
            }            

            // Write to the text document
            File.WriteAllLines(TextDocFilePath, lines.ToArray());
        }

        /// <summary>
        /// Reads the text document to initialize or update ToDoItems
        /// </summary>
        private void ReadItems()
        {
            // Reset ToDoItems and read text document
            ToDoItems.Clear();
            string[] lines = File.ReadAllLines(TextDocFilePath);

            // Look at each line from the text document and set ToDoItems
            foreach (string line in lines)
            {
                string[] props = line.Split(':');
                ToDoProperties item = new ToDoProperties();
                item.Name = props[0];
                item.Priority = props[1];
                item.Date = props[2];
                ToDoItems.Add(item);
            }                
        }

        #endregion

        #region "Click Event Methods

        /// <summary>
        /// Adds entered to do item to the list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Add item to ToDoItems
            ToDoProperties item = new ToDoProperties();
            item.Name = tbItemName.Text;
            item.Priority = cbPriority.SelectedItem.ToString();
            item.Date = tbDueDate.Text;
            ToDoItems.Add(item);

            // Write the item to the text document
            WriteItems();

            // Clear out entered information
            tbItemName.Text = "";
            cbPriority.SelectedIndex = 0;
            tbDueDate.Text = DateTime.Today.ToShortDateString();

            // Disable add button
            btnAdd.IsEnabled = false;

            // Reload the list view
            ReadItems();
        }

        /// <summary>
        /// Checks to see if all fields are filled out to enable add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbDueDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbDueDate.Text != "" && tbItemName.Text != "" && cbPriority.SelectedItem != null)
            {
                btnAdd.IsEnabled = true;
            }
            else
            {
                btnAdd.IsEnabled = false;
            }
        }

        /// <summary>
        /// Checks to see if all fields are filled out to enable add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbDueDate.Text != "" && tbItemName.Text != "" && cbPriority.SelectedItem != null)
            {
                btnAdd.IsEnabled = true;
            }
            else
            {
                btnAdd.IsEnabled = false;
            }
        }

        /// <summary>
        /// Checks to see if all fields are filled out to enable add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbDueDate.Text != "" && tbItemName.Text != "" && cbPriority.SelectedItem != null)
            {
                btnAdd.IsEnabled = true;
            }
            else
            {
                btnAdd.IsEnabled = false;
            }
        }

        #endregion

        #endregion


    }
}


