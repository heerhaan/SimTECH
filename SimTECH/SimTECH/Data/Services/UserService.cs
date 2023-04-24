using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    // Authentication & authorization has been disabled for now
    public class UserService
    {
        /* NOTE: Local storage (aka browser storage is used in this situation but we should evaluate if it is actually the best option.
           Bear in mind that if you move away from protectedLocalStorage, then you need to register the service in Program.cs. */
        private readonly ProtectedLocalStorage protectedLocalStorage;

        private readonly string localStorageKey = "simTechUsers";

        public UserService(ProtectedLocalStorage protectedLocalStorage)
        {
            this.protectedLocalStorage = protectedLocalStorage;
        }

        public User? FindUser(string username, string password)
        {
            var testUsers = new List<User>()
            {
                new User()
                {
                    Username = "belial",
                    Password = "theredking",
                    FullName = "Belial the Red King",
                    CoolGrade = 9,
                    Roles = new List<string> { "admin" }
                },
                new User()
                {
                    Username = "beldr",
                    Password = "unbreakableone",
                    FullName = "Baldur the Prince",
                    CoolGrade = 5,
                }
            };

            return testUsers.SingleOrDefault(e => e.Username == username && e.Password == password);
        }

        public async Task<User?> FetchLocalUserAsync()
        {
            try
            {
                var storedUserResult = await protectedLocalStorage.GetAsync<string>(localStorageKey);

                if (storedUserResult.Success && !string.IsNullOrEmpty(storedUserResult.Value))
                {
                    return JsonConvert.DeserializeObject<User>(storedUserResult.Value);
                }
            }
            catch (InvalidOperationException)
            {
            }

            return null;
        }

        public async Task PersistUserToLocalAsync(User user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            await protectedLocalStorage.SetAsync(localStorageKey, userJson);
        }

        public async Task ClearLocalUserAsync() => await protectedLocalStorage.DeleteAsync(localStorageKey);
    }
}
