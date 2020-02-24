using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CAZ_Common.Commands;
using System.Windows.Data;

namespace Home_UI.Home_Hub
{
    class BudgetBuddyViewModel : INotifyPropertyChanged
    {

        #region "Events"

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ICommand AddItemCommand => new CAZ_Common.Commands.DelegateCommand(AddItem);

        public BudgetBuddyViewModel()
        {
            InitializeTable();
        }

        public DataTable AccountTable
        {
            get
            {
                return _accountTable;
            }
            set
            {
                _accountTable = value;
            }
        }
        private DataTable _accountTable;

        public CollectionViewSource BudgetBuddyDataView
        {
            get
            {
                _budgetBuddyDataView = new CollectionViewSource();
                _budgetBuddyDataView.Source = AccountItems;
                return _budgetBuddyDataView;
            }
            set
            {
                _budgetBuddyDataView = value;
                OnPropertyChanged();
            }
        }
        private CollectionViewSource _budgetBuddyDataView;

        public BudgetBuddyItemViewModel ItemToAdd
        {
            get
            {
                if (_itemToAdd == null)
                    _itemToAdd = new BudgetBuddyItemViewModel();
                return _itemToAdd;
            }
            set
            {
                _itemToAdd = value;
            }
        }
        private BudgetBuddyItemViewModel _itemToAdd;

        public List<BudgetBuddyItemViewModel> AccountItems
        {
            get
            {
                return _accountItems;
            }
            set
            {
                _accountItems = value;
            }
        }
        private List<BudgetBuddyItemViewModel> _accountItems;

        public List<string> ItemTypes
        {
            get
            {
                return new List<string>() { "Expense", "Income" };
            }
        }

        public List<string> AccountNames
        {
            get
            {
                return new List<string>() { "Checking", "Savings" };
            }
        }

        private void InitializeTable()
        {
            AccountTable = CAZ_DB.TblBudgetBuddy.GetTable();

            AccountItems = (from DataRow dr in AccountTable.Rows select dr).ToList().Select(dr => new BudgetBuddyItemViewModel(dr[nameof(BudgetBuddyItemViewModel.Date)].ToString(), dr[nameof(BudgetBuddyItemViewModel.Account)].ToString(),dr[nameof(BudgetBuddyItemViewModel.Name)].ToString(), dr[nameof(BudgetBuddyItemViewModel.Type)].ToString(), (decimal)dr[nameof(BudgetBuddyItemViewModel.Amount)], (decimal)dr[nameof(BudgetBuddyItemViewModel.AccountTotal)])).ToList();
        }

        private void AddItem(object commandParameter)
        {
            AccountItems.Add(ItemToAdd);
            OnPropertyChanged(nameof(AccountItems));
            OnPropertyChanged(nameof(BudgetBuddyDataView));
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
    }
}
