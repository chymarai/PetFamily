using PetFamily.Accounts.Application.Commands.RegisterUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Presentation.Requests;

public record RegisterUserRequest(
    string UserName,
    string Email,
    string Password)
{
    public RegisterUserCommand ToCommand() => 
        new(UserName, Email, Password); 
}