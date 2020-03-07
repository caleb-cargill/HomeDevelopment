using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_Core
{
    class BudgetBuddyItem : INotifyPropertyChanged
    {

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public BudgetBuddyItem(CAZ_DB.Access_DB.BudgetBuddyItem item)
        {
            DBItem = item;
        }

        #endregion

        #region Properties

        private CAZ_DB.Access_DB.BudgetBuddyItem DBItem { get; set; }

        public string Date
        {
            get
            {
                if (_date == null && DBItem != null)
                    _date = DBItem.Date.ToString("MM/dd/yyyy");
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }
        private string _date;

        public string Name
        {
            get
            {
                if (_name == null && DBItem != null)
                    _name = DBItem.Name;
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private string _name;

        public string Type
        {
            get
            {
                if (_type == null && DBItem != null)
                    _type = DBItem.Type;
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }
        private string _type;

        public string Account
        {
            get
            {
                if (_account == null && DBItem != null)
                    _account = DBItem.Account;
                return _account;
            }
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }
        private string _account;

        public string Amount
        {
            get
            {
                if (_amount == null && DBItem != null)
                    _amount = $"{(DBItem.Amount < 0 ? "-" : "")}${Math.Abs(DBItem.Amount)}";
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }
        private string _amount;

        public string AccountTotal
        {
            get
            {
                if (_accountTotal == null && DBItem != null)
                    _accountTotal = $"{(DBItem.AccountTotal < 0 ? "-" : "")}${Math.Abs(DBItem.AccountTotal)}";
                return _accountTotal;
            }
            set
            {
                _accountTotal = value;
                OnPropertyChanged();
            }
        }
        private string _accountTotal;

        #endregion

        #region Methods

        public CAZ_DB.Access_DB.BudgetBuddyItem GetDBItem()
        {
            return DBItem;
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
