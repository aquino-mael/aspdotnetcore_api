using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.County
{
  public class CountyDtoUpdateResult
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int IBGECode { get; set; }
    public Guid UfId { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
