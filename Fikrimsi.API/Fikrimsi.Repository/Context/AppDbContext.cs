﻿using Fikrimsi.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Repository.Context
{
    public class AppDbContext : IdentityDbContext<UserApp, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Title> Titles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }

    }
}
