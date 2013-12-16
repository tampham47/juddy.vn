using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PbData.Entities;
using PbData.Business;

namespace Juddy.Services.Models
{
    public class sv_Photo
    {
        private string serverPath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];

        public sv_Photo(pb_Photo data)
        {
            PhotoId = data.PhotoId;
            ImagePath = serverPath + data.ImagePath;
            ImageH1 = serverPath + data.ImageFixH1();
            ImageW1 = serverPath + data.ImageFixW1();
        }

        public System.Guid PhotoId { get; set; }
        public string ImagePath { get; set; }
        public string ImageW1 { get; set; }
        public string ImageH1 { get; set; }
    }
}