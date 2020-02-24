using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace CAZ_DB
{

    public static class TblBudgetBuddy
    {

        private static string connectionString => "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\caleb\\Documents\\CAZDevelopment.accdb";
        private static string strSQL = "SELECT * FROM tbl_BudgetBuddy";

        public static DataTable GetTable()
        {
            OleDbConnection myConn = new OleDbConnection(connectionString);
            OleDbDataAdapter myCmd = new OleDbDataAdapter(strSQL, myConn);
            myConn.Open();

            DataSet dtSet = new DataSet();
            myCmd.Fill(dtSet, "Budget Buddy");
            DataTable dataTable = dtSet.Tables[0];
            myConn.Close();

            return dataTable;
        }

        public static void WriteTable(DataTable dataTable)
        {

        }
    }




}
