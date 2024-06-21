using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.DTOs.Response.Dependent
{
    public class DependentResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public DependentResponse(long id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
