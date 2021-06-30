using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using ourShop.DataBase;
using Npgsql;
using NpgsqlTypes;

namespace ourShop.Tests
{
    [TestFixture]
    public class DBTest
    {
        [Test]
        public void LoginCheck()
        {
            try
            {
                for (var i = 0; i <= 10; i++)
                {
                    using (var dbo = new ourShopEntities())
                    {
                        var _password = new NpgsqlParameter("_password", NpgsqlDbType.Varchar);
                        _password.Direction = System.Data.ParameterDirection.Input;
                        _password.Value = "test";

                        var _username = new NpgsqlParameter("_username", NpgsqlDbType.Varchar);
                        _username.Direction = System.Data.ParameterDirection.Input;
                        _username.Value = "test";

                        var _email = new NpgsqlParameter("_email", NpgsqlDbType.Varchar);
                        _email.Direction = System.Data.ParameterDirection.Input;
                        _email.Value = "";

                        var _number = new NpgsqlParameter("_number", NpgsqlDbType.Varchar);
                        _number.Direction = System.Data.ParameterDirection.Input;
                        _number.Value = "";

                        var _IPv4 = new NpgsqlParameter("_IPv4", NpgsqlDbType.Varchar);
                        _IPv4.Direction = System.Data.ParameterDirection.Input;
                        _IPv4.Value = "127.0.0.1";

                        var ret = new NpgsqlParameter("_userId", NpgsqlDbType.Integer);
                        ret.Direction = System.Data.ParameterDirection.InputOutput;
                        ret.Value = -1;


                        dbo.Database.ExecuteSqlCommand("select public.get_usercanlogin(@_password, @_username, @_email, @_number, @_IPv4, @_userId);",
                           _password, _username, _email, _number, _IPv4, ret);

                        Assert.IsNotNull(ret);
                        if (ret != null)
                        {
                            var type = ret.Value.GetType();

                            Assert.IsNotNull(type);
                            if (type != null && type.IsArray)
                            {
                                var userId = ((object[])ret.Value)[0];
                                var message = ((object[])ret.Value)[1];
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        [Test]
        public void PermissionCheck()
        {
           Assert.IsTrue(DbFunction.Instance().IsUserHasPermission(1, "CHANGE_PAGE_SETTINGS"));
        }
    }
}