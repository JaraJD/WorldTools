using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.Commands.UserCommands
{
    public class LoginUserCommand
    {
        [Required] public string UserEmail { get; set; }
        [Required] public string Password { get; set; }

    }
}
