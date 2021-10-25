using System;

namespace Api.Domain.models
{
    public class CountyModel : BaseModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _IBGECode;
        public int IBGECode
        {
            get { return _IBGECode; }
            set { _IBGECode = value; }
        }

        private Guid _ufId;
        public Guid UfId
        {
            get { return _ufId; }
            set { _ufId = value; }
        }


    }
}
