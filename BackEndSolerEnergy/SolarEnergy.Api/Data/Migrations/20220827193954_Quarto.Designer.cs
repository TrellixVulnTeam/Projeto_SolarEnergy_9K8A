﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolarEnergy.Api.Data;

#nullable disable

namespace SolarEnergy.Api.Data.Migrations
{
    [DbContext(typeof(SolarDbContext))]
    [Migration("20220827193954_Quarto")]
    partial class Quarto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SolarEnergy.Api.Models.Geracao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("Kw")
                        .HasColumnType("int");

                    b.Property<int>("UnidadeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UnidadeId");

                    b.ToTable("Gerações", (string)null);
                });

            modelBuilder.Entity("SolarEnergy.Api.Models.Unidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apelido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Unidades", (string)null);
                });

            modelBuilder.Entity("SolarEnergy.Api.Models.Geracao", b =>
                {
                    b.HasOne("SolarEnergy.Api.Models.Unidade", "Unidade")
                        .WithMany("Geracoes")
                        .HasForeignKey("UnidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unidade");
                });

            modelBuilder.Entity("SolarEnergy.Api.Models.Unidade", b =>
                {
                    b.Navigation("Geracoes");
                });
#pragma warning restore 612, 618
        }
    }
}