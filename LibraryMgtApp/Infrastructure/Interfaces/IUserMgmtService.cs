using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Dto;
using LibraryMgtApp.Extensions.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using static LibraryMgtApp.Context.Models.AppUser;

namespace LibraryMgtApp.Infrastructure.Interfaces
{
    public interface IUserMgmtService
    {
        Task<IEnumerable<ApplicationIdentityRole>> GetRoles();
        Task<ApplicationIdentityRole> GetRoleById(Guid id);
        Task<PagedList<AppUser>> GetUsers(UserParams userParams);
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUser(Guid id);
        Task<AppUser> GetUserByEmail(string email);
        Task<AppUser> GetUserPhoneNumber(string phone);
        Task<(List<ValidationResult> Result, AppUser User)> DeleteUser(Guid id);
        Task<(List<ValidationResult> Result, AddUserDto User)> CreateUser(AddUserDto vm);
        Task<(List<ValidationResult> Result, UserForUpdateDto User)> UpdateUser(UserForUpdateDto vm, Guid UserId);
        Task<bool> UserExists(string email);
        Task<bool> PhoneExists(string phone);
    }
}
