using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_Core
{
    class RecurringItem : INotifyPropertyChanged
    {

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public RecurringItem(CAZ_DB.Access_DB.RecurringItem item)
        {
            DBItem = item;
        }

        #endregion

        #region Properties

        private CAZ_DB.Access_DB.RecurringItem DBItem { get; set; }

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
                DBItem.Name = _name;
                OnPropertyChanged();
            }
        }
        private string _name;

        public string Repeat
        {
            get
            {
                if (_repeat == null && DBItem != null)
                    _repeat = DBItem.Repeat;
                return _repeat;
            }
            set
            {
                _repeat = value;
                DBItem.Repeat = _repeat;
                OnPropertyChanged();
            }
        }
        private string _repeat;

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
                DBItem.Account = Account;
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
                DBItem.Amount = CAZ_Common.Converters.ConvertCurrencyStringToDouble(_amount);
                OnPropertyChanged();
            }
        }
        private string _amount;

        #endregion

        #region Methods

        public CAZ_DB.Access_DB.RecurringItem GetDBItem()
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
