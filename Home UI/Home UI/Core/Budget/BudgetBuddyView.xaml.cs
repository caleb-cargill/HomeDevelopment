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
            vm = new BudgetBuddyViewModel();
            this.DataContext = vm;
            InitializeComponent();

            vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(BudgetBuddyViewModel.AccountItems)))
                CollectionViewSource.GetDefaultView(dgGeneral.ItemsSource).Refresh();
        }

        private BudgetBuddyViewModel vm;

        private void dgGeneral_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            vm.SelectedBudgetItem = ((BudgetBuddyItem)e.AddedCells.FirstOrDefault().Item);
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            BudgetBuddyItem item = (BudgetBuddyItem)e.Item;
            if (item != null)
                e.Accepted = vm.FilterAccountItems(item);
        }
    }
}
