using System.Web;
using System.Web.UI;

namespace ourShop
{
    public abstract class MainPage : Page
    {
        public abstract int IdModule { get; }


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