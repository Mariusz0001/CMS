using System;
using System.Web.UI.WebControls;
using CMS.DataBase;
using System.Reflection;
using System.Web.UI;
using System.Web;
using System.Web.UI.HtmlControls;

namespace CMS
{
    public abstract class EditForm : Page
    {
        public abstract object GetData();
        public virtual void _Page_Load(object sender, EventArgs e)
        {

        }

        public int? IdFromURL
        {
            get
            {
                return Utils.TryParseNullableInt(Utils.getParameterFromURL("id"));
            }
        }

        public void ShowToastMessage(string message)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toast", "ShowToast('" + message + "')", true);
        }
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindableForm.BindProperties(GetData(), this.Form.Controls);
            }

            _Page_Load(sender, e);
        }
    }
}