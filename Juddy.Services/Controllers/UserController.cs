using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PbData.Business;
using PbData.Entities;

namespace Juddy.Services.Controllers
{
    public class UserController : ApiController
    {
        public List<pb_UserProfile> GetAll()
        {
            bn_UserProfile bnUser = new bn_UserProfile(isLazy: false);
            return bnUser.GetAll();
        }

        public List<pb_UserProfile> Filter()
        {
            bn_UserProfile bnUser = new bn_UserProfile(isLazy: false);
            return bnUser.GetAll();
        }
    }
}
