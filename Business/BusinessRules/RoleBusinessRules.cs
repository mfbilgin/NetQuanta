using Business.Constants.Messages;
using Core.Exceptions;
using DataAccess.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Business.BusinessRules;

public class RoleBusinessRules(IRoleRepository roleRepository)
{
    public void RoleNameCanNotBeDuplicated(string name)
    {
        var role = roleRepository.GetByName(name);
        if (role is not null)
        {
            throw new BusinessException(RoleMessages.RoleNameAlreadyExists,StatusCodes.Status409Conflict);
        }
    }
    public void RoleIdMustBeExist(Guid id)
    {
        var role = roleRepository.Get(role => role.Id == id);
        if (role is null)
        {
            throw new BusinessException(RoleMessages.RoleNotFound,StatusCodes.Status404NotFound);
        }
    }
}