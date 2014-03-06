//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PbData.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class pb_Product
    {
        public pb_Product()
        {
            this.pb_Comment = new HashSet<pb_Comment>();
            this.pb_Photo = new HashSet<pb_Photo>();
            this.pb_ProductTag = new HashSet<pb_ProductTag>();
            this.pb_Order = new HashSet<pb_Order>();
        }
    
        public System.Guid ProductId { get; set; }
        public System.Guid UserId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string HumanCode { get; set; }
        public int Status { get; set; }
        public long Views { get; set; }
        public int Amount { get; set; }
    
        public virtual ICollection<pb_Comment> pb_Comment { get; set; }
        public virtual ICollection<pb_Photo> pb_Photo { get; set; }
        public virtual ICollection<pb_ProductTag> pb_ProductTag { get; set; }
        public virtual ICollection<pb_Order> pb_Order { get; set; }
        public virtual pb_UserProfile pb_UserProfile { get; set; }
    }
}
