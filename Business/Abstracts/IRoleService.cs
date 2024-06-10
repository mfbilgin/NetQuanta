using Core.Entities.Dtos.Role;
using Core.Extensions.Paging;

namespace Business.Abstracts;

public interface IRoleService
{
    public void Add(RoleAddDto roleAddDto);
    public void Update(RoleUpdateDto roleUpdateDto);
    public void Delete(RoleDeleteDto roleDeleteDto);
    public RoleGetDto? GetById(Guid id);
    public PageableModel<RoleGetDto> GetAll(int index = 1, int size = 10);
    RoleGetDto? GetByName(string name);
}