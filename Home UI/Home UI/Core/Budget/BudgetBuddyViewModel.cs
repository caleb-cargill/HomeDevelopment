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

        #region "Events"

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ICommand AddItemCommand => new CAZ_Common.Commands.DelegateCommand(AddItem);

        public BudgetBuddyViewModel()
        {

        }

        public List<BudgetBuddyItem> AccountItems
        {
            get
            {
                return BudgetBuddyItems.GetBudgetItems();
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

        private void AddItem(object commandParameter)
        {
            BudgetBuddyItems.AddItem(AccountItem);
            OnPropertyChanged(nameof(AccountItems));
            AccountItem = null;
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
