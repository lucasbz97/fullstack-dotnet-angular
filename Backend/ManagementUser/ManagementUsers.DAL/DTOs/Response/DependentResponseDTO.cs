using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.DAL.DTOs.Response
{
    public class DependentResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public long UserId { get; set; }

        public DependentResponseDTO(long id, string name, int age, long userId) 
        {
            Id = id;
            Name = name;
            Age = age;
            UserId = userId;
        }
    }
}
