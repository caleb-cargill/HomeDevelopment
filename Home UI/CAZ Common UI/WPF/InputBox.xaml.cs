using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CAZ.Common.UI.WPF
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window, INotifyPropertyChanged
    {

        #region "Events"

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region "Constructor"

        public InputBox(string title,
                        string defaultMessage,
                        string defaultResponse,
                        string highlightText = "",
                        bool isCombo = false,
                        List<string> comboItems = null)
        {
            InitializeComponent();

            InputTitle = title;
            DefaultMessage = defaultMessage;
            Value = defaultResponse;
            IsTextBlock = !isCombo;
            IsComboBox = isCombo;
            if (IsComboBox)
            {
                ComboBoxItems = comboItems;
                if (!comboItems.Contains(Value))
                    Value = ComboBoxItems.First();
            }
            else
            {
                HighlightText(highlightText);
            }
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// title of the input box
        /// </summary>
        public string InputTitle
        {
            get
            {
                return _inputTitle;
            }
            set
            {
                _inputTitle = value;
                OnPropertyChanged();
            }
        }
        private string _inputTitle;

        /// <summary>
        /// default message to display to the user
        /// </summary>
        public string DefaultMessage
        {
            get
            {
                return _defaultMessage;
            }
            set
            {
                _defaultMessage = value;
                OnPropertyChanged();
            }
        }
        private string _defaultMessage;

        /// <summary>
        /// Value entered by the user
        /// </summary>
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        private string _value;

        /// <summary>
        /// bool to denote whether this will be a textblock entered value
        /// </summary>
        public bool IsTextBlock
        {
            get
            {
                return _isTextBlock;
            }
            set
            {
                _isTextBlock = value;
                OnPropertyChanged();
            }
        }
        private bool _isTextBlock;

        /// <summary>
        /// bool to denote whether this will be a combobox entered value
        /// </summary>
        public bool IsComboBox
        {
            get
            {
                return _isComboBox;
            }
            set
            {
                _isComboBox = value;
                OnPropertyChanged();
            }
        }
        private bool _isComboBox;

        /// <summary>
        /// List of items to fill the combobox
        /// </summary>
        public List<string> ComboBoxItems
        {
            get
            {
                if (_comboBoxItems == null)
                    _comboBoxItems = new List<string>();
                return _comboBoxItems;
            }
            set
            {
                _comboBoxItems = value;
                OnPropertyChanged();
            }
        }
        private List<string> _comboBoxItems;

        /// <summary>
        /// Error Message to show to the user
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        private string _errorMessage;

        /// <summary>
        /// bool to denote whether the error message will be visible or not
        /// </summary>
        public bool IsErrorMessageVisible
        {
            get
            {
                return _isErrorMessageVisible;
            }
            set
            {
                _isErrorMessageVisible = value;
                OnPropertyChanged();
            }
        }
        private bool _isErrorMessageVisible;

        #endregion

        #region "Methods"

        /// <summary>
        /// highlights the selected text
        /// </summary>
        /// <param name="highlightText"></param>
        private void HighlightText(string highlightText)
        {
            TbTextInput.Focus();
            TbTextInput.SelectedText = highlightText;
        }

        /// <summary>
        /// click event for the cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// click event for the ok button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            IsErrorMessageVisible = false;
            if (Value == "")
            {
                ErrorMessage = "Enter a value to continue";
                IsErrorMessageVisible = true;
            }
            else
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        /// <summary>
        /// Raises OnPropertyChanged Event
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null, object newValue = null, object oldValue = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName: propertyName));
        }

        #endregion

    }
}
