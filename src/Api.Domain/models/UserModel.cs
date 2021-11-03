namespace Api.Domain.models
{
  public class UserModel : BaseModel
  {
    private string _name;
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    private string _email;
    public string Email
    {
      get { return _email.ToLower(); }
      set { _email = value; }
    }
  }
}
