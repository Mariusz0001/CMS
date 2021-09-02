using ourShop.DataBase;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            Page.ClientScript.RegisterClientScriptInclude("Registration", ResolveUrl("~/Scripts/utils.js"));
            
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

        public virtual void BindPoperties(object Been)
        {
            /*
             * 
             * to do
             * 
             * bindowanie tax
             * category
             * description
             * pictures
             * */

            try
            {
                foreach (var propertyInfo in Been.GetType().GetProperties())
                {
                    foreach (Control ctrl in this.Form.Controls)
                    {
                        if (ctrl is ContentPlaceHolder)
                        {
                            ContentPlaceHolder chp = ((System.Web.UI.WebControls.ContentPlaceHolder)ctrl);

                            foreach (Control controll in chp.Controls)
                            {
                                    if (controll.ID == propertyInfo.Name)
                                    {
                                        try
                                        {
                                            if (controll is TextBox)
                                            {
                                                TextBox tb = ((TextBox)controll);

                                                tb.Text = propertyInfo.GetValue(Been, null).ToString();
                                            }
                                            else if (controll is Label)
                                            {
                                                Label lb = ((Label)controll);

                                                lb.Text = propertyInfo.GetValue(Been, null).ToString();
                                            }
                                            else if (controll is System.Web.UI.HtmlControls.HtmlInputGenericControl)
                                            {
                                                System.Web.UI.HtmlControls.HtmlInputGenericControl cb = ((System.Web.UI.HtmlControls.HtmlInputGenericControl)controll);

                                                cb.Value = propertyInfo.GetValue(Been, null).ToString();
                                            }
                                            else if (controll is System.Web.UI.HtmlControls.HtmlInputCheckBox)
                                            {
                                                System.Web.UI.HtmlControls.HtmlInputCheckBox cb = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)controll);

                                                if (Utils.TryParseNullableBoolean(propertyInfo.GetValue(Been, null).ToString()).Value)
                                                    cb.Checked = true;
                                                else
                                                    cb.Checked = false;
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "MainPage.BindPoperties " + Been.ToString(), Utils.GetExceptionMessage(ex));
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