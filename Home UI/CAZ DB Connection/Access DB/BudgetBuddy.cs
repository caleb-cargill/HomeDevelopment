using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.OleDb;

namespace CAZ_DB.Access_DB
{
    public static class BudgetBuddyItems
    {
        private static string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = C:\Users\caleb\source\repos\HomeDevelopment\Home UI\CAZ DB Connection\Access DB\CAZAccessDB.accdb";
        private static string query = "SELECT * FROM Tbl_BudgetBuddy";
        private static DataTable dataTable;
        private static OleDbConnection myConn;

        public static List<BudgetBuddyItem> GetBudgetItems()
        {
            myConn = new OleDbConnection(connection);
            OleDbDataAdapter myCmd = new OleDbDataAdapter(query, myConn);
            myConn.Open();

            DataSet dtSet = new DataSet();
            myCmd.Fill(dtSet, "Tbl_BudgetBuddy");
            dataTable = dtSet.Tables[0];

            List<BudgetBuddyItem> items = new List<BudgetBuddyItem>();

            foreach (DataRow dr in dataTable.Rows)
                items.Add(new BudgetBuddyItem(dr));

            myConn.Close();

            return items;
        }

        public static void AddItem(BudgetBuddyItem item)
        {
            myConn = new OleDbConnection(connection);
            DateTime date = new DateTime();
            DateTime.TryParse(item.Date, out date);
            OleDbCommand myCmd = new OleDbCommand("INSERT INTO Tbl_BudgetBuddy (Item Date, Item Name, Item Type, Account, Amount, Account Total) VALUES (@Date, @Name, @Type, @Account, @Amount, @AccountTotal)");
            myCmd.Connection = myConn;

            myConn.Open();
            myCmd.Parameters.AddWithValue("@Date", date.ToString("yyyyMMdd"));
            myCmd.Parameters.AddWithValue("@Name", item.Name);
            myCmd.Parameters.AddWithValue("@Type", item.Type);
            myCmd.Parameters.AddWithValue("@Account", item.Account);
            myCmd.Parameters.AddWithValue("@Amount", Convert.ToDecimal(item.Amount));
            myCmd.Parameters.AddWithValue("@AccountTotal", Convert.ToDecimal(item.AccountTotal));
            myCmd.ExecuteNonQuery();
            myConn.Close();
        }
    }

    public class BudgetBuddyItem : INotifyPropertyChanged
    {

        #region "Events"

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public BudgetBuddyItem()
        {

        }

        public BudgetBuddyItem(DataRow _dr)
        {
            dr = _dr;
        }

        private DataRow dr;

        public string Date
        {
            get
            {
                if (_date == null && dr != null)
                    _date = dr.Field<DateTime>("Item Date").ToString();
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
                if (_name == null && dr != null)
                    _name = dr.Field<string>("Item Name");
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
                if (_type == null && dr != null)
                    _type = dr.Field<string>("Item Type");
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
                if (_account == null && dr != null)
                    _account = dr.Field<string>("Account");
                return _account;
            }
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }
        private string _account;

        public double Amount
        {
            get
            {
                if (_amount == 0 && dr != null)
                    _amount = Decimal.ToDouble(dr.Field<Decimal>("Amount"));
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }
        private double _amount;

        public double AccountTotal
        {
            get
            {
                if (_accountTotal == 0 && dr != null)
                    _accountTotal = Decimal.ToDouble(dr.Field<Decimal>("Account Total"));
                return _accountTotal;
            }
            set
            {
                _accountTotal = value;
                OnPropertyChanged();
            }
        }
        private double _accountTotal;

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
