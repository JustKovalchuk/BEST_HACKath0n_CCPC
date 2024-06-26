﻿using Hackaton.Models.Advertisement;
using Hackaton.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Hackaton.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<UserData> UserData { get; set; }
        public DbSet<AdvertisementData> AdvertisementData { get; set; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
