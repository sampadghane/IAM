﻿// <auto-generated />
using AccessManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccessManagement.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20250130065308_this1")]
    partial class this1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("AccessManagement.Models.AuditLog", b =>
                {
                    b.Property<string>("TimeStamp")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("action")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TimeStamp");

                    b.ToTable("logs");
                });

            modelBuilder.Entity("AccessManagement.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("AccessManagement.Models.UserEmail", b =>
                {
                    b.Property<string>("username")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("otp")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("username");

                    b.ToTable("userEmail");
                });
#pragma warning restore 612, 618
        }
    }
}
