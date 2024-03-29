﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    public class UniDelDbContext : DbContext
    {
        public UniDelDbContext(DbContextOptions<UniDelDbContext> options) : base(options)
        {

        }



        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<FleetManager> FleetManagers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<CourierCompany> CourierCompanies { get; set; }
        public DbSet<CompanyVehicle> CompanyVehicles { get; set; }
        public DbSet<CompanyDelivery> CompanyDeliveries { get; set; }
        public DbSet<CallLog> CallLogs { get; set; }
        public DbSet<CompanyCall> CompanyCalls { get; set; }
        public DbSet<CompanyDriver> CompanyDrivers { get; set; }
        public DbSet<DriverVehicle> DriverVehicles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
    }
}