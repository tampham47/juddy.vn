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
    
    public partial class pb_ContactType
    {
        public pb_ContactType()
        {
            this.pb_UserContact = new HashSet<pb_UserContact>();
        }
    
        public int ContactTypeId { get; set; }
        public string Icon { get; set; }
        public string TypeName { get; set; }
        public byte Order { get; set; }
    
        public virtual ICollection<pb_UserContact> pb_UserContact { get; set; }
    }
}
