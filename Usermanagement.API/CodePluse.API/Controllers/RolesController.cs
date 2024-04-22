using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePluse.API.DTO;
using CodePluse.API.DTO.Reponse;
using CodePluse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CodePluse.API.DTO.Reponse.ResponseDTO;

namespace CodePluse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await roleRepository.GetAllAsync();

            // Initialize the response object
            var roleDTOs = roles.Select(role => new RoleDTO
            {
                status = new Status // Initialize Status for each RoleDTO
                {
                    Code = "200",
                    Description = "Success"
                },
                data = new RoleData
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                }
            }).ToList(); // Ensure to materialize the LINQ query

            // Return the response object
            return Ok(roleDTOs);
        }
    }
}
