﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;

namespace jjlbs_service.oracle
{
    public class Oraclehp
    {
        public string connectionString = "User Id=LBSUSER;Password=LBSPWD;Data Source=10.1.8.17:1521/JJLBS";

        public DataSet Query(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);

                    command.Fill(ds, "ds");
                }
                catch (OracleException ex)
                {

                    throw new Exception(ex.Message);
                }
                connection.Close();
                return ds;
            }
        }

        public int QueryCUD(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (OracleCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = SQLString;
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        return 1;
                    }
                }
                catch (OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}