using System.ComponentModel.DataAnnotations;

namespace GpProject206.Domain
{

    public class Product : AMongoEntity
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        [Range(0, Double.PositiveInfinity)]
        public double Price { get; set; } 
        public string ImgSrc { get; set; }        
    }
}