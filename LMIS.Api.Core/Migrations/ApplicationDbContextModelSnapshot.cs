﻿// <auto-generated />
using System;
using LMIS.Api.Core.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LMIS.Api.Core.Model.ApplicationUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Pin")
                        .HasColumnType("integer");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("applicationUsers");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.Book", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CopyNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ObtainedThrough")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.BookInventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BookId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("bookInventories");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.CheckoutTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BookId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MemberId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("bookInventoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("MemberId");

                    b.HasIndex("UserId");

                    b.HasIndex("bookInventoryId");

                    b.ToTable("checkoutTransactions");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GenreId"));

                    b.Property<int>("MaximumBooksAllowed")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GenreId");

                    b.ToTable("genres");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MemberId"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MemberTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Member_Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("MemberId");

                    b.HasIndex("MemberTypeId")
                        .IsUnique();

                    b.HasIndex("userId");

                    b.ToTable("members");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.MemberType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("memberTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8403),
                            Name = "Student"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8405),
                            Name = "Staff"
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8406),
                            Name = "Regular Member"
                        },
                        new
                        {
                            Id = 4,
                            CreatedOn = new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8407),
                            Name = "Premium Member"
                        },
                        new
                        {
                            Id = 5,
                            CreatedOn = new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8407),
                            Name = "Guest"
                        },
                        new
                        {
                            Id = 6,
                            CreatedOn = new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8408),
                            Name = "Senior Citezen"
                        },
                        new
                        {
                            Id = 7,
                            CreatedOn = new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8409),
                            Name = "Corparate Member"
                        });
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.UserRole", b =>
                {
                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.Property<int>("roleId")
                        .HasColumnType("integer");

                    b.Property<int>("userRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("userRoleId"));

                    b.HasKey("userId", "roleId");

                    b.HasIndex("roleId");

                    b.ToTable("userRoles");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.BookInventory", b =>
                {
                    b.HasOne("LMIS.Api.Core.Model.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.CheckoutTransaction", b =>
                {
                    b.HasOne("LMIS.Api.Core.Model.Book", "book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMIS.Api.Core.Model.Member", "member")
                        .WithMany("checkoutTransactions")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMIS.Api.Core.Model.ApplicationUser", "user")
                        .WithMany("checkoutTransactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMIS.Api.Core.Model.BookInventory", "bookInventory")
                        .WithMany("checkoutTransactions")
                        .HasForeignKey("bookInventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("book");

                    b.Navigation("bookInventory");

                    b.Navigation("member");

                    b.Navigation("user");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.Member", b =>
                {
                    b.HasOne("LMIS.Api.Core.Model.MemberType", "memberType")
                        .WithOne()
                        .HasForeignKey("LMIS.Api.Core.Model.Member", "MemberTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMIS.Api.Core.Model.ApplicationUser", "user")
                        .WithMany("members")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("memberType");

                    b.Navigation("user");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.UserRole", b =>
                {
                    b.HasOne("LMIS.Api.Core.Model.Role", "Role")
                        .WithMany("userRoles")
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMIS.Api.Core.Model.ApplicationUser", "User")
                        .WithMany("userRoles")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.ApplicationUser", b =>
                {
                    b.Navigation("checkoutTransactions");

                    b.Navigation("members");

                    b.Navigation("userRoles");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.BookInventory", b =>
                {
                    b.Navigation("checkoutTransactions");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.Member", b =>
                {
                    b.Navigation("checkoutTransactions");
                });

            modelBuilder.Entity("LMIS.Api.Core.Model.Role", b =>
                {
                    b.Navigation("userRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
