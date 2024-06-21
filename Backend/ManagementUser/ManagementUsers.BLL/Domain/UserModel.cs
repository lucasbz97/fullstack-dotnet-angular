using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.Domain
{
    public class UserModel
    {
        public long Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public virtual List<DependentModel> Dependents { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }

        public UserModel(string name, int age, DateTime lastUpdatedAt)
        {
            Name = name;
            Age = age;
            LastUpdatedAt = lastUpdatedAt;
        }
        public UserModel(long id, string name, int age, DateTime lastUpdatedAt)
        {
            Name = name;
            Age = age;
            LastUpdatedAt = lastUpdatedAt;
            Id = id;
        }
    }
}
