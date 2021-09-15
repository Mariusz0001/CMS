using System;
using System.Web.UI.WebControls;
using ourShop.DataBase;
using System.Reflection;
using System.Web.UI;
using System.Web;

namespace ourShop
{
    public abstract class EditFormUC : System.Web.UI.UserControl
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
                BindableForm.BindProperties(GetData(), this.Controls);
            }

            _Page_Load(sender, e);
        }
    }
}