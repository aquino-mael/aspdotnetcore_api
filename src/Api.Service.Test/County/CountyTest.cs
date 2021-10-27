using System;
using System.Collections.Generic;
using Api.Domain.Dtos.County;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.County
{
    public class CountyTest
    {
        public static Guid Id { get; set; }
        public static string Name { get; set; }
        public static string ChangedName { get; set; }
        public static Guid UfId { get; set; }
        public static int IBGECode { get; set; }
        public static int ChangedIBGECode { get; set; }

        public CountyDto countyDto;
        public CountyDtoComplete countyDtoComplete;
        public List<CountyDtoComplete> _countyDtoCompleteList = new List<CountyDtoComplete>();
        public CountyDtoCreate createDtoCounty;
        public CountyDtoUpdate updateDtoCounty;
        public CountyDtoCreateResult createDtoCountyResult;
        public CountyDtoUpdateResult updateDtoCountyResult;

        public CountyTest()
        {
            Name = Faker.Name.FullName();
            ChangedName = Faker.Name.FullName();
            UfId = Guid.NewGuid();
            IBGECode = Faker.RandomNumber.Next(1, 10000);
            ChangedIBGECode = Faker.RandomNumber.Next(1, 10000);

            countyDto = new CountyDto
            {
                Id = Id,
                Name = Name,
                IBGECode = IBGECode,
                UfId = UfId,
            };

            countyDtoComplete = new CountyDtoComplete
            {
                Id = Id,
                Name = Name,
                IBGECode = IBGECode,
                UfId = UfId,
                Uf = new UfDto
                {
                    Id = UfId,
                    Initials = Faker.Address.UsStateAbbr(),
                    Name = Faker.Name.First(),
                },
            };

            createDtoCounty = new CountyDtoCreate
            {
                Name = Name,
                UfId = UfId,
                IBGECode = IBGECode,
            };

            updateDtoCounty = new CountyDtoUpdate
            {
                Id = Id,
                Name = ChangedName,
                IBGECode = ChangedIBGECode,
                UfId = UfId,
            };

            createDtoCountyResult = new CountyDtoCreateResult
            {
                CreatedAt = DateTime.UtcNow,
                Id = Id,
                IBGECode = IBGECode,
                Name = Name,
                UfId = UfId,
            };

            updateDtoCountyResult = new CountyDtoUpdateResult
            {
                Id = Id,
                Name = ChangedName,
                IBGECode = ChangedIBGECode,
                UfId = UfId,
                UpdatedAt = DateTime.UtcNow,
            };

            for (int i = 0; i < 5; i++)
            {
                var _ufId = Guid.NewGuid();
                var _dtoResultComplete = new CountyDtoComplete
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    IBGECode = Faker.RandomNumber.Next(1, 10000),
                    UfId = _ufId,
                    Uf = new UfDto
                    {
                        Id = _ufId,
                        Initials = Faker.Address.UsStateAbbr(),
                        Name = Faker.Name.First(),
                    },
                };
                _countyDtoCompleteList.Add(_dtoResultComplete);
            }
        }
    }
}
