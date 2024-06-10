using Core.DataAccess.EntityFramework;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public sealed class EfUserRepository(DbContext context) : EfEntityRepositoryBase<User>(context), IUserRepository
{
    public User? GetByUsername(string username)
    {
        return Get(user => user.Username.ToLower() == username.ToLower());
    }

    public User? GetByEmail(string email)
    {
        return Get(user => user.Email == email);
    }

    public void VerifyEmail(string username)
    {
        var user = GetByUsername(username);
        if (user is null) return;
        user.IsEmailVerified = true;
        Update(user);
    }
}