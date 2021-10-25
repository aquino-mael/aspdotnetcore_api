using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.ZipCode
{
  public class ZipCodeDtoUpdateResult
  {
    public Guid Id { get; set; }

    public string ZipCode { get; set; }

    public string Street { get; set; }

    public string Number { get; set; }

    public Guid CountyId { get; set; }

    public DateTime UpdatedAt { get; set; }
  }
}
