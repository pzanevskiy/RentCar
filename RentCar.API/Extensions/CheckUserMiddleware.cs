using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RentCar.Database;
using RentCar.Database.Entities.UserEntities;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentCar.API.Extensions
{
    public class CheckUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, RentCarDbContext dbContext)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            var userId = identity?.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var user = await dbContext.User.FindAsync(Guid.Parse(userId));

            if (user is null)
            {
                dbContext.User.Add(new User
                {
                    UserId = Guid.Parse(identity.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value),
                    FirstName = identity.Claims.First(x => x.Type == ClaimTypes.GivenName).Value,
                    LastName = identity.Claims.First(x => x.Type == ClaimTypes.Surname).Value,
                    Email = identity.Claims.First(x => x.Type == ClaimTypes.Email).Value
                });
            }

            var tokenRoles = identity?.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
            var dbRoles = await dbContext.Role.Select(x => x.Name).ToListAsync();
            var notExistingRoles = tokenRoles.Except(dbRoles);

            if (notExistingRoles.Any())
            {
                foreach (var role in notExistingRoles)
                {
                    dbContext.Role.Add(new Role
                    {
                        Name = role
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            var usersRoles = await dbContext.Role.Select(x => x.RoleId).ToListAsync();

            foreach (var roleId in usersRoles)
            {
                if (!await dbContext.UsersRoles
                    .AnyAsync(x => x.UserId == Guid.Parse(userId) && x.RoleId == roleId))
                {
                    dbContext.UsersRoles.Add(new UsersRoles
                    {
                        UserId = Guid.Parse(userId),
                        RoleId = roleId
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            await _next(context);
        }
    }
}
