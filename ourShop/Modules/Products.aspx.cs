using ourShop.DataBase;
using System;
using System.Data;

namespace ourShop.Modules
{
    public partial class Products : MainPage
    {
        public override bool LoginRequired
        {
            get
            {
                return true;
            }
        }
        public override int IdModule
        {
            get
            {
                return 7;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindProductList();
            }
        }

        private void BindProductList()
        {
            var ret = DbFunction.Instance().GetProductsConfigurationList(SessionProperties.GetUserId(Session).Value);

            if (ret != null)
            {
                DataTable dt = Utils.CreateDataTable<Beens.Get_Product_Result>(ret);
                ProductGrid.DataSource = dt;
                ProductGrid.DataBind();
            }
        }

        protected void EditButton_Click(object sender, System.EventArgs e)
        {

        }

        protected void DeleteButton_Click(object sender, System.EventArgs e)
        {

        }
    }
}
 