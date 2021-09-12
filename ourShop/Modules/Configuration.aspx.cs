using ourShop.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;

namespace ourShop.Modules
{
    public partial class Configuration : MainPage
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
                return 2;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindProductList();

                using (var dbo = new ourShopEntities())
                {
                    var pageTitle = dbo.Settings
                                .Where(s => s.Name == "PageTitle")
                                .FirstOrDefault();

                    if (pageTitle != null && pageTitle.ValueString.Length > 0)
                    {
                        PageTitle.Text = pageTitle.ValueString;
                    }
                }
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    using (var dbo = new ourShopEntities())
                    {
                        dbo.Database.ExecuteSqlCommand("call public.set_settings({0}, {1});", "PageTitle", PageTitle.Text);
                    }
                }
            }
            catch(Exception ex)
            {
                DbStoredProcedure.Instance().SaveLog(null, DbStoredProcedure.LogType.Error, "DBFunctionBase", Utils.GetExceptionMessage(ex));
            }
        }
    }
}