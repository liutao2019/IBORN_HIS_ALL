using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;

namespace FS.SOC.HISFC.Components.Project
{
    public class Export
    {
        public void ToDBF()
        {
            System.Data.Odbc.OdbcConnection conn = new System.Data.Odbc.OdbcConnection();
            string connectStr = @"Driver={Microsoft dBASE Driver (*.dbf)};Driverid=277;Dbq=C:/";
            conn.ConnectionString = connectStr;
            conn.Open();
            System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("Create Table TestTable2 (Field1 int, Field2 char(10))", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            
        }
    }
}
