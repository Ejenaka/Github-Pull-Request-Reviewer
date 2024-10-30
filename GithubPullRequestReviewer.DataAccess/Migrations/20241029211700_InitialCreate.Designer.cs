﻿// <auto-generated />
using System;
using GithubPullRequestReviewer.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GithubPullRequestReviewer.DataAccess.Migrations
{
    [DbContext(typeof(PullRequestReviewerDbContext))]
    [Migration("20241029211700_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GithubPullRequestReviewer.DataAccess.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int[]>("CodeLines")
                        .HasColumnType("integer[]");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ParentCommentId")
                        .HasColumnType("integer");

                    b.Property<int?>("PullRequestId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("PullRequestId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("GithubPullRequestReviewer.DataAccess.Entities.PullRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RepositoryId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.ToTable("PullRequests");
                });

            modelBuilder.Entity("GithubPullRequestReviewer.DataAccess.Entities.PullRequestIssue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CommentId")
                        .HasColumnType("integer");

                    b.Property<int>("IssueType")
                        .HasColumnType("integer");

                    b.Property<int>("IssueTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("PullRequestId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("PullRequestId");

                    b.ToTable("PullRequestIssues");
                });

            modelBuilder.Entity("GithubPullRequestReviewer.DataAccess.Entities.Repository", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Repositories");
                });

            modelBuilder.Entity("GithubPullRequestReviewer.DataAccess.Entities.Comment", b =>
                {
                    b.HasOne("GithubPullRequestReviewer.DataAccess.Entities.Comment", "ParentComment")
                        .WithMany()
                        .HasForeignKey("ParentCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GithubPullRequestReviewer.DataAccess.Entities.PullRequest", null)
                        .WithMany("Comments")
                        .HasForeignKey("PullRequestId");

                    b.Navigation("ParentComment");
                });

            modelBuilder.Entity("GithubPullRequestReviewer.DataAccess.Entities.PullRequest", b =>
                {
                    b.HasOne("GithubPullRequestReviewer.DataAccess.Entities.Repository", "Repository")
                        .WithMany()
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("GithubPullRequestReviewer.DataAccess.Entities.PullRequestIssue", b =>
                {
                    b.HasOne("GithubPullRequestReviewer.DataAccess.Entities.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GithubPullRequestReviewer.DataAccess.Entities.PullRequest", "PullRequest")
                        .WithMany()
                        .HasForeignKey("PullRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("PullRequest");
                });

            modelBuilder.Entity("GithubPullRequestReviewer.DataAccess.Entities.PullRequest", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
