﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Juddy.Services.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public string Index()
        {
            return "connected";
        }
    }
}
