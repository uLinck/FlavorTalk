using FlavorTalk.Domain;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorTalk.Core.Features.Users.Queries;
public static class GetAllUsers // Not really used for anything. And probably will continue like this. Used to test the authentication
{
    public record Query(); // TODO: Add pagination

    public record Response(Guid Id, string? Name, string? Email)
    {
        public static Response ResponseFromUser(User user) => new(user.Id, user.UserName, user.Email);
    }

    public class Handler
    {
        public static async Task<List<Response>> Handle(Query query, UserManager<User> userManager) => (await userManager.Users.ToListAsync()).Select(Response.ResponseFromUser).ToList();
    }
}
