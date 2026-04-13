using AutoMapper;
using Identity.DTOs.Permission;
using Identity.Repositories.Interfaces;
using Identity.Services.Interfaces;
using Identity.Exceptions;
using Identity.Entities;

namespace Identity.Services.Implementations
{
    public class PermissionService: IPermissionService
    {
        private readonly IPermissionRepository _permissionRepo;
        private readonly IMapper _mapper;

        public PermissionService(IPermissionRepository permissionRepo,IMapper mapper)
        {
            _permissionRepo = permissionRepo;
            _mapper = mapper;
        }

        public async Task<List<PermissionDto>> GetAllPermissionsAsync()
        {
            var permissions = await _permissionRepo.GetAllAsync();

            return _mapper.Map<List<PermissionDto>>(permissions);
        }
        public async Task<PermissionDto> GetPermissionByIdAsync(int permissionId)
        {
            var permission = await _permissionRepo.GetByIdAsync(permissionId);

            if (permission == null)
            {
                throw new NotFoundException("Permission not found");
            }

            return _mapper.Map<PermissionDto>(permission);
        }
        public async Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto dto)
        {

            var existing =  await _permissionRepo.GetByNameAsync(dto.Name);

            if(existing == null)
            {
                throw new NotFoundException("Permission not found");
            }

            var permission = _mapper.Map<Permission>(dto);
            var created = await _permissionRepo.CreateAsync(permission);
            return _mapper.Map<PermissionDto>(created);
        }

        public async Task DeletePermissionAsync(int permissionId)
        {
            var permission = await _permissionRepo.GetByIdAsync(permissionId);

            if (permission == null)
            {
                throw new NotFoundException("Permission not found");
            }

            await _permissionRepo.DeleteAsync(permissionId);
        }

        public async Task<List<string>> GetRolesByPermissionAsync(int permissionId)
        {
            return await _permissionRepo.GetRolesByPermissionIdAsync(permissionId);
        }


        public async Task<List<string>> GetUsersByPermissionAsync(int permissionId)
        {
            return await _permissionRepo.GetUserByPermissionIdAsync(permissionId);
        }

        public async Task<bool> IsPermissionAssignedToRoleAsync(int roleId, int permissionId)
        {
            return await _permissionRepo.IsPermissionAssignedToRole(roleId, permissionId);
        }
        public async Task<bool> IsPermissionAssignedToUserAsync(int userId, int permissionId)
        {
            return await _permissionRepo.IsPermissionAssignedToUser(userId, permissionId);
        }
    }
}
