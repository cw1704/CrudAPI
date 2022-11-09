using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrudAPI.Domain
{
    public class Order
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public IList<OrderItem> Items { get; set; }

        [Required]

        [Range(0, Double.PositiveInfinity)]
        public double TotalPrice { get; set; } = 0.0;
        public string MemberId { get; set; } = "";
        public string PromotionId { get; set; } = "";
    }

    public class OrderItem
    { 
        public string ProductId { get; set; }
        public IList<string> OptionSelected { get; set; }
    }
}