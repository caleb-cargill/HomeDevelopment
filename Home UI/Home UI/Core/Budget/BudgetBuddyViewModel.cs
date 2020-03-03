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
using CAZ_DB.Access_DB;

namespace Project_Core
{
    class BudgetBuddyViewModel : INotifyPropertyChanged
    {

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ICommand AddItemCommand => new CAZ_Common.Commands.DelegateCommand(AddItem);

        public ICommand DeleteItemCommand => new CAZ_Common.Commands.DelegateCommand(DeleteItem);

        public ICommand ResetFilterCommand => new CAZ_Common.Commands.DelegateCommand(ResetFilter);

        public BudgetBuddyViewModel()
        {
            ValidateInput();
        }

        public List<BudgetBuddyItem> AccountItems
        {
            get
            {
                return GetAccountItems();
            }
        }

        public BudgetBuddyItem AccountItem
        {
            get
            {
                if (_accountItem == null)
                    _accountItem = new BudgetBuddyItem();
                return _accountItem;
            }
            set
            {
                _accountItem = value;
                OnPropertyChanged();
            }
        }
        private BudgetBuddyItem _accountItem;

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

        public List<string> AccountNames
        {
            get
            {
                return new List<string>() { "Checking", "Savings" };
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

        private List<BudgetBuddyItem> GetAccountItems()
        {
            List<BudgetBuddyItem> myItems = BudgetBuddyItems.GetBudgetItems();

            if (FromDateFilter != null && FromDateFilter != "")
            {
                DateTime date = new DateTime();
                DateTime date2 = new DateTime();
                DateTime.TryParse(FromDateFilter, out date);
                myItems = myItems.Where(i => DateTime.TryParse(i.Date, out date2) && date2 >= date).ToList();
            }

            if (ToDateFilter != null && ToDateFilter != "")
            {
                DateTime date = new DateTime();
                DateTime date2 = new DateTime();
                DateTime.TryParse(ToDateFilter, out date);
                myItems = myItems.Where(i => DateTime.TryParse(i.Date, out date2) && date2 <= date).ToList();
            }

            if (SelectedType != "All")
            {
                myItems = myItems.Where(i => i.Type.Equals(SelectedType)).ToList();
            }

            if (SelectedAccount != "All")
            {
                myItems = myItems.Where(i => i.Account.Equals(SelectedAccount)).ToList();
            }

            if (SearchedItem != null && SearchedItem != "")
            {
                myItems = myItems.Where(i => i.Name.ToLower().Contains(SearchedItem.ToLower()) || i.Amount.ToString().Contains(SearchedItem)).ToList();
            }

            return myItems;
        }

        private void AddItem(object commandParameter)
        {
            if (ValidateInput())
            {
                BudgetBuddyItems.AddItem(AccountItem);
                OnPropertyChanged(nameof(AccountItems));
                AccountItem = null;
            }
        }

        private void DeleteItem(object commandParameter)
        {
            BudgetBuddyItems.DeleteItem(((BudgetBuddyItem)SelectedBudgetItem).ID);
            OnPropertyChanged(nameof(AccountItems));
            AccountItem = null;
        }

        private bool ValidateInput()
        {
            IsAmountValid = CAZ_Common.Validators.IsStringValidDecimal(AccountItem.Amount);

            IsTotalValid = CAZ_Common.Validators.IsStringValidDecimal(AccountItem.AccountTotal);

            return IsAmountValid && IsTotalValid;
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
