using System.Collections.Generic;
using System.Linq;

namespace KoatuuMapper.Models
{
    public class ClassifierTypes
    {
        private readonly List<ClassifierType> _types;

        public ClassifierTypes()
        {
            _types = new List<ClassifierType>
            {
                new ClassifierType { Name = "O", Description = "обл." },
                new ClassifierType { Name = "K", Description = "м." },
                new ClassifierType { Name = "P", Description = "р-н" },
                new ClassifierType { Name = "H", Description = "тер. громада" },
                new ClassifierType { Name = "M", Description = "м." },
                new ClassifierType { Name = "T", Description = "смт" },
                new ClassifierType { Name = "C", Description = "с." },
                new ClassifierType { Name = "X", Description = "с-ще" },
                new ClassifierType { Name = "B", Description = "р-н" }
            };
        }

        public string FindType(string name)
        {
            var retult = _types.FirstOrDefault(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
            return retult != null ? retult.Description : string.Empty;
        }
    }

    public class ClassifierType
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
