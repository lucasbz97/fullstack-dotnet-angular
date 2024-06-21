using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.DAL.Entities
{
    public class DependentModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }

        // Relacionamento com UserModel
        public UserModel User { get; set; }

        public DependentModel(long id, string name, int age, long userId)
        {
            Id = id;
            Name = name;
            Age = age;
            UserId = userId;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }

}
