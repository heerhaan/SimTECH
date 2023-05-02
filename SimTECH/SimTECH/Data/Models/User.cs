﻿using SimTECH.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace SimTECH.Data.Models
{
    // Authentication & authorization has been disabled for now
    public sealed class User
    {
        public long Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public Country Country { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; } = new();

        // CoolGrade is an example meant to show how policy authorization works
        [NotMapped]
        public int CoolGrade { get; set; }

        public ClaimsPrincipal ToClaimsPrincipal()
        {
            return new ClaimsPrincipal(
                new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, Username),
                        new Claim(ClaimTypes.Hash, Password),
                        new Claim(nameof(FullName), FullName),
                        new Claim(nameof(Country), Country.ToString()),
                        new Claim(nameof(CoolGrade), CoolGrade.ToString())
                    }.Concat(Roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()),
                    "SimTech"
                )
            );
        }

        public static User FromClaimsPrincipal(ClaimsPrincipal principal) => new()
        {
            Username = principal.FindFirstValue(ClaimTypes.Name) ?? string.Empty,
            Password = principal.FindFirstValue(ClaimTypes.Hash) ?? string.Empty,
            FullName = principal.FindFirstValue(nameof(FullName)) ?? string.Empty,
            Country = Enum.TryParse(principal.FindFirstValue(nameof(Country)), out Country parsedCountry)
                ? parsedCountry
                : EnumHelper.GetDefaultCountry(),
            CoolGrade = Convert.ToInt32(principal.FindFirstValue(nameof(CoolGrade))),
            Roles = principal
                .FindAll(ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList(),
        };
    }
}