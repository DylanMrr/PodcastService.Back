// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PodcastService.Podcast.Api.Data;

namespace PodcastService.Podcast.Api.Migrations
{
    [DbContext(typeof(PodcastContext))]
    partial class PodcastContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PodcastService.Podcast.Api.Data.Podcast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Podcasts");
                });

            modelBuilder.Entity("PodcastService.Podcast.Api.Data.PodcastSubscribers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PodcastId")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PodcastId");

                    b.ToTable("PodcastSubscribers");
                });

            modelBuilder.Entity("PodcastService.Podcast.Api.Data.PodcastSubscribers", b =>
                {
                    b.HasOne("PodcastService.Podcast.Api.Data.Podcast", "Podcast")
                        .WithMany("PodcastSubscribers")
                        .HasForeignKey("PodcastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Podcast");
                });

            modelBuilder.Entity("PodcastService.Podcast.Api.Data.Podcast", b =>
                {
                    b.Navigation("PodcastSubscribers");
                });
#pragma warning restore 612, 618
        }
    }
}
