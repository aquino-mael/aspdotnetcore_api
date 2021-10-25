using System;
using Api.Domain.Dtos.Uf;

namespace Api.Domain.Dtos.County
{
  public class CountyDtoComplet
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int IBGECode { get; set; }
    public Guid UfId { get; set; }
    public UfDto Uf { get; set; }
  }
}
