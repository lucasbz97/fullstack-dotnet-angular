using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.DAL.DTOs.Request
{
    public class UserRequestDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public UserRequestDTO(long id, string name, int age) 
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
