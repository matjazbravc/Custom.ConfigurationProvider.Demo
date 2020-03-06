﻿// <auto-generated />
using Custom.Configuration.Provider.Demo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Custom.Configuration.Provider.Demo.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("Custom.Configuration.Provider.Demo.Data.Entities.AppSettingsCustomEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomSettingA")
                        .HasColumnName("CustomSettingA")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("CustomSettingB")
                        .HasColumnName("CustomSettingB")
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("Default")
                        .HasColumnName("Default")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("AppCustomSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
