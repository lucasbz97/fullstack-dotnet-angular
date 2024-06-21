using ManagementUsers.BLL.DTOs.Response.Dependent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.DTOs.Response.User
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public List<DependentResponse?> Dependents { get; set; } = new List<DependentResponse?> { };

        public UserResponse(string name, int age, long id)
        {
            Name = name;
            Age = age;
            Id = id;
        }
    }
}