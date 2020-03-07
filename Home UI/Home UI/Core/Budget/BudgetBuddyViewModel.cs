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

        public ICommand ResetFilterCommand => new CAZ_Common.Commands.DelegateCommand(ResetFilter);

        public BudgetBuddyViewModel()
        {
            ValidateInput();
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
                return Accounts.GetAccounts().Values.ToList<string>();
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
                BudgetBuddyItems.AddItem(AccountItem.GetDBItem());
                OnPropertyChanged(nameof(AccountItem));
                AccountItem = null;
            }
        }

        private void DeleteItem(object commandParameter)
        {
            BudgetBuddyItems.DeleteItem(((BudgetBuddyItem)SelectedBudgetItem).GetDBItem().ID);
            OnPropertyChanged(nameof(AccountItem));
        }

        private bool ValidateInput()
        {
            IsAmountValid = CAZ_Common.Validators.IsStringValidDecimal(AccountItem.Amount.Split('$').Last());

            IsTotalValid = CAZ_Common.Validators.IsStringValidDecimal(AccountItem.AccountTotal.Split('$').Last());

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
