﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SteamData;

#nullable disable

namespace SteamData.Migrations
{
    [DbContext(typeof(SteamContext))]
    [Migration("20230414085943_AddServers")]
    partial class AddServers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CompanyCountry", b =>
                {
                    b.Property<int>("CompaniesCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("CountriesCountryId")
                        .HasColumnType("int");

                    b.HasKey("CompaniesCompanyId", "CountriesCountryId");

                    b.HasIndex("CountriesCountryId");

                    b.ToTable("CompanyCountry");
                });

            modelBuilder.Entity("GameUser", b =>
                {
                    b.Property<int>("GamesGameId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("GamesGameId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("GameUser");
                });

            modelBuilder.Entity("SteamDomain.Account", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmailId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("EmailId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            EmailId = 1,
                            CreationDate = new DateTime(2000, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "dalsyan@email.com",
                            Password = "0001",
                            UserId = 1
                        },
                        new
                        {
                            EmailId = 2,
                            CreationDate = new DateTime(2000, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "tentxten@email.com",
                            Password = "0002",
                            UserId = 2
                        },
                        new
                        {
                            EmailId = 3,
                            CreationDate = new DateTime(2000, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jamonsioo@email.com",
                            Password = "0003",
                            UserId = 3
                        },
                        new
                        {
                            EmailId = 4,
                            CreationDate = new DateTime(2001, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "enricdetu@email.com",
                            Password = "0004",
                            UserId = 4
                        },
                        new
                        {
                            EmailId = 5,
                            CreationDate = new DateTime(2000, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "reisapo@email.com",
                            Password = "0005",
                            UserId = 5
                        });
                });

            modelBuilder.Entity("SteamDomain.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            CompanyId = 1,
                            CompanyName = "Riot Games"
                        },
                        new
                        {
                            CompanyId = 2,
                            CompanyName = "ConcernedApe"
                        },
                        new
                        {
                            CompanyId = 3,
                            CompanyName = "FromSoftware"
                        },
                        new
                        {
                            CompanyId = 4,
                            CompanyName = "Epic Games"
                        },
                        new
                        {
                            CompanyId = 5,
                            CompanyName = "Focus Entertainment"
                        });
                });

            modelBuilder.Entity("SteamDomain.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            CountryId = 1,
                            CountryName = "Spain"
                        },
                        new
                        {
                            CountryId = 2,
                            CountryName = "UK"
                        },
                        new
                        {
                            CountryId = 3,
                            CountryName = "Italy"
                        },
                        new
                        {
                            CountryId = 4,
                            CountryName = "USA"
                        },
                        new
                        {
                            CountryId = 5,
                            CountryName = "China"
                        });
                });

            modelBuilder.Entity("SteamDomain.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameId"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            GameId = 1,
                            CompanyId = 1,
                            Gender = "MOBA",
                            Title = "League of Legends"
                        },
                        new
                        {
                            GameId = 2,
                            CompanyId = 4,
                            Gender = "Shooter",
                            Title = "Fortnite"
                        },
                        new
                        {
                            GameId = 3,
                            CompanyId = 5,
                            Gender = "Rol",
                            Title = "Call of Cthulhu"
                        },
                        new
                        {
                            GameId = 4,
                            CompanyId = 2,
                            Gender = "Simulator",
                            Title = "Stardew Valley"
                        });
                });

            modelBuilder.Entity("SteamDomain.Server", b =>
                {
                    b.Property<int>("ServerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServerId"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.HasKey("ServerId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CountryId");

                    b.HasIndex("GameId");

                    b.ToTable("Server");
                });

            modelBuilder.Entity("SteamDomain.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("CountryId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            CountryId = 1,
                            Nickname = "dalsyan"
                        },
                        new
                        {
                            UserId = 2,
                            CountryId = 1,
                            Nickname = "Tentxten"
                        },
                        new
                        {
                            UserId = 3,
                            CountryId = 1,
                            Nickname = "Jamonsioo"
                        },
                        new
                        {
                            UserId = 4,
                            CountryId = 1,
                            Nickname = "EnricDeTu"
                        },
                        new
                        {
                            UserId = 5,
                            CountryId = 1,
                            Nickname = "ReiSapo"
                        });
                });

            modelBuilder.Entity("CompanyCountry", b =>
                {
                    b.HasOne("SteamDomain.Company", null)
                        .WithMany()
                        .HasForeignKey("CompaniesCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SteamDomain.Country", null)
                        .WithMany()
                        .HasForeignKey("CountriesCountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameUser", b =>
                {
                    b.HasOne("SteamDomain.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SteamDomain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SteamDomain.Account", b =>
                {
                    b.HasOne("SteamDomain.User", "User")
                        .WithOne("EmailId")
                        .HasForeignKey("SteamDomain.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SteamDomain.Game", b =>
                {
                    b.HasOne("SteamDomain.Company", null)
                        .WithMany("Games")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("SteamDomain.Server", b =>
                {
                    b.HasOne("SteamDomain.Company", null)
                        .WithMany("Servers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SteamDomain.Country", null)
                        .WithMany("Servers")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SteamDomain.Game", null)
                        .WithMany("Servers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SteamDomain.User", b =>
                {
                    b.HasOne("SteamDomain.Country", null)
                        .WithMany("Users")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SteamDomain.Company", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("Servers");
                });

            modelBuilder.Entity("SteamDomain.Country", b =>
                {
                    b.Navigation("Servers");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SteamDomain.Game", b =>
                {
                    b.Navigation("Servers");
                });

            modelBuilder.Entity("SteamDomain.User", b =>
                {
                    b.Navigation("EmailId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
