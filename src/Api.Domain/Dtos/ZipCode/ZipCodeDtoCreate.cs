using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.ZipCode
{
  public class ZipCodeDtoCreate
  {
    [Required(ErrorMessage = "Zip Code is a required field.")]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "Street is a required field.")]
    public string Street { get; set; }

    public string Number { get; set; }

    [Required(ErrorMessage = "County ID is a required field.")]
    public Guid CountyId { get; set; }
  }
}
