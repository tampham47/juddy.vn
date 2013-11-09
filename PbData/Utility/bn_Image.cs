using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PbData.Utility
{
    public class bn_Image
    {
        //scale image
        public static bool FixedWidth(string sourceImg, string desImg, int width)
        {
            if (!System.IO.File.Exists(sourceImg)) return false;

            var imageOriginal = Image.FromFile(sourceImg);          //Encoding source file into Image object
            double percent = (double)imageOriginal.Width / width;   //Calculate scale percentage
            int destWidth = (int)(imageOriginal.Width / percent);
            int destHeight = (int)(imageOriginal.Height / percent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            try
            {
                //Some scale options
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(imageOriginal, 0, 0, destWidth, destHeight);
                ImageHelper.SaveJpeg(desImg, b); //Compress JPEG image
                return true;
            }
            finally
            {
                g.Dispose();
            }
        }

        public static bool FixedHeight(string sourceImg, string desImg, int height)
        {
            if (!System.IO.File.Exists(sourceImg)) return false;        //Encoding source file into Image object
            var imageOriginal = Image.FromFile(sourceImg);
            double percent = (double)imageOriginal.Height / height;     //Calculate scale percentage
            int destWidth = (int)(imageOriginal.Width / percent);
            int destHeight = (int)(imageOriginal.Height / percent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            try
            {
                //Some scale options
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(imageOriginal, 0, 0, destWidth, destHeight);
                ImageHelper.SaveJpeg(desImg, b); //Compress JPEG image
                return true;
            }
            finally
            {
                g.Dispose();
            }
        }

        //return a bitmap
        public static bool ScaleImage(string sourceImg, float ratio)
        {
            if (!System.IO.File.Exists(sourceImg)) return false;        //Encoding source file into Image object
            var imageOriginal = Image.FromFile(sourceImg);

            int destWidth = (int)(imageOriginal.Width / ratio);
            int destHeight = (int)(imageOriginal.Height / ratio);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            try
            {
                //Some scale options
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(imageOriginal, 0, 0, destWidth, destHeight);
                ((Image)b).Save(sourceImg);
                return true;
            }
            finally
            {
                g.Dispose();
            }
            return true;
        }
    }
}
