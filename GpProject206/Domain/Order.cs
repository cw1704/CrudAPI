using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GpProject206.Domain
{
    public class Order : AMongoEntity
    {
        public IList<OrderItem> Items { get; set; } = new List<OrderItem>();
        [Range(0, Double.PositiveInfinity)]
        public double TotalPrice { get; set; } = 0.0;
        public string MemberId { get; set; } = "";
        public string PromotionCode { get; set; } = "";
        public string Address { get; set; } = "";
        public string ContactNumber { get; set; } = "";
    }

    public class OrderItem
    { 
        public string ProductId { get; set; }
        public int Qty { get; set; }
    }
}