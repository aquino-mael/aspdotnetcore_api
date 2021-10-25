using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
  public class UfEntity : BaseEntity
  {
    [Required]
    [MaxLength(2)]
    public string Initials { get; set; }
    [Required]
    [MaxLength(45)]
    public string Name { get; set; }
    public IEnumerable<CountyEntity> Counties { get; set; }
  }
}
