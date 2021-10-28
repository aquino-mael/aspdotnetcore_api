using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Domain.Dtos.County;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.County
{
    public class WhenRequestCounty : BaseIntegration
    {
        private Guid GlobalTestId;

        public void ChangeGlobalTestId(Guid newTestId)
        {
            GlobalTestId = newTestId;
        }

        [Fact]
        public async Task CanExecuteACRUDInCounties()
        {
            await AddToken();

            #region POST
            var county = new CountyDtoCreate
            {
                Name = "Imperatriz",
                IBGECode = 1234,
                UfId = new Guid("57a9e9f7-9aea-40fe-a783-65d4feb59fa8")
            };

            var response = await PostJsonAsync(county, $"{hostApi}counties", client);
            Assert.True(HttpStatusCode.Created == response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();

            var _countyDtoCreateResult = JsonConvert.DeserializeObject<CountyDtoCreateResult>(jsonResult);

            Assert.NotNull(_countyDtoCreateResult);
            Assert.True(_countyDtoCreateResult.Id != Guid.Empty);

            ChangeGlobalTestId(_countyDtoCreateResult.Id);

            Assert.True(_countyDtoCreateResult.CreatedAt != null);
            Assert.Equal(county.Name, _countyDtoCreateResult.Name);
            Assert.Equal(county.UfId, _countyDtoCreateResult.UfId);
            Assert.Equal(county.IBGECode, _countyDtoCreateResult.IBGECode);
            #endregion

            #region GET
            response = await client.GetAsync($"{hostApi}counties");
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var _counties = JsonConvert.DeserializeObject<IEnumerable<CountyDtoComplete>>(jsonResult);
            Assert.NotNull(_counties);
            Assert.NotEmpty(_counties);

            response = await client.GetAsync($"{hostApi}counties/{GlobalTestId}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var _county = JsonConvert.DeserializeObject<CountyDto>(jsonResult);
            Assert.NotNull(_county);
            Assert.Equal(GlobalTestId, _county.Id);

            response = await client.GetAsync($"{hostApi}counties/complete/{GlobalTestId}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var _countyComplete = JsonConvert.DeserializeObject<CountyDtoComplete>(jsonResult);

            Assert.NotNull(_countyComplete);
            Assert.NotNull(_countyComplete.Uf);
            Assert.Equal(GlobalTestId, _countyComplete.Id);

            var IBGECode = 1234;

            response = await client.GetAsync($"{hostApi}counties/complete/ibge/{IBGECode}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            _countyComplete = JsonConvert.DeserializeObject<CountyDtoComplete>(jsonResult);
            Assert.NotNull(_countyComplete);
            Assert.NotNull(_countyComplete.Uf);
            Assert.Equal(IBGECode, _countyComplete.IBGECode);
            #endregion

            #region PUT
            var _countyDtoUpdate = new CountyDtoUpdate
            {
                Id = GlobalTestId,
                IBGECode = 21341231,
                Name = "São Luis UPDATE",
                UfId = new Guid("5ff1b59e-11e7-414d-827e-609dc5f7e333"),
            };

            response = await client.PutAsync($"{hostApi}counties", new StringContent(
                JsonConvert.SerializeObject(_countyDtoUpdate),
                System.Text.Encoding.UTF8,
                "application/json"
            ));
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var _countyDtoUpdateResult = JsonConvert.DeserializeObject<CountyDtoUpdateResult>(jsonResult);
            Assert.NotNull(_countyDtoUpdateResult);
            Assert.True(_countyDtoUpdateResult.UpdatedAt != null);
            Assert.Equal(_countyDtoUpdate.Id, _countyDtoUpdateResult.Id);
            Assert.Equal(_countyDtoUpdate.IBGECode, _countyDtoUpdateResult.IBGECode);
            #endregion

            #region DELETE
            response = await client.DeleteAsync($"{hostApi}counties/{GlobalTestId}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var _deletedCounty = JsonConvert.DeserializeObject<bool>(jsonResult);
            Assert.NotNull(_deletedCounty);
            Assert.True(_deletedCounty);
            #endregion
        }
    }
}
