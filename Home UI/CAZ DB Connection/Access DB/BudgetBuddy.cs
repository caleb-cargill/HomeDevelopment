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

        #region Properties

        private static string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = C:\Users\caleb\source\repos\HomeDevelopment\Home UI\CAZ DB Connection\Access DB\CAZAccessDB.accdb";
        private static string query = "SELECT * FROM Tbl_BudgetBuddy";
        private static DataTable dataTable;
        private static OleDbConnection myConn;

        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of budget buddy items for the application to display
        /// </summary>
        /// <returns></returns>
        public static List<BudgetBuddyItem> GetBudgetItems()
        {
            // Setup connection and command
            myConn = new OleDbConnection(connection);
            OleDbDataAdapter myCmd = new OleDbDataAdapter(query, myConn);
            myConn.Open();

            // fill a data table
            DataSet dtSet = new DataSet();
            myCmd.Fill(dtSet, "Tbl_BudgetBuddy");
            dataTable = dtSet.Tables[0];

            // Create list of BudgetBuddyItems
            List<BudgetBuddyItem> items = new List<BudgetBuddyItem>();
            foreach (DataRow dr in dataTable.Rows)
                items.Add(new BudgetBuddyItem(dr));

            // Close the connection and return the items
            myConn.Close();
            return items;
        }

        /// <summary>
        /// Adds an item to the data base table
        /// </summary>
        /// <param name="item"></param>
        public static void AddItem(BudgetBuddyItem item)
        {
            // Setup connection and parse date
            myConn = new OleDbConnection(connection);
            DateTime date = new DateTime();
            DateTime.TryParse(item.Date, out date);

            try
            {
                // Open connection and setup command
                myConn.Open();
                OleDbCommand myCmd = new OleDbCommand();
                myCmd.Connection = myConn;

                // Create insert statement
                myCmd.CommandText = "insert into Tbl_BudgetBuddy " +
                    "(ItemDate, ItemName, ItemType, Account, Amount, AccountTotal) " +
                    "values('" + date.ToString("MM/dd/yyyy") + "'," +
                    "'" + item.Name + "'," +
                    "'" + item.Type + "'," +
                    "'" + item.Account + "'," +
                    "'" + Convert.ToDecimal(item.Amount) + "'," +
                    "'" + Convert.ToDecimal(item.AccountTotal) + "')";

                // Execute command
                myCmd.ExecuteNonQuery();
            }
            catch
            {
                // TODO: Create error message
            }

            // Close the connection
            myConn.Close();
        }

        public static void DeleteItem(int id)
        {
            // Setup connection and parse date
            myConn = new OleDbConnection(connection);

            try
            {
                // Open connection and setup command
                myConn.Open();
                OleDbCommand myCmd = new OleDbCommand();
                myCmd.Connection = myConn;

                // setup delete command
                myCmd.CommandText = "delete from Tbl_BudgetBuddy where ID=" + id;

                // execute
                myCmd.ExecuteNonQuery();
            }
            catch
            {

            }

            // close the connection
            myConn.Close();
        }

        #endregion 

    }

    public class BudgetBuddyItem : INotifyPropertyChanged
    {

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public BudgetBuddyItem()
        {
            Amount = "0";
            AccountTotal = "0";
        }

        public BudgetBuddyItem(DataRow _dr)
        {
            dr = _dr;
        }

        #endregion

        #region Properties

        private DataRow dr;

        public int ID
        {
            get
            {
                return dr.Field<int>("ID");
            }
        }

        public string Date
        {
            get
            {
                if (_date == null && dr != null)
                    _date = dr.Field<DateTime>("ItemDate").ToString();
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
                    _name = dr.Field<string>("ItemName");
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
                    _type = dr.Field<string>("ItemType");
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

        public string Amount
        {
            get
            {
                if (_amount == null && dr != null)
                    _amount = dr.Field<Decimal>("Amount").ToString();
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
                if (_accountTotal == null && dr != null)
                    _accountTotal = dr.Field<Decimal>("AccountTotal").ToString();
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
