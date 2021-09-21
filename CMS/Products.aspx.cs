using CMS.DataBase;
using CMS.Modules.UC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS
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
            try
            {
                var ret = DbFunction.Instance().GetProductsList(CategoryFromURL);

                foreach (var item in ret)
                {
                    try
                    {
                        ProductCardUC myControl = (ProductCardUC)Page.LoadControl("~/Modules/UC/ProductCardUC.ascx");
                        myControl.LoadProduct(item.Id.Value, item.Name, item.Price.Value, item.Description, item.PicturePath);

                        UserControlHolder.Controls.Add(myControl);
                    }
                    catch (Exception ex)
                    {
                        DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "Products", Utils.GetExceptionMessage(ex));
                    }
                }
            }
            catch (Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "Products", Utils.GetExceptionMessage(ex));
            }
        }
    }
}