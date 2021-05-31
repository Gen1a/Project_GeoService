using System;
using System.ComponentModel.DataAnnotations;

namespace Project_GeoService.Interfaces
{
    public abstract class BaseEntity
    {
        [Key]
        [Range(1, long.MaxValue, ErrorMessage = "{0} must be greater than {1}.")]
        public long Id { get; set; }
    }
}