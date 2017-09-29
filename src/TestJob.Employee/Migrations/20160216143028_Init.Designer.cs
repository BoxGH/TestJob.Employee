using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using TestJob.Employee.Models;

namespace TestJob.Employee.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20160216143028_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestJob.Employee.Models.Workman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("Status");

                    b.Property<decimal>("Tasso");

                    b.Property<decimal>("Tax");

                    b.Property<int>("TaxRate");

                    b.Property<decimal>("Wages");

                    b.HasKey("Id");
                });
        }
    }
}
