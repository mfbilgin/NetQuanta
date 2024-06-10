using Core.DataAccess.EntityFramework;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class EfRoleRepository(DbContext context) : EfEntityRepositoryBase<Role>(context),IRoleRepository
{
    public Role? GetByName(string name)
    {
        return Get(role => role.Name.ToLower() == name.ToLower());
    }
}