﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCApp.Models;

namespace MVCApp.InfraStructure
{
    public class EventDBContext : DbContext
    {
        public EventDBContext(DbContextOptions<EventDBContext> options):base(options)
        {

        }
        public DbSet<EventData> Events { get; set; }

        public DbSet<EventRegister> EventRegister { get; set; }
    }
}
