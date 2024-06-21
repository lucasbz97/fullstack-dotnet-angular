using ManagementUsers.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.DAL.DTOs.Request
{
    public class DependentRequestDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public long UserId { get; set; }

        public DependentRequestDTO(long id, string name, int age, long userId)
        {
            Id = id;
            Name = name;
            Age = age;
            UserId = userId;
        }

        public DependentRequestDTO(string name, int age, long userId)
        {
            Name = name;
            Age = age;
            UserId = userId;
        }
    }
}
