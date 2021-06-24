using Npgsql;
using NpgsqlTypes;
using ourShop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ourShop.Modules.UC
{
    public partial class LoginUC : System.Web.UI.UserControl
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var retLogin = Login();

                if (retLogin != null)
                {
                    if (retLogin.IsError)
                    {
                        ResultLabel.Text = retLogin.Message;
                        return;
                    }
                    else if (retLogin.Id > 0)
                    {
                        LoadUserProperties(retLogin.Id.Value);

                        HttpCookie returnCookie = Request.Cookies["returnUrl"];

                        if (returnCookie != null && !string.IsNullOrEmpty(returnCookie.Value) && !returnCookie.Value.Contains("Login"))
                        {
                            Response.Redirect(returnCookie.Value);
                            Response.Cookies.Remove("returnUrl");
                        }
                        else
                        {
                            Response.Redirect("/Home");
                        }
                    }
                }
                else
                {
                    ResultLabel.Text = "Wrong username or password";
                    return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ResultLabel.Text = "Can not login - check logs messages";
            }
        }
        private Beens.Result_Been Login()
        {
            using (var dbo = new ourShopEntities())
            {
                var _password = new NpgsqlParameter("_password", NpgsqlDbType.Varchar);
                _password.Direction = System.Data.ParameterDirection.Input;
                _password.Value = Password.Value.ToString();

                var _username = new NpgsqlParameter("_username", NpgsqlDbType.Varchar);
                _username.Direction = System.Data.ParameterDirection.Input;
                _username.Value = UserName.Text;

               
                var _email = new NpgsqlParameter("_email", NpgsqlDbType.Varchar);
                _email.Direction = System.Data.ParameterDirection.Input;


                if (IsValidEmail(UserName.Text))
                {
                    _username.Value = "";
                    _email.Value = UserName.Text;
                }
                else
                {
                    _email.Value = "";
                }


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

                if (ret != null)
                {
                    var type = ret.Value.GetType();

                    if (type != null && type.IsArray)
                    {
                        if (int.Parse(((object[])ret.Value)[0].ToString()) > 0)
                        {
                            return new Beens.Result_Been { Id = int.Parse(((object[])ret.Value)[0].ToString()), IsError = false, Message = ((object[])ret.Value)[1].ToString() };
                        }
                        else
                        {
                            return new Beens.Result_Been { Id = int.Parse(((object[])ret.Value)[0].ToString()), IsError = true, Message = ((object[])ret.Value)[1].ToString() };
                        }
                    }
                    else
                    {
                        return new Beens.Result_Been { Id = null, IsError =true, Message = "Can not fetch data. Check connection."  };
                    }
                }
                else
                {
                    return new Beens.Result_Been { Id = null, IsError = true, Message = "Can not fetch data. Check connection." };
                }
            }
        }
        private void LoadUserProperties(int userId)
        {
            try
            {
                if (userId > 0)
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}