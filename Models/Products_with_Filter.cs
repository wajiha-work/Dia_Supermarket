using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dia_Supermarket.Models
{
    public class Products_with_Filter
    {
        public PagedList.IPagedList<tb_Products> ProductsList { get; set; }
        // for paginated products list

        public tb_Products ProductForFilter { get; set; }
        // for applying filters
    }
}