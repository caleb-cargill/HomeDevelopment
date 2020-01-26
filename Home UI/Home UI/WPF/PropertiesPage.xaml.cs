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

namespace Home_UI.WPF
{
    /// <summary>
    /// Interaction logic for PropertiesPage.xaml
    /// </summary>
    public partial class PropertiesPage : UserControl
    {
        public PropertiesPage()
        {
            InitializeComponent();
        }

        public int CurrentYear
        {
            get
            {
                if (_currentYear == 0)
                    _currentYear = DateTime.Now.Year;
                return _currentYear;
            }
            set
            {
                _currentYear = value;
            }
        }
        private int _currentYear;
    }
}
