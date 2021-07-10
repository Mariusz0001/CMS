using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ourShop.DataBase
{
    public class DbFunction
    {
        private static DbFunction _object;
        
        private DbFunction() { }
        
        public static DbFunction Instance()
        {
            if (_object == null)
            {
                _object = new DbFunction();
            }
            return _object;
        }

        public Boolean IsUserHasPermission(int userId, string PermissionName)
        {
            using (var dbo = new ourShopEntities())
            {
                var _userId = new NpgsqlParameter("_userId", NpgsqlDbType.Integer);
                _userId.Direction = System.Data.ParameterDirection.Input;
                _userId.Value = userId;

                var _permissionName = new NpgsqlParameter("_permissionName", NpgsqlDbType.Varchar);
                _permissionName.Direction = System.Data.ParameterDirection.Input;
                _permissionName.Value = PermissionName;

                var _ret = new NpgsqlParameter("_ret", NpgsqlDbType.Boolean);
                _ret.Direction = System.Data.ParameterDirection.InputOutput;
                _ret.Value = false;


                dbo.Database.ExecuteSqlCommand("select public.get_isuserhaspermission(@_userId, @_permissionName, @_ret);",
                    _userId, _permissionName, _ret);
                if (_ret != null && _ret.Value != null && Boolean.Parse(_ret.Value.ToString()) == true)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Beens.Get_MenuToolbar_Result> GetMenuToolbar(int userId)
        {
            try
            {
                var result = new List<Beens.Get_MenuToolbar_Result>();

                using (var dbo = new ourShopEntities())
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(dbo.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand("select * from public.get_menutoolbar(1)", conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    var values = new object[reader.FieldCount];
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        values[i] = reader[i];
                                    }
                                    //result.Add(values);
                                }

                                /*  while (reader.Read())
                                  {
                                      var row = reader.GetValue(i) as object[];
                                      if (row[i] != null)
                                      {
                                          int? toolbarId = Utils.TryParseNullable(row[0].ToString());
                                          int? idParentToolbar = Utils.TryParseNullable(row[1].ToString());

                                          result.Add(new Beens.Get_MenuToolbar_Result
                                          {
                                              ToolbarId = toolbarId,
                                              IdParentToolbar = idParentToolbar,
                                              ToolbarName = row[2].ToString(),
                                              IconName = row[3].ToString()

                                          }
                                          );
                                      }

                                      i++;
                                      reader.NextResult();
                                  }*/
                                reader.Close();
                                return result;
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