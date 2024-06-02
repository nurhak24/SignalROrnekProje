﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SignalROrnekProje.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):IdentityDbContext<IdentityUser,IdentityRole,string>(options)
    {


        public DbSet<Product> Products { get; set; }

    }
}
