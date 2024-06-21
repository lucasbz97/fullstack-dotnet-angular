using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.DTOs.Request.DependentRequest
{
    public class UpdateDependentRequest : DependentRequest
    {
        public long? Id { get; set; }
        [Required(ErrorMessage = "Name é um campo requerido")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age é um campo requerido")]
        [Range(19, 120, ErrorMessage = "A idade deve ser maior que 18 e menor que 120")]
        public int Age { get; set; }
    }
}
