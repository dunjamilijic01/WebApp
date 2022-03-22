﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Projekat_WEB.Migrations
{
    [DbContext(typeof(KantinaContext))]
    [Migration("20220320190826_V7")]
    partial class V7
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Models.Jelo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<string>("NazivJela")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Restoran")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Jelo");
                });

            modelBuilder.Entity("Models.Kantina", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("NazivKantine")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Kantina");
                });

            modelBuilder.Entity("Models.Meni", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Dan")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("JeloNaMenijuID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("JeloNaMenijuID");

                    b.ToTable("Meni");
                });

            modelBuilder.Entity("Models.Porudzbina", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("JeloPorudzbineID")
                        .HasColumnType("int");

                    b.Property<int?>("PorudzbinaZaID")
                        .HasColumnType("int");

                    b.Property<bool>("PreuzetaPorudzbina")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("JeloPorudzbineID");

                    b.HasIndex("PorudzbinaZaID");

                    b.ToTable("Porudzbina");
                });

            modelBuilder.Entity("Models.Radnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("kantinaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("kantinaID");

                    b.ToTable("Radnik");
                });

            modelBuilder.Entity("Models.Meni", b =>
                {
                    b.HasOne("Models.Jelo", "JeloNaMeniju")
                        .WithMany("ListaMenija")
                        .HasForeignKey("JeloNaMenijuID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JeloNaMeniju");
                });

            modelBuilder.Entity("Models.Porudzbina", b =>
                {
                    b.HasOne("Models.Jelo", "JeloPorudzbine")
                        .WithMany("JeloUPorudzbinama")
                        .HasForeignKey("JeloPorudzbineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Radnik", "PorudzbinaZa")
                        .WithMany("Porudzbine")
                        .HasForeignKey("PorudzbinaZaID");

                    b.Navigation("JeloPorudzbine");

                    b.Navigation("PorudzbinaZa");
                });

            modelBuilder.Entity("Models.Radnik", b =>
                {
                    b.HasOne("Models.Kantina", "kantina")
                        .WithMany("ListaRadnika")
                        .HasForeignKey("kantinaID");

                    b.Navigation("kantina");
                });

            modelBuilder.Entity("Models.Jelo", b =>
                {
                    b.Navigation("JeloUPorudzbinama");

                    b.Navigation("ListaMenija");
                });

            modelBuilder.Entity("Models.Kantina", b =>
                {
                    b.Navigation("ListaRadnika");
                });

            modelBuilder.Entity("Models.Radnik", b =>
                {
                    b.Navigation("Porudzbine");
                });
#pragma warning restore 612, 618
        }
    }
}
