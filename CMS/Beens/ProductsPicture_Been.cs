using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Beens
{
    public class ProductsPicture_Been
    {
        public int Id { get; set; }
        public int? LocalListId { get; set; }
        public int? IdProduct { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }

        public int? OrderNumber { get; set; }

        public bool IsEnabled { get; set; }
    }
}