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
using Home_UI.To_Do_List;
using System.ComponentModel;

namespace Home_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ToDoList : Window, INotifyPropertyChanged
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
                OnPropertyChanged("ToDoName");
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
                OnPropertyChanged("Priority");
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
                OnPropertyChanged("DueDate");
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
                OnPropertyChanged("ToDoItems");
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

        #region "Events"

        /// <summary>
        /// Event for property change call
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method to handle property change
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
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
            List<ToDoProperties> toDos = new List<ToDoProperties>();
            ToDoItems = toDos;
            string[] lines = File.ReadAllLines(TextDocFilePath);

            // Look at each line from the text document and set ToDoItems
            foreach (string line in lines)
            {
                string[] props = line.Split(':');
                ToDoProperties item = new ToDoProperties();
                item.Name = props[0];
                item.Priority = props[1];
                item.Date = props[2];
                toDos.Add(item);
            }

            ToDoItems = toDos;
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

        #region "Context Menu Methods"

        /// <summary>
        /// Context menu function to edit a currently selected to do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmEdit_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item
            ToDoProperties selectedProps = (ToDoProperties)lvToDo.SelectedItem;
            string myItem = selectedProps.Name + ":" + selectedProps.Priority + ":" + selectedProps.Date;

            // Show a new edit form and return edited version 
            EditWindow edit = new EditWindow(selectedProps.Name, selectedProps.Priority, selectedProps.Date);
            edit.ShowDialog();
            string newItem = edit.NewName + ":" + edit.NewPriority + ":" + edit.NewDate;

            // Reset ToDoItems and read text document
            ToDoItems.Clear();
            string[] lines = File.ReadAllLines(TextDocFilePath);

            // Look at each line from the text document and set ToDoItems
            foreach (string line in lines)
            {
                if (line != myItem)
                {
                    string[] props = line.Split(':');
                    ToDoProperties item = new ToDoProperties();
                    item.Name = props[0];
                    item.Priority = props[1];
                    item.Date = props[2];
                    ToDoItems.Add(item);
                }
            }

            // Add edited item
            string[] newProps = newItem.Split(':');
            ToDoProperties editItem = new ToDoProperties();
            editItem.Name = newProps[0];
            editItem.Priority = newProps[1];
            editItem.Date = newProps[2];
            ToDoItems.Add(editItem);

            // Rewrite the text document to update
            WriteItems();
        }

        /// <summary>
        /// Context menu function to delete a currently selected to do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmDelete_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item
            ToDoProperties selectedProps = (ToDoProperties)lvToDo.SelectedItem;
            string myItem = selectedProps.Name + ":" + selectedProps.Priority + ":" + selectedProps.Date;

            // Reset ToDoItems and read text document
            ToDoItems.Clear();
            string[] lines = File.ReadAllLines(TextDocFilePath);

            // Look at each line from the text document and set ToDoItems, removing the deleted one
            foreach (string line in lines)
            {
                if (line != myItem)
                {
                    string[] props = line.Split(':');
                    ToDoProperties item = new ToDoProperties();
                    item.Name = props[0];
                    item.Priority = props[1];
                    item.Date = props[2];
                    ToDoItems.Add(item);
                }
            }

            // Rewrite the text document to update
            WriteItems();
        }

        #endregion       

        #endregion


    }
}


