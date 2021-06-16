using ourShop.DataBase;
using System;
using System.Linq;
using System.Web.Services;

namespace ourShop.Modules
{
    public partial class Configuration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
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
                Console.WriteLine(ex.Message);
            }
        }
    }
}