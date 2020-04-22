﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SFF_API.Context;

namespace SFF_API.Migrations
{
    [DbContext(typeof(SFFEntitiesContext))]
    partial class SFFEntitiesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("SFF_API.Models.FilmClubModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FilmClubs");
                });

            modelBuilder.Entity("SFF_API.Models.MovieModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Genre")
                        .HasColumnType("TEXT");

                    b.Property<int>("RentalLimit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("SFF_API.Models.RatingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<int>("FilmClubModelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieModelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RentalModelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FilmClubModelId");

                    b.HasIndex("MovieModelId");

                    b.HasIndex("RentalModelId")
                        .IsUnique();

                    b.ToTable("MovieRatings");
                });

            modelBuilder.Entity("SFF_API.Models.RentalModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FilmClubModelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieModelId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RentalActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RentalDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FilmClubModelId");

                    b.HasIndex("MovieModelId");

                    b.ToTable("RentalLog");
                });

            modelBuilder.Entity("SFF_API.Models.TriviaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FilmClubModelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieModelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RentalModelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Trivia")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FilmClubModelId");

                    b.HasIndex("MovieModelId");

                    b.HasIndex("RentalModelId")
                        .IsUnique();

                    b.ToTable("MovieTrivias");
                });

            modelBuilder.Entity("SFF_API.Models.RatingModel", b =>
                {
                    b.HasOne("SFF_API.Models.FilmClubModel", "FilmClub")
                        .WithMany()
                        .HasForeignKey("FilmClubModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFF_API.Models.MovieModel", null)
                        .WithMany("Ratings")
                        .HasForeignKey("MovieModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFF_API.Models.RentalModel", "Rental")
                        .WithOne("Rating")
                        .HasForeignKey("SFF_API.Models.RatingModel", "RentalModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SFF_API.Models.RentalModel", b =>
                {
                    b.HasOne("SFF_API.Models.FilmClubModel", "FilmClub")
                        .WithMany("Rentals")
                        .HasForeignKey("FilmClubModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFF_API.Models.MovieModel", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SFF_API.Models.TriviaModel", b =>
                {
                    b.HasOne("SFF_API.Models.FilmClubModel", "FilmClub")
                        .WithMany()
                        .HasForeignKey("FilmClubModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFF_API.Models.MovieModel", null)
                        .WithMany("Trivias")
                        .HasForeignKey("MovieModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFF_API.Models.RentalModel", "Rental")
                        .WithOne("Trivia")
                        .HasForeignKey("SFF_API.Models.TriviaModel", "RentalModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
