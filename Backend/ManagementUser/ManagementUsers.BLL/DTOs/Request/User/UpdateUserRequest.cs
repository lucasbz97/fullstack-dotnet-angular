using ManagementUsers.BLL.DTOs.Request.DependentRequest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.DTOs.Request.User
{
    public class UpdateUserRequest
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name é um campo requerido")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age é um campo requerido")]
        public int Age { get; set; }

        public List<UpdateDependentRequest> Dependents { get; set; } = new List<UpdateDependentRequest>();

        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
