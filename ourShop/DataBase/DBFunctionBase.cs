using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace ourShop.DataBase
{
    public class DBFunctionBase : DBBase
    {
        protected List<object[]> GetTableFunction(string schema, string functionName, List<ObjectParameter> parameters)
        {
            try
            {
                String SQL = "select * from " + schema + "." + functionName + "(";
                int parametersCount = parameters.Count();
                int i = 1;

                foreach (var param in parameters)
                {
                    SQL += "'" + param.Value + "'";

                    if (i < parametersCount)
                        SQL += ",";

                    i++;
                }

                SQL += ")";


                using (var dbo = new ourShopEntities())
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(dbo.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(SQL, conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                var result = new List<object[]>();

                                while (reader.Read())
                                {
                                    object[] row = new object[reader.FieldCount];

                                    reader.GetValues(row);

                                    if (row != null)
                                    {
                                        result.Add(row);
                                    }
                                }
                                reader.Close();
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ZAPIS DO LOGÓW DATABASE!
                return null;
            }
        }
    }
}