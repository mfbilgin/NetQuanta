
using Core.Entities.Concretes;

namespace Core.Security.Authorization;

public interface ITokenHelper
{
    AccessToken CreateToken(User user,Role role);

}