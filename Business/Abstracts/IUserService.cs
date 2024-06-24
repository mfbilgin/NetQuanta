using Core.Entities.Concretes;
using Core.Entities.Dtos.Auth;
using Core.Entities.Dtos.User;
using Core.Extensions.Paging;

namespace Business.Abstracts;

public interface IUserService
{
    public void Add(User user);
    public void Update(UserUpdateDto userUpdateDto);
    public void Delete(UserDeleteDto userDeleteDto);
    public PageableModel<UserGetDto> GetAll(int index = 1, int size = 10);
    public UserGetDto? GetById(Guid id);
    public UserGetDto? GetByUsername(string name);
    public string GetEmailByUsername(string username);

    public void VerifyEmail(string username);
    public void ChangeUserRole(ChangeUserRoleDto changeUserRoleDto);
    public User ChangePassword(ChangePasswordDto changePasswordDto);
    public User ResetPassword(ResetPasswordDto resetPasswordDto);
    public string RemindUsername(string email);
}