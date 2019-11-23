﻿// <auto-generated />
using System;
using API.StartApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.StartApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API.Base.Files.Models.Entities.FileEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<string>("Extension");

                    b.Property<string>("Name");

                    b.Property<string>("OriginalName");

                    b.Property<string>("Path");

                    b.Property<bool>("Protected");

                    b.Property<int>("Size");

                    b.Property<string>("SubDirectory");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("File");
                });

            modelBuilder.Entity("API.Base.Logging.Models.Entities.LogsAuditEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthInfo")
                        .HasMaxLength(1024);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Data")
                        .HasMaxLength(10240);

                    b.Property<string>("Info")
                        .HasMaxLength(10240);

                    b.Property<string>("Ip");

                    b.Property<int>("Level");

                    b.Property<string>("RequestBody")
                        .HasMaxLength(10240);

                    b.Property<string>("RequestUri")
                        .HasMaxLength(512);

                    b.Property<int>("ResponseDuration");

                    b.Property<string>("Result")
                        .HasMaxLength(10240);

                    b.Property<string>("Tag");

                    b.Property<string>("TraceIdentifier")
                        .HasMaxLength(128);

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("TraceIdentifier");

                    b.ToTable("LogsAudit");
                });

            modelBuilder.Entity("API.Base.Logging.Models.Entities.LogsUiEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<string>("AuthorId");

                    b.Property<string>("Controller");

                    b.Property<DateTime>("Created");

                    b.Property<string>("NewVersion");

                    b.Property<string>("OldVersion");

                    b.Property<string>("TargetId");

                    b.Property<string>("TraceIdentifier")
                        .HasMaxLength(128);

                    b.Property<DateTime>("Updated");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Id");

                    b.HasIndex("TraceIdentifier");

                    b.ToTable("LogsUi");
                });

            modelBuilder.Entity("API.Base.Web.Base.Auth.Models.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AuthRole");
                });

            modelBuilder.Entity("API.Base.Web.Base.Auth.Models.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("LoginToken");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AuthUser");
                });

            modelBuilder.Entity("API.Base.Web.Common.Models.Entities.SettingEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<string>("FileId");

                    b.Property<string>("Slug");

                    b.Property<int>("Type");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("Id");

                    b.HasIndex("Slug");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("API.Base.Web.Common.Models.Entities.TemplateEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<string>("Slug")
                        .IsRequired();

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Slug");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("API.Base.Web.Common.Models.Entities.TranslationEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<string>("Language")
                        .IsRequired();

                    b.Property<string>("Slug")
                        .IsRequired();

                    b.Property<DateTime>("Updated");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Language");

                    b.HasIndex("Slug");

                    b.ToTable("Translation");
                });

            modelBuilder.Entity("API.StartApp.Example.ExampleEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.Property<int>("OrderIndex");

                    b.Property<string>("Slug");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("OrderIndex");

                    b.HasIndex("Slug");

                    b.ToTable("Example");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AuthUserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("iTEC.App.Address.AddressEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("Created");

                    b.Property<float>("LocationLat");

                    b.Property<float>("LocationLong");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("iTEC.App.Category.CategoryEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.Property<string>("ParentId");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("iTEC.App.Product.ProductCategory.ProductCategoryEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("ProductId");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("iTEC.App.Product.ProductEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("AvailableUnits");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<float>("Price");

                    b.Property<string>("SellerId");

                    b.Property<int>("Unit");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("iTEC.App.Product.ProductPhoto.ProductPhotoEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("FileId");

                    b.Property<bool>("IsThumbnail");

                    b.Property<string>("ProductId");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPhoto");
                });

            modelBuilder.Entity("iTEC.App.Profile.BuyerProfile.BuyerProfileEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("Type");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BuyerProfile");
                });

            modelBuilder.Entity("iTEC.App.Profile.SellerProfile.SellerProfileEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("TargetType");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SellerProfile");
                });

            modelBuilder.Entity("API.Base.Logging.Models.Entities.LogsUiEntity", b =>
                {
                    b.HasOne("API.Base.Web.Base.Auth.Models.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("API.Base.Web.Common.Models.Entities.SettingEntity", b =>
                {
                    b.HasOne("API.Base.Files.Models.Entities.FileEntity", "File")
                        .WithMany()
                        .HasForeignKey("FileId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("API.Base.Web.Base.Auth.Models.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("API.Base.Web.Base.Auth.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("API.Base.Web.Base.Auth.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("API.Base.Web.Base.Auth.Models.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Base.Web.Base.Auth.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("API.Base.Web.Base.Auth.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iTEC.App.Category.CategoryEntity", b =>
                {
                    b.HasOne("iTEC.App.Category.CategoryEntity", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("iTEC.App.Product.ProductCategory.ProductCategoryEntity", b =>
                {
                    b.HasOne("iTEC.App.Category.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("iTEC.App.Product.ProductEntity", "Product")
                        .WithMany("Categories")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("iTEC.App.Product.ProductEntity", b =>
                {
                    b.HasOne("iTEC.App.Profile.SellerProfile.SellerProfileEntity", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");
                });

            modelBuilder.Entity("iTEC.App.Product.ProductPhoto.ProductPhotoEntity", b =>
                {
                    b.HasOne("API.Base.Files.Models.Entities.FileEntity", "File")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.HasOne("iTEC.App.Product.ProductEntity", "Product")
                        .WithMany("Photos")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("iTEC.App.Profile.BuyerProfile.BuyerProfileEntity", b =>
                {
                    b.HasOne("iTEC.App.Address.AddressEntity", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("API.Base.Web.Base.Auth.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("iTEC.App.Profile.SellerProfile.SellerProfileEntity", b =>
                {
                    b.HasOne("iTEC.App.Address.AddressEntity", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("API.Base.Web.Base.Auth.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
