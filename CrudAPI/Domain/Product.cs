using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace CrudAPI.Domain
{

    public class ProductItem : AMongoEntity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        [Range(0, Double.PositiveInfinity)]
        public double Price { get; set; } 
        public string ImgSrc { get; set; }        
    }
}