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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CAZ.Common.UI.WPF
{
    /// <summary>
    /// Interaction logic for CategoryTag.xaml
    /// </summary>
    public partial class CategoryTag : UserControl, INotifyPropertyChanged
    {

        #region "Events"

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region "Constructor"

        public CategoryTag(string tagName)
        {
            this.DataContext = this;

            InitializeComponent();

            TagName = tagName;

            OnPropertyChanged(nameof(BorderWidth));
            OnPropertyChanged(nameof(BorderBackground));
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// width of the tag
        /// </summary>
        public int BorderWidth
        {
            get
            {
                if (TbTagName != null)
                    return (TbTagName.Text.Length * 5) + 25;
                else
                    return 100;
            }
        }

        /// <summary>
        /// background of the tag
        /// </summary>
        public Brush BorderBackground
        {
            get
            {
                return Brushes.Red;
            }
        }

        /// <summary>
        /// text to display on the tag
        /// </summary>
        public string TagName
        {
            get
            {
                return _tagName;
            }
            set
            {
                _tagName = value;
                OnPropertyChanged();
            }
        }
        private string _tagName;

        #endregion

        #region "Methods"

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
