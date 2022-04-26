using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dia_Supermarket.Models
{
    public class TopSellerProducts
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_image { get; set; }
        public decimal? price { get; set; }
        public int? stock { get; set; }
        public string cat_name { get; set; }
        public int? sales { get; set; }
    }
}