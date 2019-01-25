using System;
using System.Collections.Generic;
using System.Text;
using EnumSeeder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnumSeeder.Service
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected internal virtual void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


    }
    }
