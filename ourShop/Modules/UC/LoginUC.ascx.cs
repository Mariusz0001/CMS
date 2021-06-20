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
                var userId = Login();

                if (userId > 0)
                {
                    Session["UserId"] = userId;

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
                else
                {
                    ResultLabel.Text = "Wrong username or password.";
                }
            }
            catch(Exception ex)
            {
                ResultLabel.Text = "Can not login " + ex.Message;
            }
        }
        private int? Login()
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


                var _userId = new NpgsqlParameter("_userId", NpgsqlDbType.Integer);
                _userId.Direction = System.Data.ParameterDirection.InputOutput;
                _userId.Value = -1;


                dbo.Database.ExecuteSqlCommand("select public.get_usercanlogin(@_password, @_username, @_email, @_number, @_userId);", _password, _username, _email, _number, _userId);

                if (_userId != null && !String.IsNullOrEmpty(_userId.NpgsqlValue.ToString()) && int.Parse(_userId.NpgsqlValue.ToString()) > 0)
                    return int.Parse(_userId.NpgsqlValue.ToString());
                else
                    return null;
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