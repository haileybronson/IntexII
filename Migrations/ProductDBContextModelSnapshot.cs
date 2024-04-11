﻿// <auto-generated />
using System;
using IntexII.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntexII.Migrations
{
    [DbContext(typeof(ProductDBContext))]
    partial class ProductDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IntexII.Models.Orders", b =>
                {
                    b.Property<int>("transaction_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("transaction_Id"));

                    b.Property<int?>("amount")
                        .HasColumnType("int");

                    b.Property<string>("bank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country_of_transaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("customer_Id")
                        .HasColumnType("int");

                    b.Property<string>("date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("day_of_week")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("entry_mode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fraud")
                        .HasColumnType("int");

                    b.Property<string>("shipping_address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("time")
                        .HasColumnType("int");

                    b.Property<string>("type_of_card")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type_of_transaction")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("transaction_Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("IntexII.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<string>("img_Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("num_Parts")
                        .HasColumnType("int");

                    b.Property<string>("primary_Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("secondary_Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("IntexII.Models.ProductRecommendations", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("recommendation_1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("recommendation_2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("recommendation_3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("recommendation_4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("recommendation_5")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("ProductRecommendations");
                });

            modelBuilder.Entity("IntexII.Models.UserRecommendations", b =>
                {
                    b.Property<int>("user_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_ID"));

                    b.Property<string>("recommendation_1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("recommendation_2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("recommendation_3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("recommendation_4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("recommendation_5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("selected_product_ID")
                        .HasColumnType("int");

                    b.HasKey("user_ID");

                    b.ToTable("UserRecommendations");
                });
#pragma warning restore 612, 618
        }
    }
}
