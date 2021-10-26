using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.Uf
{
    public class UfTest
    {
        public static string Name { get; set; }
        public static string Initials { get; set; }
        public static Guid IdUf { get; set; }
        public List<UfDto> dtoList = new List<UfDto>();
        public UfDto ufDto;

        public UfTest()
        {
            IdUf = Guid.NewGuid();
            Name = Faker.Name.FullName();
            Initials = Faker.Address.UsStateAbbr();
            ufDto = new UfDto
            {
                Id = IdUf,
                Initials = Initials,
                Name = Name
            };

            for (int i = 0; i < 5; i++)
            {
                var dto = new UfDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Initials = Faker.Address.UsStateAbbr(),
                };

                dtoList.Add(dto);
            }
        }
    }
}
