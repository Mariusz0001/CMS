﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ourShop.Modules.UC
{
    public partial class ProductCardUC : System.Web.UI.UserControl
    {
        private int ProductId;

        public ProductCardUC()
        {
        }
        public void LoadProduct(int ProductId, string Name, double Price, string Description, string ImageURL)
        {
            this.ProductId = ProductId;
            this.ProductCardTitle.Text = Name;
            this.ProductCardPrice.Text = Price.ToString();
            this.ProductCardImage.ImageUrl = ImageURL;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}