using Npgsql;
using NpgsqlTypes;
using System;

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
    }
}