using ourShop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ourShop
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        private void LoadUserProperties()
        {
            using (var dbo = new ourShopEntities())
            {
          /*      var user = dbo.Us
                           .Where(s => s.Enabled == true && (s.IdCategoriesBook_Parent == null || s.IdCategoriesBook_Parent == 0));
                if (categories != null)
                {
                    var categoriesList = categories.ToList();
                    this.PopulateCategoriesTreeView(categoriesList, 0, null);
                }*/
            }
        }
    }
}