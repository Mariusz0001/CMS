using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ourShop.DataBase
{
    public class DbStoredProcedure
    {
        private static DbStoredProcedure _object;

        private DbStoredProcedure() { }

        public static DbStoredProcedure Instance()
        {
            if (_object == null)
            {
                _object = new DbStoredProcedure();
            }
            return _object;
        }

        public Beens.Result_Been LoginUser(HttpSessionState session, string password, string userName, string userNumber, string IPv4)
        {
            using (var dbo = new ourShopEntities())
            { 
                var _password = new NpgsqlParameter("_password", NpgsqlDbType.Varchar);
                _password.Direction = System.Data.ParameterDirection.Input;
                _password.Value = password;

                var _username = new NpgsqlParameter("_username", NpgsqlDbType.Varchar);
                _username.Direction = System.Data.ParameterDirection.Input;
                _username.Value = userName;


                var _email = new NpgsqlParameter("_email", NpgsqlDbType.Varchar);
                _email.Direction = System.Data.ParameterDirection.Input;


                if (Utils.IsValidEmail(userName))
                {
                    _username.Value = "";
                    _email.Value = userName;
                }
                else
                {
                    _email.Value = "";
                }


                var _number = new NpgsqlParameter("_number", NpgsqlDbType.Varchar);
                _number.Direction = System.Data.ParameterDirection.Input;
                _number.Value = userNumber;


                var _IPv4 = new NpgsqlParameter("_IPv4", NpgsqlDbType.Varchar);
                _IPv4.Direction = System.Data.ParameterDirection.Input;
                _IPv4.Value = IPv4;

                var ret = new NpgsqlParameter("_userId", NpgsqlDbType.Integer);
                ret.Direction = System.Data.ParameterDirection.InputOutput;
                ret.Value = -1;

                dbo.Database.ExecuteSqlCommand("select public.get_usercanlogin(@_password, @_username, @_email, @_number, @_IPv4, @_userId);",
                           _password, _username, _email, _number, _IPv4, ret);

                if (ret != null)
                {
                    var type = ret.Value.GetType();

                    if (type != null && type.IsArray)
                    {
                        int userId = int.Parse(((object[])ret.Value)[0].ToString());
                        string message = ((object[])ret.Value)[1].ToString();

                        if (userId > 0)
                        {
                            this.SetUserSessionProperties(session, userId);
                            return new Beens.Result_Been { Id = userId, IsError = false, Message = message };
                        }
                        else
                        {
                            return new Beens.Result_Been { Id = userId, IsError = true, Message = message };
                        }
                    }
                    else
                    {
                        return new Beens.Result_Been { Id = null, IsError = true, Message = "Can not fetch data. Check connection." };
                    }
                }
                else
                {
                    return new Beens.Result_Been { Id = null, IsError = true, Message = "Can not fetch data. Check connection." };
                }
            }
        }

        public Boolean SetUserSessionProperties(HttpSessionState Session, int userId)
        {
            if (Session != null && userId > 0)
            {
                using (var dbo = new ourShopEntities())
                {
                    var user = dbo.Users.Where(s => s.Id == userId).FirstOrDefault();
                    if (user != null)
                    {
                        Session["UserId"] = user.Id;
                        Session["FirstName"] = user.FirstName;
                        Session["LastName"] = user.LastName;
                        Session["Name"] = user.Name;
                        Session["Number"] = user.Number;
                        Session["Position"] = user.Position;
                        Session["Phone"] = user.Phone;
                        Session["Email"] = user.Email;
                        Session["IdLanguageBook"] = user.IdLanguageBook;

                        return true;
                    }
                }
            }
            return false;
        }
    }
}