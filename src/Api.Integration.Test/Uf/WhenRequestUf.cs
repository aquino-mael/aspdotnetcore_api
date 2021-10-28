using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Uf
{
    public class WhenRequestUf : BaseIntegration
    {
        public static Guid UfId = new Guid("b81f95e0-f226-4afd-9763-290001637ed4");
        [Fact]
        public async Task WhenRequestUfGetAll()
        {
            await AddToken();

            var _getAllResponse = await client.GetAsync($"{hostApi}ufs");
            Assert.True(HttpStatusCode.OK == _getAllResponse.StatusCode);

            var jsonResult = await _getAllResponse.Content.ReadAsStringAsync();

            var _allUfsResult = JsonConvert.DeserializeObject<IEnumerable<UfDto>>(jsonResult);
            Assert.True(_allUfsResult.Count() == 27);
        }

        [Fact]
        public async Task WhenRequestUfGetById()
        {
            await AddToken();

            var _getResponse = await client.GetAsync($"{hostApi}ufs/{UfId}");
            Assert.True(HttpStatusCode.OK == _getResponse.StatusCode);

            var jsonResult = await _getResponse.Content.ReadAsStringAsync();

            var _ufResult = JsonConvert.DeserializeObject<UfDto>(jsonResult);
            Assert.NotNull(_ufResult);
            Assert.Equal(UfId, _ufResult.Id);
            Assert.Equal("Santa Catarina", _ufResult.Name);
            Assert.Equal("SC", _ufResult.Initials);
        }
    }
}
