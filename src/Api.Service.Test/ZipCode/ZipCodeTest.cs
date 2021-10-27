using System;
using Api.Domain.Dtos.County;
using Api.Domain.Dtos.Uf;
using Api.Domain.Dtos.ZipCode;

namespace Api.Service.Test.ZipCode
{
    public class ZipCodeTest
    {
        public static Guid Id { get; set; }
        public static string ZipCode { get; set; }
        public static string ChangedZipCode { get; set; }
        public static string Street { get; set; }
        public static string ChangedStreet { get; set; }
        public static string Number { get; set; }
        public static string ChangedNumber { get; set; }
        public static Guid CountyId { get; set; }
        public static Guid UfId { get; set; }

        public ZipCodeDto zipCodeDto;
        public ZipCodeDtoCreate zipCodeDtoCreate;
        public ZipCodeDtoCreateResult zipCodeDtoCreateResult;
        public ZipCodeDtoUpdate zipCodeDtoUpdate;
        public ZipCodeDtoUpdateResult zipCodeDtoUpdateResult;

        public ZipCodeTest()
        {
            Id = Guid.NewGuid();
            ZipCode = Faker.Address.ZipCode();
            Street = Faker.Address.StreetName();
            Number = Faker.RandomNumber.Next(1, 10).ToString();
            CountyId = Guid.NewGuid();

            zipCodeDto = new ZipCodeDto
            {
                Id = Id,
                ZipCode = ZipCode,
                Street = Street,
                Number = Number,
                CountyId = CountyId,
                County = new CountyDtoComplete
                {
                    Id = CountyId,
                    Name = Faker.Name.FullName(),
                    IBGECode = Faker.RandomNumber.Next(1, 10000),
                    UfId = UfId,
                    Uf = new UfDto
                    {
                        Id = UfId,
                        Initials = Faker.Address.UsStateAbbr(),
                        Name = Faker.Name.First(),
                    },
                },
            };

            zipCodeDtoCreate = new ZipCodeDtoCreate
            {
                ZipCode = ZipCode,
                Street = Street,
                Number = Number,
                CountyId = CountyId,
            };

            zipCodeDtoCreateResult = new ZipCodeDtoCreateResult
            {
                Id = Id,
                ZipCode = ZipCode,
                Street = Street,
                Number = Number,
                CountyId = CountyId,
                CreatedAt = DateTime.UtcNow,
            };

            zipCodeDtoUpdate = new ZipCodeDtoUpdate
            {
                Id = Id,
                ZipCode = ChangedZipCode,
                Street = ChangedStreet,
                Number = ChangedNumber,
                CountyId = CountyId,
            };

            zipCodeDtoUpdateResult = new ZipCodeDtoUpdateResult
            {
                Id = Id,
                ZipCode = ChangedZipCode,
                Street = ChangedStreet,
                Number = ChangedNumber,
                CountyId = CountyId,
                UpdatedAt = DateTime.UtcNow,
            };
        }
    }
}
