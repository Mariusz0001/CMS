using System;
using System.Web;
using System.Web.UI;

namespace ourShop
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
            if (!this.IsPostBack)
            {
                if (LoginRequired && HttpContext.Current.Session["UserId"] == null)
                {
                    if (!HttpContext.Current.Request.Url.AbsolutePath.Contains("Login"))
                    {
                        if (Request.Cookies["returnUrl"] != null)
                            Request.Cookies["returnUrl"].Value = HttpContext.Current.Request.Url.AbsolutePath;
                        else
                            Request.Cookies.Add(new HttpCookie("returnUrl", HttpContext.Current.Request.Url.AbsolutePath));
                    }

                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        public int? IdFromURL
        {
            get
            {
                try
                {
                    string sUrl = HttpContext.Current.Request.Url.AbsoluteUri;

                    var parameters = sUrl.Split('?');

                    foreach (var par in parameters)
                    {
                        if (par != null && par.Length > 0 && par.Contains("id="))
                        {
                            var urlId = par.Replace("id=", "");

                            return int.Parse(urlId);
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
}