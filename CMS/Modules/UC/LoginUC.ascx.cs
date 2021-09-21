using CMS.DataBase;
using System;
using System.Linq;
using System.Web;

namespace CMS.Modules.UC
{
    public partial class LoginUC : System.Web.UI.UserControl
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var httpRequestBase = new HttpRequestWrapper(Request);
                var retLogin =  DataBase.DbStoredProcedure.Instance().LoginUser(Session, Password.Value.ToString(), UserName.Text, "", Utils.GetClientIpAddress(httpRequestBase));

                if (retLogin != null)
                {
                    if (retLogin.IsError)
                    {
                        ResultLabel.Text = retLogin.Message;
                        return;
                    }
                    else if (retLogin.Id > 0)
                    {
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
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "DBFunctionBase", Utils.GetExceptionMessage(ex));
                ResultLabel.Text = "Can not login";
            }
        }
    }
}