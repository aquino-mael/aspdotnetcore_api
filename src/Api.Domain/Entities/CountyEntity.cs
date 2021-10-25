using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
  public class CountyEntity : BaseEntity
  {
    [Required]
    [MaxLength(60)]
    public string Name { get; set; }
    public int IBGECode { get; set; }
    [Required]
    public Guid UfId { get; set; }
    public UfEntity Uf { get; set; }
    public IEnumerable<ZipCodeEntity> ZipCodes { get; set; }
  }
}
