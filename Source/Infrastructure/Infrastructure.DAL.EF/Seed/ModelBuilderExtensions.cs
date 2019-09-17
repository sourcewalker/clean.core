using Core.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.DAL.EF.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Site>().HasData(
                new Site
                {
                    Id = Guid.NewGuid(),
                    Culture = "en-US",
                    Name = "US",
                    Domain = ".com",
                    CreatedDate = DateTimeOffset.UtcNow
                },
                new Site
                {
                    Id = Guid.NewGuid(),
                    Culture = "en-GB",
                    Name = "GB",
                    Domain = ".co.uk",
                    CreatedDate = DateTimeOffset.UtcNow
                },
                new Site
                {
                    Id = Guid.NewGuid(),
                    Culture = "en-IE",
                    Name = "IE",
                    Domain = ".ie",
                    CreatedDate = DateTimeOffset.UtcNow
                },
                new Site
                {
                    Id = Guid.NewGuid(),
                    Culture = "de-DE",
                    Name = "DE",
                    Domain = ".de",
                    CreatedDate = DateTimeOffset.UtcNow
                },
                new Site
                {
                    Id = Guid.NewGuid(),
                    Culture = "de-AT",
                    Name = "AT",
                    Domain = ".at",
                    CreatedDate = DateTimeOffset.UtcNow
                }
            );
        }
    }
}
