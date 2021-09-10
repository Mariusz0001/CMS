using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ourShop.Beens
{
    public class Get_Product_Result
    {
        public int? Id { get; set; }

        public int? IdCategoriesBook { get; set; }

        public int? IdProductsStatusBook { get; set; }

        public int? IdTaxPercentagesBook { get; set; }

        public string CategoryName { get; set; }

        public string StatusName { get; set; }

        public int? TaxValue { get; set; }

        public string Name { get; set; }

        public string Barcode { get; set; }

        public double? Price { get; set; }

        public int? Qty { get; set; }

        public string Description { get; set; }

        public bool? Enabled { get; set; }

        public string PicturePath { get; set; }

    }
}