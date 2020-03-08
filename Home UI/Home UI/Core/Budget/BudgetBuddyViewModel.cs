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
using System.Collections.ObjectModel;

namespace Project_Core
{
    class BudgetBuddyViewModel : INotifyPropertyChanged
    {

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ICommand AddItemCommand => new CAZ_Common.Commands.DelegateCommand(AddItem);

        public ICommand DeleteItemCommand => new CAZ_Common.Commands.DelegateCommand(DeleteItem);

        public ICommand AddRecurringItemCommand => new CAZ_Common.Commands.DelegateCommand(AddRecurringItem);

        public ICommand DeleteRecurringItemCommand => new CAZ_Common.Commands.DelegateCommand(DeleteRecurringItem);

        public ICommand ResetFilterCommand => new CAZ_Common.Commands.DelegateCommand(ResetFilter);

        public BudgetBuddyViewModel()
        {
            ValidateInput();
            ValidateRecurringInput();
        }

        public ObservableCollection<BudgetBuddyItem> AccountItems
        {
            get
            {
                if (_accountItems == null)
                    _accountItems = new ObservableCollection<BudgetBuddyItem>(CAZ_DB.Access_DB.BudgetBuddyItems.GetBudgetItems().Select(bbi => new BudgetBuddyItem(bbi)));
                return _accountItems;
            }
        }
        private ObservableCollection<BudgetBuddyItem> _accountItems;

        public BudgetBuddyItem AccountItem
        {
            get
            {
                if (_accountItem == null)
                    _accountItem = new BudgetBuddyItem(new CAZ_DB.Access_DB.BudgetBuddyItem());
                return _accountItem;
            }
            set
            {
                _accountItem = value;
                OnPropertyChanged();
            }
        }
        private BudgetBuddyItem _accountItem;

        public ObservableCollection<RecurringItem> RecurringAccountItems
        {
            get
            {
                if (_recurringAccountItems == null)
                    _recurringAccountItems = new ObservableCollection<RecurringItem>(CAZ_DB.Access_DB.RecurringItems.GetRecurringItems().Select(ri => new RecurringItem(ri)));
                return _recurringAccountItems;
            }
        }
        private ObservableCollection<RecurringItem> _recurringAccountItems;

        public RecurringItem RecurringAccountItem
        {
            get
            {
                if (_recurringItem == null)
                    _recurringItem = new RecurringItem(new CAZ_DB.Access_DB.RecurringItem());
                return _recurringItem;
            }
            set
            {
                _recurringItem = value;
                OnPropertyChanged();
            }
        }
        private RecurringItem _recurringItem;

        public bool IsAmountValid
        {
            get
            {
                return _isAmountValid;
            }
            set
            {
                _isAmountValid = value;
                OnPropertyChanged();
            }
        }
        private bool _isAmountValid;

        public bool IsRecurringAmountValid
        {
            get
            {
                return _isRecurringAmountValid;
            }
            set
            {
                _isRecurringAmountValid = value;
                OnPropertyChanged();
            }
        }
        private bool _isRecurringAmountValid;

        public bool IsTotalValid
        {
            get
            {
                return _isTotalValid;
            }
            set
            {
                _isTotalValid = value;
                OnPropertyChanged();
            }
        }
        private bool _isTotalValid;

        public List<string> ItemTypes
        {
            get
            {
                return new List<string>() { "Expense", "Income" };
            }
        }

        public List<string> FilterItemTypes
        {
            get
            {
                return new List<string>() { "All" }.Concat<string>(ItemTypes).ToList();
            }
        }

        public object SelectedBudgetItem
        {
            get
            {
                return _selectedBudgetItem;
            }
            set
            {
                _selectedBudgetItem = value;
                OnPropertyChanged();
            }
        }
        private object _selectedBudgetItem;

        public object SelectedRecurringAccountItem
        {
            get
            {
                return _selectedRecurringAccountItem;
            }
            set
            {
                _selectedRecurringAccountItem = value;
                OnPropertyChanged();
            }
        }
        private object _selectedRecurringAccountItem;

        public List<string> AccountNames
        {
            get
            {
                return CAZ_DB.Access_DB.Accounts.GetAccounts().Values.ToList<string>();
            }
        }

        public List<string> FilterAccountNames
        {
            get
            {
                return new List<string>() { "All" }.Concat<string>(AccountNames).ToList();
            }
        }

