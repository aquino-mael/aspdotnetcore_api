using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
  public class ZipCodeEntity
  {
    [Required]
    [MaxLength(10)]
    public string ZipCode { get; set; }
    [Required]
    [MaxLength(60)]
    public string Street { get; set; }
    [MaxLength(10)]
    public string Number { get; set; }

    [Required]
    public Guid CountyId { get; set; }
    public CountyEntity County { get; set; }
  }
}
