//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dia_Supermarket.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_Wishlist
    {
        public int wish_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> product_id { get; set; }
    
        public virtual tb_Products tb_Products { get; set; }
        public virtual tb_Users tb_Users { get; set; }
    }
}
