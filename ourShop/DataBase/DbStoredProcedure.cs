using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ourShop.DataBase
{
    public class DbStoredProcedure : DBBase
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
                var _password = CreateNpgsqlParameter("_password", NpgsqlDbType.Varchar, password);
                var _username = CreateNpgsqlParameter("_username", NpgsqlDbType.Varchar, userName);
                var _email = CreateNpgsqlParameter("_email", NpgsqlDbType.Varchar, null);
                var _number = CreateNpgsqlParameter("_number", NpgsqlDbType.Varchar, userNumber);
                var _IPv4 = CreateNpgsqlParameter("_IPv4", NpgsqlDbType.Varchar, IPv4);
                var ret = CreateNpgsqlParameter("_userId", NpgsqlDbType.Integer, -1, System.Data.ParameterDirection.InputOutput);

                if (Utils.IsValidEmail(userName))
                {
                    _username.Value = "";
                    _email.Value = userName;
                }
                else
                {
                    _email.Value = "";
                }
                
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
        public void SaveLog(int? idUser, int? idLogsType, String hostname, String module, String description)
        {
            try
            {
                using (var dbo = new ourShopEntities())
                {
                    dbo.Database.ExecuteSqlCommand("call public.add_log({0}, {1}, {2}, {3}, {4});", idUser, idLogsType,hostname, module, description);
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    /*    public Boolean SetSettings()
        {
            try
            {
                using (var dbo = new ourShopEntities())
                {
                    dbo.Database.ExecuteSqlCommand("call public.set_settings({0}, {1});", "PageTitle", PageTitle.Text);
                }
            }

    catch
            }*/
    }
}