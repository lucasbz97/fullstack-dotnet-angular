using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.DAL.Entities
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }

        // Relacionamentos com outras entidades
        public ICollection<DependentModel> Dependents { get; set; }

        public UserModel(long id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
