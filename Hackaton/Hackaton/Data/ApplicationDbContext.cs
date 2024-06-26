﻿using Hackaton.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hackaton.Models.Advertisement;


namespace Hackaton.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserData>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<AdvertisementData> AdvertisementData { get; set; }
    }
}
