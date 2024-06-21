﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.DAL.DTOs.Response
{
    public class UserResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public UserResponseDTO(long id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
