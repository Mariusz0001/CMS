
using System;
using System.Web;
using System.Web.UI;

namespace CMS
{
    public abstract class MainPage : Page
    {
        public abstract int IdModule { get; }
        public virtual bool LoginRequired 
        { 
            get 
            { 
                return true;
            }  
        }

        protected override void OnInit(EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude("Registration", ResolveUrl("~/Scripts/utils.js"));
            
            if (!this.IsPostBack)
            {
                if (LoginRequired && HttpContext.Current.Session["UserId"] == null)
                {
                    if (!HttpContext.Current.Request.Url.AbsolutePath.Contains("Login"))
                    {
                        HttpCookie cookie = new HttpCookie("returnUrl");
                        DateTime now = DateTime.Now;
                        
                        cookie.Value = HttpContext.Current.Request.Url.AbsolutePath;
                        cookie.Expires = now.AddMinutes(10); 
                        
                        Response.Cookies.Add(cookie);
                    }

                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        public int? IdFromURL
        {
            get
            {
                return Utils.TryParseNullableInt(getParameterFromURL("id"));
            }
        }

        public string CategoryFromURL
        {
            get
            {
                return getParameterFromURL("category");
            }
        }
        
        public void ShowToastMessage(string message)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toast", "ShowToast('"+ message + "')", true);
        }

        private string getParameterFromURL(string param)
        {
            try
            {
                string sUrl = HttpContext.Current.Request.Url.AbsoluteUri;

                var parameters = sUrl.Split('?');

                foreach (var par in parameters)
                {
                    if (par != null && par.Length > 0 && par.Contains(param + "="))
                    {
                        var urlValue = par.Replace(param + "=", "");

                        return urlValue;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}