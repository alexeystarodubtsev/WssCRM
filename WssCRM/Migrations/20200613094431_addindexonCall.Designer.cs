﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WssCRM.DBModels;

namespace WssCRM.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200613094431_addindexonCall")]
    partial class addindexonCall
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WssCRM.DBModels.AbstractPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StageID");

                    b.Property<bool>("deleted");

                    b.Property<int>("maxMark");

                    b.Property<string>("name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("StageID");

                    b.HasIndex("name", "StageID")
                        .IsUnique();

                    b.ToTable("AbstractPoints");
                });

            modelBuilder.Entity("WssCRM.DBModels.Call", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientLink");

                    b.Property<string>("ClientName")
                        .IsRequired();

                    b.Property<string>("ClientState")
                        .IsRequired();

                    b.Property<string>("Correction")
                        .IsRequired();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime?>("DateNext");

                    b.Property<DateTime?>("DateOfClose");

                    b.Property<int>("ManagerID");

                    b.Property<int>("StageID");

                    b.Property<string>("correctioncolor");

                    b.Property<TimeSpan>("duration");

                    b.HasKey("Id");

                    b.HasIndex("StageID");

                    b.HasIndex("Date", "ClientName", "Correction", "duration", "ManagerID", "StageID")
                        .IsUnique();

                    b.ToTable("Calls");
                });

            modelBuilder.Entity("WssCRM.DBModels.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("name")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("WssCRM.DBModels.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyID");

                    b.Property<bool>("deleted");

                    b.Property<string>("name");

                    b.HasKey("Id");

                    b.HasIndex("CompanyID");

                    b.HasIndex("name", "CompanyID")
                        .IsUnique()
                        .HasFilter("[name] IS NOT NULL");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("WssCRM.DBModels.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AbstractPointID");

                    b.Property<int>("CallID");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CallID");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("WssCRM.DBModels.Stage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("deleted");

                    b.HasKey("Id");

                    b.HasIndex("CompanyID");

                    b.HasIndex("Name", "CompanyID")
                        .IsUnique();

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("WssCRM.DBModels.AbstractPoint", b =>
                {
                    b.HasOne("WssCRM.DBModels.Stage")
                        .WithMany("Points")
                        .HasForeignKey("StageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WssCRM.DBModels.Call", b =>
                {
                    b.HasOne("WssCRM.DBModels.Stage")
                        .WithMany("Calls")
                        .HasForeignKey("StageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WssCRM.DBModels.Manager", b =>
                {
                    b.HasOne("WssCRM.DBModels.Company")
                        .WithMany("Managers")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WssCRM.DBModels.Point", b =>
                {
                    b.HasOne("WssCRM.DBModels.Call")
                        .WithMany("Points")
                        .HasForeignKey("CallID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WssCRM.DBModels.Stage", b =>
                {
                    b.HasOne("WssCRM.DBModels.Company")
                        .WithMany("Stages")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}