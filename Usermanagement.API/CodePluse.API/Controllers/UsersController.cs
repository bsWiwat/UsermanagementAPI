using CodePluse.API.DTO;
using CodePluse.API.DTO.Reponse;
using CodePluse.API.Models;
using CodePluse.API.Repositories.Interface;
//using CodePluse.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System;
using System.Linq;
using static CodePluse.API.DTO.Reponse.ResponseDTO;
//using static CodePluse.API.DTO.GetUsersResponseDTO;

namespace CodePluse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        //private readonly DBContext dbContext;

        //public UsersController(DBContext context)
        //{
        //    this.dbContext = context;
        //}

        //[HttpPost]
        //public IActionResult AddUser(UserDTO userDto)
        //{
        //    // Mapping DTO to domain model
        //    var user = new User()
        //    {
        //        //Id = Guid.NewGuid().ToString(), // Generate new GUID for the user
        //        Id = userDto.Id,
        //        FirstName = userDto.FirstName,
        //        LastName = userDto.LastName,
        //        Email = userDto.Email,
        //        Phone = userDto.Phone,
        //        UserName = userDto.Username, // Fixed casing of UserName
        //        Password = userDto.Password,
        //        CreateDate = DateTime.UtcNow,
        //        RoleId = userDto.RoleId,
        //        UserPermissions = new List<UserPermission>(),
        //    };

        //    // Check if user permissions are provided
        //    if (userDto.UserPermissions != null && userDto.UserPermissions.Any())
        //    {
        //        user.UserPermissions = userDto.UserPermissions.Select(permissionDto => new UserPermission
        //        {
        //            PermissionId = permissionDto.PermissionId,
        //            IsReadable = permissionDto.IsReadable,
        //            IsWritable = permissionDto.IsWritable,
        //            IsDeleteable = permissionDto.IsDeletable,
        //            UserId = user.Id
        //        }).ToList();
        //    }

        //    // Add user to the database
        //    dbContext.Users.Add(user);
        //    dbContext.SaveChanges();

        //    return Ok(user);
        //}


        [HttpPost]
        public async Task<IActionResult> AddUser(UserDTO userDto)
        {
            var user = new User()
            {
                //Id = Guid.NewGuid().ToString(), // Generate new GUID for the user
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                UserName = userDto.Username, // Fixed casing of UserName
                Password = userDto.Password,
                CreateDate = DateTime.UtcNow,
                RoleId = userDto.RoleId,
                UserPermissions = new List<UserPermission>(),
            };

            // Check if user permissions are provided
            if (userDto.UserPermissions != null && userDto.UserPermissions.Any())
            {
                user.UserPermissions = userDto.UserPermissions.Select(permissionDto => new UserPermission
                {
                    PermissionId = permissionDto.PermissionId,
                    IsReadable = permissionDto.IsReadable,
                    IsWritable = permissionDto.IsWritable,
                    IsDeleteable = permissionDto.IsDeletable,
                    UserId = user.Id
                }).ToList();
            }

            await userRepository.CreateAsync(user);

            return Ok(userDto);
        }


        // Get all
        //[HttpGet]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var users = await userRepository.GetAllAsync();

        //    // Map model to DTO
        //    var response = new List<UserData>();

        //    foreach (var user in users)
        //    {
        //        var userData = new UserData
        //        {
        //            Id = user.Id,
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //            Email = user.Email,
        //            Phone = user.Phone,
        //            Username = user.UserName,
        //            Roles = new List<RoleData>(),
        //            UserPermissions = new List<UserPermissionData>()
        //        };
        //        var userRole = user.Role;
        //        if (userRole != null)
        //        {
        //            var roleData = new RoleData()
        //            {
        //                RoleId = userRole.RoleId,
        //                RoleName = userRole.RoleName
        //            };
        //            userData.Roles.Add(roleData);
        //        }

        //        response.Add(userData);
        //    }

        //    return Ok(response);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var users = await userRepository.GetAllWithRelatedDataAsync();

        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    // Map users to GetuserDTO
        //    var userDtos = users.Select(user => new GetUserDTO
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Phone = user.Phone,
        //        Email = user.Email,
        //        RoleId = user.RoleId,
        //        RoleName = user.Role?.RoleName, // Null check for Role
        //        UserName = user.UserName,
        //        PermissionId = user.UserPermissions.FirstOrDefault()?.PermissionId ?? "", // Null check for UserPermissions
        //        PermissionName = user.UserPermissions.FirstOrDefault()?.Permission?.PermissionName ?? "" // Null check for Permission
        //    }).ToList();

        //    return Ok(userDtos);
        //}


        [HttpGet]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] string? query,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var users = await userRepository.GetAllWithRelatedDataAsync(query, sortBy, sortDirection, pageNumber, pageSize);

            if (users == null || !users.Any())
            {
                return NotFound();
            }

            var userDataList = new List<UserData>();
            var response = new GetAllUsersResponseDTO
            {
                Status = new Status
                {
                    Code = "200",
                    Description = "Success"
                },
                Data = userDataList
            };

            foreach (var user in users)
            {
                var userData = new UserData
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Username = user.UserName,
                    Date = user.CreateDate,
                    Permissions = new List<PermissionData>()
                };

                // Map role data
                if (user.Role != null)
                {
                    userData.Role = new RoleData
                    {
                        RoleId = user.Role.RoleId,
                        RoleName = user.Role.RoleName
                    };
                }

                // Map permission data
                foreach (var permission in user.UserPermissions)
                {
                    userData.Permissions.Add(new PermissionData
                    {
                        PermissionId = permission.PermissionId,
                        PermissionName = permission.Permission?.PermissionName ?? "",
                        IsReadable = permission.IsReadable ?? false,
                        IsWritable = permission.IsWritable ?? false,
                        IsDeleteable = permission.IsDeleteable ?? false
                    });
                }

                userDataList.Add(userData);
            }

            return Ok(response);
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUserById(string id)
        //{
        //    var user = await userRepository.GetByIdAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var userData = new UserData
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,
        //        Phone = user.Phone,
        //        Username = user.UserName,
        //        Permissions = new List<PermissionData>()
        //    };

        //    // Map role data
        //    if (user.Role != null)
        //    {
        //        userData.Role = new RoleData
        //        {
        //            RoleId = user.Role.RoleId,
        //            RoleName = user.Role.RoleName
        //        };
        //    }

        //    // Map permission data
        //    foreach (var permission in user.UserPermissions)
        //    {
        //        userData.Permissions.Add(new PermissionData
        //        {
        //            PermissionId = permission.PermissionId,
        //            PermissionName = permission.Permission?.PermissionName ?? ""
        //        });
        //    }

        //    return Ok(userData);
        //}


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if(user is null)
            {
                return NotFound();
            }

            var userDataList = new List<UserData>();
            var response = new GetAllUsersResponseDTO
            {
                Status = new Status
                {
                    Code = "200",
                    Description = "Success"
                },
                Data = userDataList
            };

            var userData = new UserData
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Username = user.UserName,
                Permissions = new List<PermissionData>()
            };

            userData.Role = new RoleData
            {
                RoleId = user.Role.RoleId,
                RoleName = user.Role.RoleName
            };

            foreach (var permission in user.UserPermissions)
            {
                userData.Permissions.Add(new PermissionData
                {
                    PermissionId = permission.PermissionId,
                    PermissionName = permission.Permission?.PermissionName ?? ""
                });
                //userData.Permissions.Add(permissionData);
            }
            userDataList.Add(userData);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditUser([FromRoute] string id, UpdateUserDTO request)
        {
            // Fetch the existing user from the database
            var existingUser = await userRepository.GetByIdAsync(id);

            // If the user doesn't exist, return a NotFound response
            if (existingUser == null)
            {
                return NotFound();
            }

            // Update the user's properties with the values from the request DTO
            existingUser.FirstName = request.FirstName;
            existingUser.LastName = request.LastName;
            existingUser.Email = request.Email;
            existingUser.Phone = request.Phone;
            existingUser.UserName = request.Username;
            existingUser.Password = request.Password;
            existingUser.RoleId = request.RoleId;

            // Update user permissions
            existingUser.UserPermissions.Clear(); // Clear existing permissions

            if (request.UserPermissions != null && request.UserPermissions.Any())
            {
                // Add new permissions
                existingUser.UserPermissions.AddRange(request.UserPermissions.Select(permissionDto => new UserPermission
                {
                    PermissionId = permissionDto.PermissionId,
                    IsReadable = permissionDto.IsReadable,
                    IsWritable = permissionDto.IsWritable,
                    IsDeleteable = permissionDto.IsDeletable,
                    UserId = existingUser.Id
                }));
            }

            // Save the changes to the database
            await userRepository.UpdateAsync(existingUser);

            // Return a 200 OK response
            return Ok(request);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] string id)
        {
            var userToDelete = await userRepository.GetByIdAsync(id);

            var response = new ResponseDTO
            {
                status = new ResponseDTO.Status
                {
                    Code = "200",
                    Description = "Success"
                },
                data = new ResponseDTO.Data
                {
                    Result = true,
                    Message = "Deleted"
                }
            };


            if (userToDelete == null)
            {
                return NotFound(); // Return 404 Not Found if user not found
            }

            var isDeleted = await userRepository.DeleteByIdAsync(id);

            if (isDeleted)
            {
                return Ok(response); // Return 200 OK if user deleted successfully
            }
            else
            {
                return StatusCode(500); // Return 500 Internal Server Error if deletion fails
            }
        }


        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetUsersTotal()
        {
            var count = await userRepository.GetCount();

            return Ok(count);
        }
    }
}
