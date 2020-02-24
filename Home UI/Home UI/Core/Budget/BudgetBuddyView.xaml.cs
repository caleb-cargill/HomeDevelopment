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

namespace Project_Core
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BudgetBuddyView : Window
    {
        public BudgetBuddyView()
        {
            this.DataContext = new BudgetBuddyViewModel();
            InitializeComponent();
        }

        private void dgGeneral_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("ID"))
                e.Column.Visibility = Visibility.Collapsed;
        }
    }
}
