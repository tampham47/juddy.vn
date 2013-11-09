using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PbData.Entities;

namespace PbData.Business
{
    public class bn_Order
    {
        private pb_Entities db = new pb_Entities();

        public bn_Order(pb_Entities connection = null)
        {
            if (connection != null)
            {
                db = connection;
            }
        }
        public int UpdateStatus(Guid orderId, byte status)
        {
            var re = (int)db.pb_Order_UpdateStatus(
                orderId,
                status).Single();

            return re;
        }

        public Guid Create(Guid productId, Guid? userId, string customerName, string customerPhone, double price, byte amount, string address, string comment)
        {
            var orderId = Guid.NewGuid();
            var re = (int)db.pb_Order_Create(
                orderId,
                productId,
                userId,
                customerName,
                customerPhone,
                price,
                amount,
                address,
                comment).Single();

            if (re >= 0)
                return orderId;
            else
                return Guid.Empty;
        }
        public List<pb_Order> GetBySalesman(Guid userId, byte status)
        {
            var result = db.pb_Order_GetbySalesman(
                userId,
                status).ToList();

            return result;
        }
        public List<pb_Order> GetByUserId(Guid userId)
        {
            var result = db.pb_Order_GetbyUserId(
                userId).ToList();

            return result;
        }

        //Filter 
        public List<pb_Order> FilterByUserName(Guid userId, DateTime fromDate, DateTime toDate, string request)
        {
            return new List<pb_Order>();
        }
        public List<pb_Order> FilterByHumanCode(Guid userId, DateTime fromDate, DateTime toDate, string request)
        {
            return new List<pb_Order>();
        }
        public List<pb_Order> FilterByOrderCode(Guid userId, DateTime fromDate, DateTime toDate, string request)
        {
            return new List<pb_Order>();
        }
    }

    public static class EOrder_Status
    {
        public const byte Available = 0;
    }
}
