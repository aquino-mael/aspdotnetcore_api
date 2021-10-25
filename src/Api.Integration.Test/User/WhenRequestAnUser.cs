using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.User
{
  public class WhenRequestAnUser : BaseIntegration
  {
    private string _name { get; set; }
    private string _email { get; set; }

    [Fact(DisplayName = "Can do an user CRUD.")]
    public async Task CanDoAnUserCrud()
    {
      await AddToken();
      _name = Faker.Name.First();
      _email = Faker.Internet.Email();

      var userDtoCreate = new UserDtoCreate
      {
        Email = _email,
        Name = _name,
      };

      // POST 
      var response = await PostJsonAsync(userDtoCreate, $"{hostApi}users", client);
      var jsonResult = await response.Content.ReadAsStringAsync();
      var registerPost = JsonConvert.DeserializeObject<UserDtoCreateResult>(jsonResult);

      Assert.Equal(HttpStatusCode.Created, response.StatusCode);
      Assert.Equal(_name, registerPost.Name);
      Assert.Equal(_email, registerPost.Email);
      Assert.NotNull(registerPost.Id);

      // GET ALL
      response = await client.GetAsync($"{hostApi}users");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      jsonResult = await response.Content.ReadAsStringAsync();
      var dtoList = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonResult);

      Assert.NotNull(dtoList);
      Assert.NotEmpty(dtoList);
      Assert.True(dtoList.Where(p => p.Id == registerPost.Id).Count() == 1);

      // PUT
      var userUpdate = new UserDtoUpdate
      {
        Id = registerPost.Id,
        Name = Faker.Name.FullName(),
        Email = Faker.Internet.Email(),
      };

      var stringContent = new StringContent(
        JsonConvert.SerializeObject(userUpdate),
        System.Text.Encoding.UTF8,
        "application/json"
      );

      response = await client.PutAsync($"{hostApi}users", stringContent);

      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      jsonResult = await response.Content.ReadAsStringAsync();
      var updatedResult = JsonConvert.DeserializeObject<UserDtoUpdateResult>(jsonResult);

      Assert.NotNull(updatedResult);
      Assert.Equal(registerPost.Id, updatedResult.Id);
      Assert.NotEqual(registerPost.Name, updatedResult.Name);
      Assert.NotEqual(registerPost.Email, updatedResult.Email);

      // GET BY ID
      response = await client.GetAsync($"{hostApi}users/{updatedResult.Id}");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      jsonResult = await response.Content.ReadAsStringAsync();
      var user = JsonConvert.DeserializeObject<UserDto>(jsonResult);

      Assert.NotNull(user);
      Assert.Equal(updatedResult.Id, user.Id);
      Assert.Equal(updatedResult.Name, user.Name);
      Assert.Equal(updatedResult.Email, user.Email);
      Assert.Equal(registerPost.CreatedAt, user.CreatedAt);

      // DELETE
      response = await client.DeleteAsync($"{hostApi}users/{user.Id}");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      jsonResult = await response.Content.ReadAsStringAsync();
      var deleted = JsonConvert.DeserializeObject<bool>(jsonResult);
      Assert.True(deleted);
      response = await client.GetAsync($"{hostApi}users/{updatedResult.Id}");
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
  }
}
