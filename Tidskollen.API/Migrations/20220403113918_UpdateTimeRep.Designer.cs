// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tidskollen.API.Model;

namespace Tidskollen.API.Migrations
{
    [DbContext(typeof(TidDbContext))]
    [Migration("20220403113918_UpdateTimeRep")]
    partial class UpdateTimeRep
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tidskollen.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            DateOfBirth = new DateTime(1990, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Therese",
                            LastName = "Brorsson"
                        },
                        new
                        {
                            EmployeeId = 2,
                            DateOfBirth = new DateTime(1987, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Julia",
                            LastName = "Karlsson"
                        },
                        new
                        {
                            EmployeeId = 3,
                            DateOfBirth = new DateTime(1985, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Louisa",
                            LastName = "Stark"
                        });
                });

            modelBuilder.Entity("Tidskollen.Models.EmployeeProject", b =>
                {
                    b.Property<int>("EmployeeProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EmployeeProjectId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("EmployeeProjects");
                });

            modelBuilder.Entity("Tidskollen.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            ProjectId = 1,
                            ProjectName = "TomatOdling"
                        },
                        new
                        {
                            ProjectId = 2,
                            ProjectName = "Tulpaner"
                        });
                });

            modelBuilder.Entity("Tidskollen.Models.TimeReport", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<bool>("CheckStatus")
                        .HasColumnType("bit");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeId");

                    b.ToTable("TimeReports");
                });

            modelBuilder.Entity("Tidskollen.Models.EmployeeProject", b =>
                {
                    b.HasOne("Tidskollen.Models.Employee", "Employee")
                        .WithMany("EmployeeProject")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tidskollen.Models.Project", "Project")
                        .WithMany("EmployeeProject")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tidskollen.Models.TimeReport", b =>
                {
                    b.HasOne("Tidskollen.Models.Employee", "Employee")
                        .WithMany("TimeReport")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
