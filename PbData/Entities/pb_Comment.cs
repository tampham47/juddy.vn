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
    
    public partial class pb_Comment
    {
        public System.Guid CommentId { get; set; }
        public System.Guid ProductId { get; set; }
        public System.Guid UserId { get; set; }
        public string Comment { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual pb_UserProfile pb_UserProfile { get; set; }
        public virtual pb_Product pb_Product { get; set; }
    }
}
