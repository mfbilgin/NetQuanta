using AutoMapper;
using Business.Abstracts;
using Business.BusinessRules;
using Business.ValidationRules.FluentValidation.RoleValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Security;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concretes;
using Core.Entities.Dtos.Role;
using Core.Extensions.Paging;
using DataAccess.Abstracts;

namespace Business.Concretes;

public sealed class RoleManager(IRoleRepository roleRepository, IMapper mapper, RoleBusinessRules roleBusinessRules)
    : IRoleService
{
    [SecurityAspect("admin")]
    [ValidationAspect(typeof(RoleAddValidator))]
    [CacheRemoveAspect("IRoleService.Get")]
    public void Add(RoleAddDto roleAddDto)
    {
        roleBusinessRules.RoleNameCanNotBeDuplicated(roleAddDto.Name);

        var role = mapper.Map<Role>(roleAddDto);
        role.Id = Guid.NewGuid();
        role.Name = role.Name.ToLower();
        roleRepository.Add(role);
    }

    [SecurityAspect("admin")]
    [ValidationAspect(typeof(RoleUpdateValidator))]
    [CacheRemoveAspect("IRoleService.Get")]
    public void Update(RoleUpdateDto roleUpdateDto)
    {
        roleBusinessRules.RoleIdMustBeExist(roleUpdateDto.Id);
        roleBusinessRules.RoleNameCanNotBeDuplicated(roleUpdateDto.Name);

        var role = mapper.Map<Role>(roleUpdateDto);
        role.Name = role.Name.ToLower();
        roleRepository.Update(role);
    }

    [SecurityAspect("admin")]
    [CacheRemoveAspect("IRoleService.Get")]
    public void Delete(RoleDeleteDto roleDeleteDto)
    {
        roleBusinessRules.RoleIdMustBeExist(roleDeleteDto.Id);
        var role = mapper.Map<Role>(roleDeleteDto);

        roleRepository.Delete(role);
    }
    
    [CacheAspect]
    public RoleGetDto? GetById(Guid id)
    {
        var role = roleRepository.GetById(id);
        return mapper.Map<RoleGetDto>(role);
    }

    [SecurityAspect("admin")]
    [CacheAspect]
    public RoleGetDto? GetByName(string name)
    {
        var role = roleRepository.GetByName(name);
        return mapper.Map<RoleGetDto>(role);
    }

    [SecurityAspect("admin")]
    [CacheAspect]
    public PageableModel<RoleGetDto> GetAll(int index = 1, int size = 10)
    {
        var roles = roleRepository.GetList(index, size);
        return mapper.Map<PageableModel<RoleGetDto>>(roles);
    }
}