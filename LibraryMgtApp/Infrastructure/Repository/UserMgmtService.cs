using LibraryMgtApp.Context;
using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Dto;
using LibraryMgtApp.Extensions;
using LibraryMgtApp.Extensions.Helpers;
using LibraryMgtApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static LibraryMgtApp.Context.Models.AppUser;

namespace LibraryMgtApp.Infrastructure.Repository
{
    public class UserMgmtService : IUserMgmtService
    {
        private List<ValidationResult> results = new List<ValidationResult>();
        private readonly DataContext _context;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public UserMgmtService(
            DataContext context,
            RoleManager<ApplicationIdentityRole> roleManager,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<(List<ValidationResult> Result, AddUserDto User)> CreateUser(AddUserDto vm)
        {
            results.Clear();
            try
            {
                var user = AppUser.Create(vm.FirstName, vm.LastName, vm.Email, vm.Gender, vm.PhoneNumber, vm.NIN);

                bool isValid = Validator.TryValidateObject(user, new ValidationContext(user, null, null),
                    results, false);

                if (!isValid || results.Count > 0)
                    return (results, null);

                user.UserName = vm.Email;
                user.EmailConfirmed = true;
                user.FullName = vm.FirstName + " " + vm.LastName;
                user.Activated = true;
                user.IsDisabled = false;
                user.CreatedOnUtc = DateTime.Now.GetDateUtcNow();
                user.LockoutEnabled = false;

                var createResult = await _userManager.CreateAsync(user, vm.Password);
                createResult = await _userManager.AddToRolesAsync(user, vm.Roles);
            }
            catch (Exception ex)
            {
                results.Add(new ValidationResult($"User couldn't be created! \n {ex.Message}"));
            }
            return (results, vm);
        }

        public async Task<ApplicationIdentityRole> GetRoleById(Guid id)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
            return role;
        }

        public async Task<IEnumerable<ApplicationIdentityRole>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<AppUser> GetUser(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<PagedList<AppUser>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.OrderBy(u => u.Id).AsQueryable().Where(c => c.IsDisabled == false);
            return await PagedList<AppUser>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }
        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email == email))
                return true;

            return false;
        }

        public async Task<bool> PhoneExists(string phone)
        {
            if (await _context.Users.AnyAsync(x => x.PhoneNumber == phone))
                return true;

            return false;
        }

        public async Task<(List<ValidationResult> Result, UserForUpdateDto User)> UpdateUser(UserForUpdateDto vm, Guid UserId)
        {
            results.Clear();
            try
            {
                var user = await this.GetUser(UserId);
                if (user == null)
                {
                    results.Add(new ValidationResult("User couldn't be found to complete update operation."));
                    return (results, null);
                }
                user.UserName = user.Email;
                user.Email = user.Email; //ReadOnly
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.FullName = vm.FirstName + ' ' + vm.LastName;
                user.Gender = vm.Gender;
                user.PhoneNumber = vm.PhoneNumber;
                user.NIN = vm.NIN;
                user.IsDisabled = false;

                bool isValid = Validator.TryValidateObject(user, new ValidationContext(user, null, null),
                    results, false);

                if (!isValid || results.Count > 0)
                    return (results, null);
                await _userManager.UpdateAsync(user);

                return (results, vm);
            }
            catch (Exception ex)
            {
                results.Add(new ValidationResult($"User couldn't be updated! \n {ex.Message}"));
            }
            return (results, vm);
        }

        public async Task<(List<ValidationResult> Result, AppUser User)> DeleteUser(Guid id)
        {
            results.Clear();
            try
            {
                var user = await this.GetUser(id);
                if (user == null)
                {
                    results.Add(new ValidationResult("User couldn't be found to complete delete operation."));
                    return (results, null);
                }

                //delete user
                user.IsDisabled = true;
                var deleteUser = await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                results.Add(new ValidationResult($"User couldn't be deleted! \n {ex.Message}"));
            }
            return (results, null);
        }
        public async Task<AppUser> GetUserPhoneNumber(string phone)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phone);
            return user;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}