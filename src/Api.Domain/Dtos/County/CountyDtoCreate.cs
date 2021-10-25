using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.County
{
  public class CountyDtoCreate
  {
    [Required(ErrorMessage = "County name is required.")]
    [StringLength(60, ErrorMessage = "County name can only contain {1} caracters.")]
    public string Name { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "IBGE code is invalid.")]
    public int IBGECode { get; set; }

    [Required(ErrorMessage = "UF code is required.")]
    public Guid UfId { get; set; }
  }
}
