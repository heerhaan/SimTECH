using Microsoft.AspNetCore.Components.Authorization;
using SimTECH.Data.Models;
using SimTECH.Services;
using System.Security.Claims;

namespace SimTECH.Providers
{
    public class SimAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {
        private readonly UserService userService;

        public User CurrentUser { get; private set; } = new();

        public SimAuthenticationStateProvider(UserService userService)
        {
            this.userService = userService;

            AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        }

        public async Task LoginAsync(string username, string password)
        {
            var principal = new ClaimsPrincipal();
            var user = userService.FindUser(username, password);

            if (user != null)
            {
                await userService.PersistUserToLocalAsync(user);
                principal = user.ToClaimsPrincipal();
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public async Task LogoutAsync()
        {
            await userService.ClearLocalUserAsync();

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
        }

        // NOTE: Don't call this method inside a component, it will create a new function-call to GetAuthenticationStateAsync() every time the component is added
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();
            var user = await userService.FetchLocalUserAsync();

            if (user != null)
            {
                var dbUser = userService.FindUser(user.Username, user.Password);

                if (dbUser != null)
                {
                    principal = dbUser.ToClaimsPrincipal();
                    CurrentUser = dbUser;
                }
            }

            return new(principal);
        }

        private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
        {
            var authenticationState = await task;

            if (authenticationState != null)
            {
                CurrentUser = User.FromClaimsPrincipal(authenticationState.User);
            }
        }

        public void Dispose()
        {
            AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
        }
    }
}
