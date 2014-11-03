using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace testing_server
{
    [Serializable()]
    class Class1
    {
        private OleDbDataReader rd;
        private OleDbCommand cmd;
        private OleDbConnection Cn;
        public Class1()
        {
            //Cn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source =" + path + "; Jet OLEDB:Database password = medicine");
            //Cn.Open();
        }
        //public List<string> fetch_date(string query)
        //{
        //    cmd = new OleDbCommand(query,Cn);
        //    rd = cmd.ExecuteReader();

        //}
    }
}
