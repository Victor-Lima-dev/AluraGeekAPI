﻿// <auto-generated />
using System;
using AluraGeekAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AluraGeekAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AluraGeekAPI.Models.CarrinhoCompra", b =>
                {
                    b.Property<string>("CarrinhoCompraId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CarrinhoCompraId");

                    b.ToTable("CarrinhoCompras");
                });

            modelBuilder.Entity("AluraGeekAPI.Models.CarrinhoItem", b =>
                {
                    b.Property<int>("CarrinhoItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarrinhoItemId"));

                    b.Property<string>("CarrinhoCompraId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("CarrinhoItemId");

                    b.HasIndex("CarrinhoCompraId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("CarrinhoItens");
                });

            modelBuilder.Entity("AluraGeekAPI.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdutoId"));

                    b.Property<string>("Categoria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProdutoId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("AluraGeekAPI.Models.CarrinhoItem", b =>
                {
                    b.HasOne("AluraGeekAPI.Models.CarrinhoCompra", null)
                        .WithMany("CarrinhoItens")
                        .HasForeignKey("CarrinhoCompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AluraGeekAPI.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("AluraGeekAPI.Models.CarrinhoCompra", b =>
                {
                    b.Navigation("CarrinhoItens");
                });
#pragma warning restore 612, 618
        }
    }
}
