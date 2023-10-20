using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ResponseVm.User
{
    public class AuthResponse
    {
        public Guid UserId { get; set; }
        public int Status { get; set; }
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public Guid BranchId { get; set; }

    }
}
