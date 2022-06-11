using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Create
{
    public class UserActivationCreateDto : BaseEntityCreateDto
    {
        public int Id { get; set; }
        public int? ActivationCode { get; set; }
        public int? ResetPasswordCode { get; set; }
    }
}
