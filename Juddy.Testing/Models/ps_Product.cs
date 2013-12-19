using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using PbData.Entities;

namespace Juddy.Testing.Models
{
    public class ps_ProductPhoto
    {
        public Guid PhotoId { get; set; }
        public string PhotoPath { get; set; }
    }

    public class ps_ProductCreate
    {
        public pb_Product Product { get; set; }

        [Required]
        public string Tags { get; set; }

        public List<ps_ProductPhoto> ProductPhotos { get; set; }
    }
}