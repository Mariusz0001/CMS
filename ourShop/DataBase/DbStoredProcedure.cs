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
        public static DbStoredProcedure _object;

        public DbStoredProcedure() { }

        public enum LogType
        {
            Error = 1,
            Warning = 2,
            Information = 3
        }

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

        public Beens.Result_Been AddLog(int? idUser, LogType logType, String module, String description)
        {
            try
            {
                using (var dbo = new ourShopEntities())
                {
                    dbo.Database.ExecuteSqlCommand("call public.add_log({0}, {1}, {2}, {3}, {4});", idUser, (int)logType, Utils.GetServerIPAddress(), module, description);
                    return new Beens.Result_Been { IsError = false, Message = "Added log successfully." };
                }
            }
            catch(Exception ex)
            {
                return new Beens.Result_Been { IsError = true, Message = ex.Message };
            }
        }

        public void SaveLog(int? idUser, LogType logType, String module, String description)
        {
            try
            {
                using (var dbo = new ourShopEntities())
                {
                    dbo.Database.ExecuteSqlCommand("call public.add_log({0}, {1}, {2}, {3}, {4});", idUser, (int)logType, Utils.GetServerIPAddress(), module, description);
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Beens.Result_Been SetProduct(int? id, string name, bool isEnabled, string barcode, double price, int idTaxPercentagesBook, int quantity, int idCategoriesBook, string description, int userId)
        {
            if(id == null || id == 0)
            {
                id = -1;
            }

            using (var dbo = new ourShopEntities())
            {
                var _name = CreateNpgsqlParameter("_name", NpgsqlDbType.Varchar, name);
                var _isEnabled = CreateNpgsqlParameter("_isEnabled", NpgsqlDbType.Boolean, isEnabled);
                var _barcode = CreateNpgsqlParameter("_barcode", NpgsqlDbType.Varchar, barcode);
                var _price = CreateNpgsqlParameter("_price", NpgsqlDbType.Double, price);
                var _idtaxpercentagesbook = CreateNpgsqlParameter("_idtaxpercentagesbook", NpgsqlDbType.Integer, idTaxPercentagesBook);
                var _quantity = CreateNpgsqlParameter("_quantity", NpgsqlDbType.Integer, quantity);
                var _idcategoriesbook = CreateNpgsqlParameter("_idcategoriesbook", NpgsqlDbType.Integer, idCategoriesBook);
                var _description = CreateNpgsqlParameter("_description", NpgsqlDbType.Varchar, description);
                var _userId = CreateNpgsqlParameter("_userId", NpgsqlDbType.Integer, userId);

                var ret = CreateNpgsqlParameter("_id", NpgsqlDbType.Integer, id, System.Data.ParameterDirection.InputOutput);

                dbo.Database.ExecuteSqlCommand("call public.set_product(@_name, @_isEnabled, @_barcode, @_price, @_idtaxpercentagesbook, @_quantity, @_idcategoriesbook, @_description, @_userId, @_id);",
                           _name, _isEnabled, _barcode, _price, _idtaxpercentagesbook, _quantity, _idcategoriesbook, _description, _userId, ret);

                if (ret != null)
                {
                    int productId = Utils.TryParseNullableInt(ret.Value.ToString()).Value;

                    if (productId > 0)
                    {
                        return new Beens.Result_Been { Id = productId, IsError = false };
                    }
                    else
                    {
                        return new Beens.Result_Been { Id = productId, IsError = true, Message = "Error occured with executing stored procedure." };
                    }
                }
                else
                {
                    return new Beens.Result_Been { Id = null, IsError = true, Message = "Can not fetch data. Check connection." };
                }
            }
        }

        public Beens.Result_Been SetProductPicture(int? id, int? idProduct, string fileName, string path, bool? isEnabled, int? orderNumber, int userId)
        {
            using (var dbo = new ourShopEntities())
            {
                var _idproduct = CreateNpgsqlParameter("_idproduct", NpgsqlDbType.Integer, idProduct);
                var _filename = CreateNpgsqlParameter("_filename", NpgsqlDbType.Varchar, fileName);
                var _isEnabled = CreateNpgsqlParameter("_isEnabled", NpgsqlDbType.Boolean, isEnabled);
                var _path = CreateNpgsqlParameter("_path", NpgsqlDbType.Varchar, path);
                var _userId = CreateNpgsqlParameter("_userId", NpgsqlDbType.Integer, userId);
                var _ordernumber = CreateNpgsqlParameter("_ordernumber", NpgsqlDbType.Integer, orderNumber);
                var ret = CreateNpgsqlParameter("_id", NpgsqlDbType.Integer, id, System.Data.ParameterDirection.InputOutput);

                dbo.Database.ExecuteSqlCommand("call public.set_productpicture(@_idproduct, @_filename, @_isenabled, @_path, @_userid, @_ordernumber, @_id);",
                           _idproduct, _filename, _isEnabled,_path, _userId, _ordernumber, ret);

                if (ret != null)
                {
                    int pictureId = Utils.TryParseNullableInt(ret.Value.ToString()).Value;

                    if (pictureId > 0)
                    {
                        return new Beens.Result_Been { Id = pictureId, IsError = false };
                    }
                    else
                    {
                        return new Beens.Result_Been { Id = pictureId, IsError = true, Message = "Error occured with executing stored procedure." };
                    }
                }
                else
                {
                    return new Beens.Result_Been { Id = null, IsError = true, Message = "Can not fetch data. Check connection." };
                }
            }
        }

        /*

        public Beens.Result_Been AddProductPicture(int? idProduct, string fileName, string path, bool? isEnabled, int? orderNumber,  int userId)
        {
            using (var dbo = new ourShopEntities())
            {
                var _filename = CreateNpgsqlParameter("_filename", NpgsqlDbType.Varchar, fileName);

                var _path = CreateNpgsqlParameter("_path", NpgsqlDbType.Varchar, path);
                var _userId = CreateNpgsqlParameter("_userId", NpgsqlDbType.Integer, userId);
                var _isEnabled = CreateNpgsqlParameter("_isEnabled", NpgsqlDbType.Boolean, isEnabled);
                var _idproduct = CreateNpgsqlParameter("_idproduct", NpgsqlDbType.Integer, idProduct);
                var _ordernumber = CreateNpgsqlParameter("_ordernumber", NpgsqlDbType.Integer, orderNumber);
                var ret = CreateNpgsqlParameter("_id", NpgsqlDbType.Integer, -1, System.Data.ParameterDirection.InputOutput);

                dbo.Database.ExecuteSqlCommand("call public.add_productspicture(@_filename, @_path, @_userid, @_isenabled, @_idproduct, @_ordernumber, @_id);",
                           _filename, _path, _userId, _isEnabled, _idproduct, _ordernumber, ret);

                if (ret != null)
                {
                    int pictureId = Utils.TryParseNullableInt(ret.Value.ToString()).Value;

                    if (pictureId > 0)
                    {
                        return new Beens.Result_Been { Id = pictureId, IsError = false };
                    }
                    else
                    {
                        return new Beens.Result_Been { Id = pictureId, IsError = true, Message = "Error occured with executing stored procedure." };
                    }
                }
                else
                {
                    return new Beens.Result_Been { Id = null, IsError = true, Message = "Can not fetch data. Check connection." };
                }
            }
        }*/

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