using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ourShop
{
    public partial class About : MainPage
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

        }
    }
}