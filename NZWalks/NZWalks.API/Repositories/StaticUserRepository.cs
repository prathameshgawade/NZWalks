using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Id = Guid.NewGuid(), Username = "readOnly", EmailAddress = "readOnly@nzwalks.com", FirstName = "Read Only", LastName = "User", Roles = new List<string> { "reader" }, Password = "readOnly" },
            new User { Id = Guid.NewGuid(), Username = "readWrite", EmailAddress = "readWrite@nzwalks.com", FirstName = "Read Write", LastName = "User", Roles = new List<string> { "reader", "writer" }, Password = "readWrite" },
        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = _users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && x.Password == password);

            return user;
        }
    }
}
