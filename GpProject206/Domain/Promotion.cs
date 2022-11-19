using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;

namespace GpProject206.Domain
{
    public class Promotion : AMongoEntity
    {
        public string Description { get; set; }
        public string Code { get; set; }
        [Range(-1, Int64.MaxValue)]
        public int CountLimit { get; set; }                 // -1 = unlimited

        [Range(0, Int64.MaxValue)]
        public double PercentageDiscount { get; set; } = 0; // 0 = disabled
        [Range(0, Int64.MaxValue)]
        public double DirectDeduction { get; set; } = 0;    // 0 = disabled
        public bool IsEnded { get; private set; } = false;
        public IList<string> AppliedOrders { get; private set; } = new List<string>();
        public void AddAppliedOrders(string id)
        {
            AppliedOrders.Add(id);
            if (AppliedOrders.Count == CountLimit - 1)
                MarkEnded();
        }

        public void MarkEnded() => IsEnded = true;
    }
}