using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Security;
using PbData.Entities;
using PbData.Business;
using PencilBox.Models;
using PencilBox.Helpers;

namespace PencilBox.Controllers
{
    public class UploadController : Controller
    {
        bn_Photo bnPhoto = new bn_Photo();

        public ActionResult Save(IEnumerable<HttpPostedFileBase> attachments)
        {
            List<ps_ProductPhoto> photos = (List<ps_ProductPhoto>)Session["Photos"];

            // The Name of the Upload component is "attachments" 
            foreach (var file in attachments)
            {
                // Some browsers send file names with full path. This needs to be stripped.
                var fileName = Path.GetFileName(file.FileName);
                var serverName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var physicalPath = Path.Combine(Server.MapPath("~/PencilBox_Data/Images"), serverName);

                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);

                //add to db
                var photoId = bnPhoto.Create(
                    ps_Membership.GetUser().UserId,
                    null,
                    serverName);

                if (photoId != null)
                {
                    photos.Add(new ps_ProductPhoto
                    {
                        PhotoId = photoId,
                        PhotoPath = serverName
                    });
                }
            }

            Session["Photos"] = photos;
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Remove(string[] fileNames, Guid placeId)
        {
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Content/CoverImages"), fileName);

                // TODO: Verify user permissions
                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo

                    //System.IO.File.Delete(physicalPath);
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult UploadAvatar(IEnumerable<HttpPostedFileBase> avatar_attachment, Guid userId)
        {
            foreach (var file in avatar_attachment)
            {
                // Some browsers send file names with full path. This needs to be stripped.
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var serverPath = Path.Combine(Server.MapPath("~/PencilBox_Data/Avatars"), fileName);

                // The files are not actually saved in this demo
                file.SaveAs(serverPath);
                //ImageHelper.CropImage(tempPath, serverPath, 300, 300);

                ////change avatar image
                //db.bp_Profile_UpdateAvatar(
                //    userId,
                //    serverName);
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult TagIcon(IEnumerable<HttpPostedFileBase> attachments)
        {
            string iconPath = (string)Session["IconPath"];

            foreach (var file in attachments)
            {
                var fileName = Path.GetFileName(file.FileName);
                var serverName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var physicalPath = Path.Combine(Server.MapPath("~/PencilBox_Data/TagIcons"), serverName);

                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);

                Session["IconPath"] = serverName;
            }

            return Content("");
        }
    }
}
