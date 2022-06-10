﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyKho.Data;

#nullable disable

namespace QuanLyKho.Migrations
{
    [DbContext(typeof(QuanLyKhoContext))]
    [Migration("20220601170646_UpdateDb01062022")]
    partial class UpdateDb01062022
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QuanLyKho.Models.ChiTietPhieuNhap", b =>
                {
                    b.Property<int>("soPhieu")
                        .HasColumnType("int")
                        .HasColumnName("soPhieu");

                    b.Property<string>("maVatTu")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("maVatTu");

                    b.Property<int>("soLuong")
                        .HasColumnType("int")
                        .HasColumnName("soLuong");

                    b.HasKey("soPhieu", "maVatTu");

                    b.HasIndex("maVatTu");

                    b.ToTable("ChiTietPhieuNhap");
                });

            modelBuilder.Entity("QuanLyKho.Models.Kho", b =>
                {
                    b.Property<string>("maKho")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("maKho");

                    b.Property<string>("tenKho")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("tenKho");

                    b.HasKey("maKho");

                    b.ToTable("Kho");
                });

            modelBuilder.Entity("QuanLyKho.Models.PhieuNhap", b =>
                {
                    b.Property<int>("soPhieu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("soPhieu");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("soPhieu"), 1L, 1);

                    b.Property<string>("loaiPhieu")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("loaiPhieu");

                    b.Property<string>("maKho")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("maKho");

                    b.Property<DateTime>("ngayLap")
                        .HasColumnType("date")
                        .HasColumnName("ngayLap");

                    b.HasKey("soPhieu");

                    b.HasIndex("maKho");

                    b.ToTable("PhieuNhap");
                });

            modelBuilder.Entity("QuanLyKho.Models.VatTu", b =>
                {
                    b.Property<string>("maVatTu")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("maVatTu");

                    b.Property<string>("anhVatTu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("donViTinh")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("donViTinh");

                    b.Property<string>("tenVatTu")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("tenVatTu");

                    b.Property<string>("xuatXu")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("xuatXu");

                    b.HasKey("maVatTu");

                    b.ToTable("VatTu");
                });

            modelBuilder.Entity("QuanLyKho.Models.ChiTietPhieuNhap", b =>
                {
                    b.HasOne("QuanLyKho.Models.VatTu", "vatTuNavigation")
                        .WithMany("list_chiTietPhieuNhap")
                        .HasForeignKey("maVatTu")
                        .IsRequired()
                        .HasConstraintName("FK_ChiTietPhieuNhap_VatTu");

                    b.HasOne("QuanLyKho.Models.PhieuNhap", "phieuNhapNavigation")
                        .WithMany("list_chiTietPhieuNhap")
                        .HasForeignKey("soPhieu")
                        .IsRequired()
                        .HasConstraintName("FK_ChiTietPhieuNhap_PhieuNhap");

                    b.Navigation("phieuNhapNavigation");

                    b.Navigation("vatTuNavigation");
                });

            modelBuilder.Entity("QuanLyKho.Models.PhieuNhap", b =>
                {
                    b.HasOne("QuanLyKho.Models.Kho", "khoNavigation")
                        .WithMany("list_phieuNhap")
                        .HasForeignKey("maKho")
                        .IsRequired()
                        .HasConstraintName("FK_PhieuNhap_Kho");

                    b.Navigation("khoNavigation");
                });

            modelBuilder.Entity("QuanLyKho.Models.Kho", b =>
                {
                    b.Navigation("list_phieuNhap");
                });

            modelBuilder.Entity("QuanLyKho.Models.PhieuNhap", b =>
                {
                    b.Navigation("list_chiTietPhieuNhap");
                });

            modelBuilder.Entity("QuanLyKho.Models.VatTu", b =>
                {
                    b.Navigation("list_chiTietPhieuNhap");
                });
#pragma warning restore 612, 618
        }
    }
}
