using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_UI.Home_Hub
{
    public class BudgetBuddyItemViewModel
    {
        public BudgetBuddyItemViewModel()
        {

        }

        public BudgetBuddyItemViewModel(string date, string account, string name, string type, decimal amount, decimal accountTotal)
        {
            Date = date;
            Account = account;
            Name = name;
            Type = type;
            Amount = Decimal.ToDouble(amount);
            AccountTotal = Decimal.ToDouble(accountTotal);
        }

        public string Date
        {
            get
            {
                if (_date == null)
                    _date = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                return _date;
            }
            set
            {
                _date = value;
            }
        }
        private string _date;

        public string Name { get; set; }

        public string Type { get; set; }

        public double Amount { get; set; }

        public double AccountTotal { get; set; }

        public string Account { get; set; }

    }
}
