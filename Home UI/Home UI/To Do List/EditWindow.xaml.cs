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
using System.Windows.Shapes;
using System.ComponentModel;

namespace Home_UI.To_Do_List
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window, INotifyPropertyChanged
    {
        public EditWindow(string name, string priority, string date)
        {
            InitializeComponent();

            // Initialize data values using current values
            NewName = name;
            NewPriority = (from x in NewPriorities where x == priority select x).FirstOrDefault();
            NewDate = date;

            // Update wpf controls
            tbNewName.Text = NewName;
            cbNewPriority.SelectedItem = NewPriority;
            tbNewDate.Text = NewDate;
        }

        #region "Events"

        /// <summary>
        /// Event for property changed
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

        #region "Properties"

        /// <summary>
        /// General name property
        /// </summary>
        public string NewName
        {
            get
            {
                return _newName;
            }
            set
            {
                _newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }
        private string _newName;

        /// <summary>
        /// General priority property
        /// </summary>
        public string NewPriority
        {
            get
            {
                return _newPriority;
            }
            set
            {
                _newPriority = value;
                OnPropertyChanged(nameof(NewPriority));
            }
        }
        private string _newPriority;

        /// <summary>
        /// Genreal date property
        /// </summary>
        public string NewDate
        {
            get
            {
                return _newDate;
            }
            set
            {
                _newDate = value;
                OnPropertyChanged(nameof(NewDate));
            }
        }
        private string _newDate;

        /// <summary>
        /// List for the combo box of priority options
        /// </summary>
        public List<string> NewPriorities
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

        #endregion

        #region "Methods"

        /// <summary>
        /// Click event for clicking the finish or cancel button button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            // Assign new values if the finish button is clicked 
            if (sender.ToString().Contains("Finish"))
            {
                NewName = tbNewDate.Text;
                NewPriority = cbNewPriority.SelectedItem.ToString();
                NewDate = tbNewDate.Text;
            }            

            // Close the window
            this.Close();
        }

        #endregion
    }
}
