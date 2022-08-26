﻿// <auto-generated />
using System;
using CalculateCartMicroservice.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CalculateCartMicroservice.Migrations
{
    [DbContext(typeof(CartDBContext))]
    partial class CartDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CalculateCartMicroservice.Entity.Model.FoodItemDetails", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FoodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long?>("UserOrderDetailsOrderId")
                        .HasColumnType("bigint");

                    b.HasKey("FoodId");

                    b.HasIndex("UserOrderDetailsOrderId");

                    b.ToTable("FoodItemDetails");
                });

            modelBuilder.Entity("CalculateCartMicroservice.Entity.Model.UserOrderDetails", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.ToTable("UserOrderDetails");
                });

            modelBuilder.Entity("CalculateCartMicroservice.Entity.Model.FoodItemDetails", b =>
                {
                    b.HasOne("CalculateCartMicroservice.Entity.Model.UserOrderDetails", null)
                        .WithMany("FoodItemList")
                        .HasForeignKey("UserOrderDetailsOrderId");
                });

            modelBuilder.Entity("CalculateCartMicroservice.Entity.Model.UserOrderDetails", b =>
                {
                    b.Navigation("FoodItemList");
                });
#pragma warning restore 612, 618
        }
    }
}