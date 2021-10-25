namespace Api.Domain.models
{
    public class ZipCodeModel : BaseModel
    {
        private string _cep;
        public string Cep
        {
            get { return _cep; }
            set { _cep = value; }
        }
        private string _street;
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        private string _number;
        public string Number
        {
            get { return _number; }
            set
            {
                _number = string.IsNullOrEmpty(value)
                  ? "S/N"
                  : value;
            }
        }
    }
}
