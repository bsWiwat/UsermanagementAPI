using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CodePluse.API.DTO;
using CodePluse.API.DTO.Reponse;
using CodePluse.API.Models;
using CodePluse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CodePluse.API.DTO.Reponse.ResponseDTO;

namespace CodePluse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionRepository permissionRepository;

        public PermissionsController(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await permissionRepository.GetAllAsync();

            var PermissionDTOs = permissions.Select(permission => new PermissionDTO
            {
                status = new Status // Initialize Status for each RoleDTO
                {
                    Code = "200",
                    Description = "Success"
                },
                data = new PermissionData
                {
                    PermissionId = permission.PermissionId,
                    PermissionName = permission.PermissionName
                }
            }).ToList(); // Ensure to materialize the LINQ query

            return Ok(PermissionDTOs);
        }
    }
}
