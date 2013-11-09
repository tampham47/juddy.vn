using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PbData.Entities
{
    public partial class pb_UserProfile
    {
        public string FullName
        {
            get
            {
                if (FirstName == null && LastName == null)
                    return UserName;
                else
                    return String.Format("{0} {1}", FirstName, LastName);
            }
        }
    }
}
