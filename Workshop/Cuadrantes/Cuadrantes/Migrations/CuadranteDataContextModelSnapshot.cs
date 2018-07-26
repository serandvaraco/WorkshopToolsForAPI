﻿// <auto-generated />
using Cuadrantes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Cuadrantes.Migrations
{
    [DbContext(typeof(CuadranteDataContext))]
    partial class CuadranteDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cuadrantes.Model.InformacionPolicial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UbicacionCuadranteId");

                    b.HasKey("Id");

                    b.HasIndex("UbicacionCuadranteId");

                    b.ToTable("InformacionPolicial");
                });

            modelBuilder.Entity("Cuadrantes.Model.InformacionUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cedula");

                    b.Property<string>("Clave");

                    b.Property<string>("Correo");

                    b.Property<DateTime>("FechaExpedicion");

                    b.Property<string>("Nombre");

                    b.Property<string>("Telefono");

                    b.HasKey("Id");

                    b.ToTable("InformacionUsuario");
                });

            modelBuilder.Entity("Cuadrantes.Model.UbicacionAlarmas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Latitud");

                    b.Property<string>("Longitud");

                    b.Property<int>("UbicacionCuadranteId");

                    b.HasKey("Id");

                    b.HasIndex("UbicacionCuadranteId");

                    b.ToTable("UbicacionAlarmas");
                });

            modelBuilder.Entity("Cuadrantes.Model.UbicacionCuadrante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Latitud");

                    b.Property<string>("Longitud");

                    b.Property<string>("Nombre");

                    b.Property<string>("NumeroCuadrante");

                    b.HasKey("Id");

                    b.ToTable("UbicacionCuadrantes");
                });

            modelBuilder.Entity("Cuadrantes.Model.InformacionPolicial", b =>
                {
                    b.HasOne("Cuadrantes.Model.UbicacionCuadrante", "Cuadrante")
                        .WithMany("InformacionPolicial")
                        .HasForeignKey("UbicacionCuadranteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Cuadrantes.Model.UbicacionAlarmas", b =>
                {
                    b.HasOne("Cuadrantes.Model.UbicacionCuadrante", "UbicacionCuadrante")
                        .WithMany("UbicacionAlarmas")
                        .HasForeignKey("UbicacionCuadranteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