        public string FromDateFilter
        {
            get
            {
                return _fromDateFilter;
            }
            set
            {
                _fromDateFilter = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AccountItems));
            }
        }
        private string _fromDateFilter;

        public string ToDateFilter
        {
            get
            {
                return _toDateFilter;
            }
            set
            {
                _toDateFilter = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AccountItems));
            }
        }
        private string _toDateFilter;

        public string SelectedAccount
        {
            get
            {
                if (_selectedAccount == null)
                    _selectedAccount = FilterAccountNames.First();
                return _selectedAccount;
            }
            set
            {
                _selectedAccount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AccountItems));
            }
        }
        private string _selectedAccount;

        public string SearchedItem
        {
            get
            {
                return _searchedItem;
            }
            set
            {
                _searchedItem = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AccountItems));
            }
        }
        private string _searchedItem;

        public string SelectedType
        {
            get
            {
                if (_selectedType == null)
                    _selectedType = FilterItemTypes.First();
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AccountItems));
            }
        }
        private string _selectedType;

        public bool FilterAccountItems(BudgetBuddyItem item)
        {
            DateTime date, date2;

            if (FromDateFilter != null && FromDateFilter != "")
            {
                DateTime.TryParse(FromDateFilter, out date);
                DateTime.TryParse(item.Date.ToString(), out date2);
                if (!(date2 >= date))
                    return false;
            }

            if (ToDateFilter != null && ToDateFilter != "")
            {
                DateTime.TryParse(ToDateFilter, out date);
                DateTime.TryParse(item.Date.ToString(), out date2);
                if (!(date2 <= date))
                    return false;
            }

            if (SelectedType != "All")
            {
                if (!item.Type.Equals(SelectedType))
                    return false;
            }

            if (SelectedAccount != "All")
            {
                if (!item.Account.Equals(SelectedAccount))
                    return false;
            }

            if (SearchedItem != null && SearchedItem != "")
            {
                if (!item.Name.ToLower().Contains(SearchedItem.ToLower()) && !item.Amount.ToString().Contains(SearchedItem))
                    return false;
            }

            return true;
        }

        private void AddItem(object commandParameter)
        {
            if (ValidateInput())
            {
                CAZ_DB.Access_DB.BudgetBuddyItems.AddItem(AccountItem.GetDBItem());
                AccountItem = null;
                OnPropertyChanged(nameof(AccountItem));
                _accountItems = null;
                OnPropertyChanged(nameof(AccountItems));
            }
        }

        private void AddRecurringItem(object commandParameter)
        {
            if (ValidateRecurringInput())
            {
                CAZ_DB.Access_DB.RecurringItems.AddItem(RecurringAccountItem.GetDBItem());
                RecurringAccountItem = null;
                OnPropertyChanged(nameof(RecurringAccountItem));
                _recurringAccountItems = null;
                OnPropertyChanged(nameof(RecurringAccountItems));
            }
        }

        private void DeleteItem(object commandParameter)
        {
            CAZ_DB.Access_DB.BudgetBuddyItems.DeleteItem(((BudgetBuddyItem)SelectedBudgetItem).GetDBItem().ID);
            _accountItems = null;
            OnPropertyChanged(nameof(AccountItems));
        }

        private void DeleteRecurringItem(object commandParameter)
        {
            CAZ_DB.Access_DB.RecurringItems.DeleteItem(((RecurringItem)SelectedRecurringAccountItem).GetDBItem().ID);
            _recurringAccountItems = null;
            OnPropertyChanged(nameof(RecurringAccountItems));
        }

        private bool ValidateInput()
        {
            IsAmountValid = CAZ_Common.Validators.IsStringValidDecimal(AccountItem.Amount.Split('$').Last());

            IsTotalValid = CAZ_Common.Validators.IsStringValidDecimal(AccountItem.AccountTotal.Split('$').Last());

            return IsAmountValid && IsTotalValid;
        }

        private bool ValidateRecurringInput()
        {
            IsRecurringAmountValid = CAZ_Common.Validators.IsStringValidDecimal(RecurringAccountItem.Amount.Split('$').Last());

            return IsRecurringAmountValid;
        }

        private void ResetFilter(object commandParameter)
        {
            FromDateFilter = "";
            ToDateFilter = "";
            SelectedType = FilterItemTypes.First();
            SelectedAccount = FilterAccountNames.First();
            SearchedItem = "";
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
