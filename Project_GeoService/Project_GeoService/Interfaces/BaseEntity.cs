using System;
using System.ComponentModel.DataAnnotations;

namespace Project_GeoService.Interfaces
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}