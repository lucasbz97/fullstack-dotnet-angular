using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.Domain
{
    public class DependentModel
    {
        public long Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public long UserId { get; set; } // Chave estrangeira para User
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual UserModel User { get; set; } // propriedade de navegacao para o EF fazer o relacionamento entre as entidades

        public DependentModel(string name, int age, long userId)
        {
            Name = name;
            Age = age;
            UserId = userId;
            LastUpdatedAt = DateTime.UtcNow;
        }
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
