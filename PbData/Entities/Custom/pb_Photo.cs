using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PbData.Utility;
using PbData.Business;

namespace PbData.Entities
{
    public partial class pb_Photo
    {
        private string serverPath = AppDomain.CurrentDomain.BaseDirectory + @"PencilBox_Data\Images\";

        //with = 230
        public string ImageFixW1()
        {
            if (ImageW1 != null)
            {
                return ImageW1;
            }
            else
            {
                try
                {
                    var desImage = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath);
                    var result = bn_Image.FixedWidth(
                        serverPath + ImagePath,
                        serverPath + desImage,
                        230);

                    if (result)
                    {
                        bn_Photo bnPhoto = new bn_Photo();
                        bnPhoto.UpdateImageW1(PhotoId, desImage);
                        return desImage;
                    }
                    else
                        return ImagePath;
                }
                catch (Exception ex)
                {
                    return ImagePath;
                }
            }
        }

        //height = 400
        public string ImageFixH1()
        {
            if (ImageH1 != null)
            {
                return ImageH1;
            }
            else
            {
                try
                {
                    var desImage = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath);
                    var result = bn_Image.FixedHeight(
                        serverPath + ImagePath,
                        serverPath + desImage,
                        400);

                    if (result)
                    {
                        bn_Photo bnPhoto = new bn_Photo();
                        bnPhoto.UpdateImageH1(PhotoId, desImage);
                        return desImage;
                    }
                    else
                        return ImagePath;
                }
                catch (Exception ex)
                {
                    return ImagePath;
                }
            }
        }

        public string ImageFixH2()
        {
            return ImageH2;
        }
    }
}
