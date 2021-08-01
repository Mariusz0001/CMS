using ourShop.Modules.UC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ourShop
{
    public partial class Products : MainPage
    {
        public override int IdModule
        {
            get
            {
                return 6;
            }
        }
        public override bool LoginRequired
        {
            get
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            ProductCardUC myControl = (ProductCardUC)Page.LoadControl("~/Modules/UC/ProductCardUC.ascx");

            UserControlHolder.Controls.Add(myControl);

            ProductCardUC myControl2 = (ProductCardUC)Page.LoadControl("~/Modules/UC/ProductCardUC.ascx");

            UserControlHolder.Controls.Add(myControl2);

            /*string category = CategoryFromURL;

            if (category?.Length > 0)
            {

            }*/
        }
    }
}