using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace ourShop.DataBase
{
    public class DbFunction : DBFunctionBase
    {
        private static DbFunction _object;
        
        private DbFunction()
        {
        }
        
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
                var _ret = CreateNpgsqlParameter("_ret", NpgsqlDbType.Boolean, false, System.Data.ParameterDirection.InputOutput);

                dbo.Database.ExecuteSqlCommand("select public.get_isuserhaspermission(@_userId, @_permissionName, @_ret);",
                    CreateNpgsqlParameter("_userId", NpgsqlDbType.Integer, userId),
                    CreateNpgsqlParameter("_permissionName", NpgsqlDbType.Varchar, PermissionName), 
                    _ret);
                if (_ret != null && _ret.Value != null && Boolean.Parse(_ret.Value.ToString()) == true)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Beens.Get_MenuToolbar_Result> GetMenuToolbar(int userId)
        {
            var paramList = new List<ObjectParameter>();

            var userIdParameters = userId > 0 ?
               new ObjectParameter("userId", userId) :
               new ObjectParameter("userId", typeof(int));

            paramList.Add(userIdParameters);

            var ret = base.GetTableFunction("public", "get_menutoolbar", paramList);
            var result = new List<Beens.Get_MenuToolbar_Result>();

            foreach(var record in ret)
            {
                result.Add(new Beens.Get_MenuToolbar_Result
                {
                    ToolbarId = Utils.TryParseNullable(record[0].ToString()),
                    IdParentToolbar = Utils.TryParseNullable(record[1].ToString()),
                    ToolbarName = record[2].ToString(),
                    IconName = record[3].ToString()
                });
            }

            return result;
        }
    }
}