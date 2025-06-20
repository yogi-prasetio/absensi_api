﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using absensi_api.Data;

#nullable disable

namespace absensi_api.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("absensi_api.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<int?>("PositionId")
                        .HasColumnType("int")
                        .HasColumnName("position_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("otp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountId");

                    b.HasIndex("PositionId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("absensi_api.Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("activity_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityId"));

                    b.Property<int>("AttendanceLogId")
                        .HasColumnType("int")
                        .HasColumnName("attendance_log_id");

                    b.Property<string>("activity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("end_time")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("start_time")
                        .HasColumnType("time");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("ActivityId");

                    b.HasIndex("AttendanceLogId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("absensi_api.Models.AttendanceLog", b =>
                {
                    b.Property<int>("AttendanceLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("attendance_log_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceLogId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    b.Property<int?>("CheckInLogId")
                        .HasColumnType("int")
                        .HasColumnName("check_in_log_id");

                    b.Property<int?>("CheckOutLogId")
                        .HasColumnType("int")
                        .HasColumnName("check_out_log_id");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("AttendanceLogId");

                    b.HasIndex("AccountId");

                    b.HasIndex("CheckInLogId")
                        .IsUnique()
                        .HasFilter("[check_in_log_id] IS NOT NULL");

                    b.HasIndex("CheckOutLogId")
                        .IsUnique()
                        .HasFilter("[check_out_log_id] IS NOT NULL");

                    b.ToTable("AttendanceLogs");
                });

            modelBuilder.Entity("absensi_api.Models.CheckInLog", b =>
                {
                    b.Property<int>("CheckInLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("check_in_log_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CheckInLogId"));

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("time_in")
                        .HasColumnType("time");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("CheckInLogId");

                    b.ToTable("CheckInLogs");
                });

            modelBuilder.Entity("absensi_api.Models.CheckOutLog", b =>
                {
                    b.Property<int>("CheckOutLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("check_out_log_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CheckOutLogId"));

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("time_out")
                        .HasColumnType("time");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("CheckOutLogId");

                    b.ToTable("CheckOutLogs");
                });

            modelBuilder.Entity("absensi_api.Models.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("position_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PositionId"));

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("PositionId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("absensi_api.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("role_description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("absensi_api.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nik")
                        .HasColumnType("int");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("absensi_api.Models.Account", b =>
                {
                    b.HasOne("absensi_api.Models.Position", "Position")
                        .WithMany("Accounts")
                        .HasForeignKey("PositionId");

                    b.HasOne("absensi_api.Models.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("absensi_api.Models.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("absensi_api.Models.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("absensi_api.Models.Activity", b =>
                {
                    b.HasOne("absensi_api.Models.AttendanceLog", "CheckInLog")
                        .WithMany("Activities")
                        .HasForeignKey("AttendanceLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckInLog");
                });

            modelBuilder.Entity("absensi_api.Models.AttendanceLog", b =>
                {
                    b.HasOne("absensi_api.Models.Account", "Account")
                        .WithMany("AttendanceLogs")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("absensi_api.Models.CheckInLog", "CheckInLog")
                        .WithOne("AttendanceLog")
                        .HasForeignKey("absensi_api.Models.AttendanceLog", "CheckInLogId");

                    b.HasOne("absensi_api.Models.CheckOutLog", "CheckOutLog")
                        .WithOne("AttendanceLog")
                        .HasForeignKey("absensi_api.Models.AttendanceLog", "CheckOutLogId");

                    b.Navigation("Account");

                    b.Navigation("CheckInLog");

                    b.Navigation("CheckOutLog");
                });

            modelBuilder.Entity("absensi_api.Models.Account", b =>
                {
                    b.Navigation("AttendanceLogs");
                });

            modelBuilder.Entity("absensi_api.Models.AttendanceLog", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("absensi_api.Models.CheckInLog", b =>
                {
                    b.Navigation("AttendanceLog");
                });

            modelBuilder.Entity("absensi_api.Models.CheckOutLog", b =>
                {
                    b.Navigation("AttendanceLog");
                });

            modelBuilder.Entity("absensi_api.Models.Position", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("absensi_api.Models.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("absensi_api.Models.User", b =>
                {
                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
