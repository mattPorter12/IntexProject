using Intex.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Intex.DAL
{
    public class NorthwestContext : DbContext
    {
        public NorthwestContext() : base("NorthwestContext") {}

        public DbSet<Assay> Assays { get; set; }
        public DbSet<AssayMaterial> AssayMaterials { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Compound> Compounds { get; set; }
        public DbSet<CompoundStatus> CompoundStatuses { get; set; }
        public DbSet<CondtionalTest> ConditionalTests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<OrderCompound> OrderCompounds { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentInfo> PaymentInfo { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<TestMaterial> TestMaterials { get; set; }
        public DbSet<WorkOrders> WorkOrders { get; set; }

    }
}