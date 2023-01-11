﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StockQuoteChat.Infrastructure;

#nullable disable

namespace StockQuoteChat.Infrastructure.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20230110052555_Add_Bot_User_Seed")]
    partial class AddBotUserSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StockQuoteChat.Application.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<long>("Timestamp")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "RoomId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("StockQuoteChat.Application.Entities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e087c588-23c6-4831-aaf5-fe4e0fe2d37e"),
                            Name = "Room One"
                        },
                        new
                        {
                            Id = new Guid("3d47ff74-c701-4c5e-ad96-8c5dba9452a7"),
                            Name = "Room Two"
                        });
                });

            modelBuilder.Entity("StockQuoteChat.Application.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7ee0312b-172f-4cd5-a95a-102dd8e1aa0e"),
                            Email = "userone@email.com",
                            FirstName = "ChatUserOne",
                            LastName = "",
                            Password = "123"
                        },
                        new
                        {
                            Id = new Guid("5663a1bb-32a6-4456-97b6-d7af18a46f4b"),
                            Email = "usertwo@email.com",
                            FirstName = "ChatUserTwo",
                            LastName = "",
                            Password = "123"
                        },
                        new
                        {
                            Id = new Guid("8f97f319-9056-4a5c-a3a5-7599f07c28e6"),
                            Email = "mychatbot@chatmail.com",
                            FirstName = "Bot",
                            LastName = "",
                            Password = "botexamplepassword"
                        });
                });

            modelBuilder.Entity("StockQuoteChat.Application.Entities.UserRoom", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("UserRooms");
                });

            modelBuilder.Entity("StockQuoteChat.Application.Entities.Message", b =>
                {
                    b.HasOne("StockQuoteChat.Application.Entities.UserRoom", "UserRoom")
                        .WithMany("Messages")
                        .HasForeignKey("UserId", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRoom");
                });

            modelBuilder.Entity("StockQuoteChat.Application.Entities.UserRoom", b =>
                {
                    b.HasOne("StockQuoteChat.Application.Entities.Room", "Room")
                        .WithMany("UserRooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockQuoteChat.Application.Entities.User", "User")
                        .WithMany("UserRooms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockQuoteChat.Application.Entities.Room", b =>
                {
                    b.Navigation("UserRooms");
                });

            modelBuilder.Entity("StockQuoteChat.Application.Entities.User", b =>
                {
                    b.Navigation("UserRooms");
                });

            modelBuilder.Entity("StockQuoteChat.Application.Entities.UserRoom", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
