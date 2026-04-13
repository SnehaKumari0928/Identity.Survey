using AutoMapper;
using Identity.DTOs.Role;
using Identity.Entities;
using Identity.Exceptions;
using Identity.Repositories.Interfaces;
using Identity.Services.Interfaces;
using System.Data;

namespace Identity.Services.Implementations
{
    public class RoleService :  IRoleService
    {

        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepo,IMapper mapper)
        {
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        public async Task<RoleResponseDto> GetRoleWithPermissionsAsync(int roleId)
        {
            var role = await _roleRepo.GetRoleWithPermissionsAsync(roleId);


            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }

            var dto = _mapper.Map<RoleResponseDto>(role);

            dto.Permissions = await _roleRepo.GetPemissionsByRoleId(roleId);

            return dto;
        }
        public async Task<List<string>> GetRolePemissionsAsync(int roleId)
        {

            var role = await _roleRepo.GetRoleWithPermissionsAsync(roleId);


            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }

            return await _roleRepo.GetPemissionsByRoleId(roleId);
        }

        public async Task<RoleResponseDto> GetRoleByIdAsync(int roleId)
        {
            var role = await _roleRepo.GetByIdAsync(roleId);

            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }

            return _mapper.Map<RoleResponseDto>(role);
        }

        public async Task<List<RoleResponseDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepo.GetAllAsync();

            return _mapper.Map<List<RoleResponseDto>>(roles);
        }
        public async Task<RoleResponseDto> CreateRoleAsync(CreateRoleDto dto)
        {
            var role = _mapper.Map<Role>(dto);

            var createdRole = await _roleRepo.CreateAsync(role);

            return _mapper.Map<RoleResponseDto>(createdRole);
        }

        public async Task<RoleResponseDto> UpdateRoleAsync(int roleId,UpdateRoleDto dto)
        {
            var role = await _roleRepo.GetByIdAsync(roleId);

            if(role == null)
            {
                throw new NotFoundException("Role not found");
            }

            role.Name = dto.Name;

            await _roleRepo.UpdateAsync(role);

            return _mapper.Map<RoleResponseDto>(role);
        }
        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _roleRepo.GetByIdAsync(roleId);
            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }

            await _roleRepo.DeleteAsync(roleId);

        }

        public async Task AssignPermissionsAsync(AssignPermissionToRoleDto dto)
        {
            var role = await _roleRepo.GetByIdAsync(dto.RoleId);
            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }

            if(dto.PermissionId == null || !dto.PermissionId.Any())
            {
                throw new BadRequestException("Permission list cannot be empty");
            }

            await _roleRepo.AssignPermissionsAsync(dto.RoleId, dto.PermissionId);
        }
    }
}
