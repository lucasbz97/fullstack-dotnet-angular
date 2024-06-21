using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.DTOs.Request.User
{
    public class GetUserByIdRequest
    {
        [Required(ErrorMessage = "Id e um campo Requerido!")]
        public long Id { get; set; }
    }
}
