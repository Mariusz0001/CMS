using ourShop.DataBase;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ourShop.Modules
{
    public partial class ProductsConfiguration : MainPage
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

        protected void ProductGrid_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                int rowIndex = Int32.Parse(e.CommandArgument.ToString());
                
                if (rowIndex > -1)
                {
                    GridView grid = sender as GridView;
                    string response = "<script>window.open('/Modules/EditForm/ProductMngm.aspx?id=" + grid.DataKeys[rowIndex].Value.ToString() + "' ,'Product', 'directories = 0,titlebar = 0,toolbar = 0,location = 0,status = 0,menubar = 0,scrollbars = no,resizable = no,width = 1000,height = 800');</script>";
                    Response.Write(response);
                }
                        
            }
        }

        protected void ProductGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
    }
}
 