using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Domain.Dtos.County;
using Api.Domain.Dtos.ZipCode;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.ZipCode
{
    public class WhenRequestZipCode : BaseIntegration
    {
        private Guid GlobalTestId;

        public void ChangeGlobalTestId(Guid newTestId)
        {
            GlobalTestId = newTestId;
        }

        [Fact]
        public async Task CanExecuteACRUDInZipCode()
        {
            await AddToken();

            var _countyDtoCreate = new CountyDtoCreate
            {
                Name = "Imperatriz",
                IBGECode = 123456789,
                UfId = new Guid("57a9e9f7-9aea-40fe-a783-65d4feb59fa8"),
            };

            var response = await PostJsonAsync(_countyDtoCreate, $"{hostApi}counties", client);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var _countyCreationResult = JsonConvert.DeserializeObject<CountyDtoCreateResult>(jsonResult);

            var _zipCodeDtoCreate = new ZipCodeDtoCreate
            {
                Street = "Rua tamôios",
                Number = "39",
                ZipCode = "65911-324",
                CountyId = _countyCreationResult.Id,
            };

            #region POST

            response = await PostJsonAsync(_zipCodeDtoCreate, $"{hostApi}zipCodes", client);
            Assert.True(HttpStatusCode.Created == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var _zipCodeDtoCreateResult = JsonConvert.DeserializeObject<ZipCodeDtoCreateResult>(jsonResult);
            Assert.NotNull(_zipCodeDtoCreateResult);
            Assert.True(_zipCodeDtoCreateResult.Id != default(Guid));

            #endregion

            #region GET

            // BY ID

            response = await client.GetAsync($"{hostApi}zipCodes/id/{_zipCodeDtoCreateResult.Id}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var _getByIdZipCode = JsonConvert.DeserializeObject<ZipCodeDto>(jsonResult);
            Assert.NotNull(_getByIdZipCode);
            Assert.Null(_getByIdZipCode.County);
            Assert.Equal(_zipCodeDtoCreateResult.Id, _getByIdZipCode.Id);

            // BY CEP

            response = await client.GetAsync($"{hostApi}zipCodes/cep/{_zipCodeDtoCreateResult.ZipCode}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var _getByZipCode = JsonConvert.DeserializeObject<ZipCodeDto>(jsonResult);
            Assert.NotNull(_getByZipCode);
            Assert.NotNull(_getByZipCode.County);
            Assert.True(_getByZipCode.Id != default(Guid));

            #endregion

            #region PUT

            var _zipCodeDtoUpdate = new ZipCodeDtoUpdate
            {
                Id = _zipCodeDtoCreateResult.Id,
                CountyId = _zipCodeDtoCreateResult.CountyId,
                Street = "TESTE UPDATE",
                ZipCode = "213234123",
            };

            response = await client.PutAsync($"{hostApi}zipCodes", new StringContent(
                JsonConvert.SerializeObject(_zipCodeDtoUpdate),
                System.Text.Encoding.UTF8,
                "application/json"
            ));
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var _updateResult = JsonConvert.DeserializeObject<ZipCodeDtoUpdateResult>(jsonResult);
            Assert.NotNull(_updateResult);
            Assert.True(_updateResult.Id != default(Guid));
            Assert.Equal(_zipCodeDtoUpdate.Id, _updateResult.Id);
            Assert.Equal(_zipCodeDtoUpdate.Street, _updateResult.Street);
            Assert.Equal(_zipCodeDtoUpdate.ZipCode, _updateResult.ZipCode);
            Assert.Equal(_zipCodeDtoUpdate.CountyId, _updateResult.CountyId);

            #endregion

            #region DELETE

            response = await client.DeleteAsync($"{hostApi}zipCodes/{_zipCodeDtoCreateResult.Id}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var _deletedResult = JsonConvert.DeserializeObject<bool>(jsonResult);
            Assert.NotNull(_deletedResult);
            Assert.True(_deletedResult);

            response = await client.GetAsync($"{hostApi}zipCodes/id/{_zipCodeDtoCreateResult.Id}");
            Assert.True(HttpStatusCode.NotFound == response.StatusCode);

            #endregion
        }
    }
}
