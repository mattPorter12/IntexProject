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

        public DbSet<Assay> Assay { get; set; }
        public DbSet<AssayMaterial> AssayMaterial { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Compound> Compound { get; set; }
        public DbSet<CompoundStatus> CompoundStatus { get; set; }
        public DbSet<CondtionalTest> ConditionalTest { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<OrderCompound> OrderCompound { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<PaymentInfo> PaymentInfo { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<TestMaterial> TestMaterial { get; set; }
        public DbSet<WorkOrders> WorkOrder { get; set; }

    }
}