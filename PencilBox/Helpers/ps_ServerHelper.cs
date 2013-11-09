using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PencilBox.Helpers
{
    public static class ps_ServerHelper
    {
        public static string Domain = 
            System.Configuration.ConfigurationManager.AppSettings["Domain"];
    }
}