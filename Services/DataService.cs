using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheBlogProject.Data;
using TheBlogProject.Enums;
using TheBlogProject.Models;

namespace TheBlogProject.Services
{
    public class DataService
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            await _dbContext.Database.MigrateAsync();

            await SeedRolesAsync();

            await SeedUsersAsync();

            await SeedBlogAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (_dbContext.Roles.Any()) return;

            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            if (_dbContext.Users.Any()) return;

            //Creates a new User for system
            var adminUser = new BlogUser()
            {
                Email = "wesley@wesleysorrentino.com",
                UserName = "wesley@wesleysorrentino.com",
                FirstName = "Wesley",
                LastName = "Sorrentino",
                DisplayName = "Wesley Sorrentino",
                PhoneNumber = "(555) 555-5555",
                EmailConfirmed = true
            };

            //UserManager creates a user defined by Admin
            await _userManager.CreateAsync(adminUser, "Abc&123!");

            //Adds user to Admin Role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            //Adds Moderator
            var modUser = new BlogUser()
            {
                Email = "wes.james17@gmail.com",
                UserName = "wes.james17@gmail.com",
                FirstName = "Wesley",
                LastName = "Sorrentino",
                DisplayName = "Wesley Sorrentino",
                PhoneNumber = "(555) 555-5555",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(modUser, "Abc&123!");

            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }

        private async Task SeedBlogAsync()
        {
            //Adds a Blog to the database if there are none
            if (_dbContext.Blogs.Any()) return;

            var blog = new Blog()
            {
                Name = "Test Blog",
                Description = "Test Description in Seed without userid",
                Created = DateTime.UtcNow
            };

            _dbContext.Add(blog);

            await _dbContext.SaveChangesAsync();
        }
    }
}