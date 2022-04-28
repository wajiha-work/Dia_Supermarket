using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dia_Supermarket.Models
{
    public class Cart_Item
    {
        public int product_id { get; set; }

        public string product_name { get; set; }

        public string product_image { get; set; }

        public decimal? price { get; set; }

        public int quantity { get; set; }
    }
}