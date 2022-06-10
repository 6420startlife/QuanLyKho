using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    public partial class UpdateDb01062022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kho",
                columns: table => new
                {
                    maKho = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    tenKho = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kho", x => x.maKho);
                });

            migrationBuilder.CreateTable(
                name: "VatTu",
                columns: table => new
                {
                    maVatTu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    tenVatTu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    donViTinh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    anhVatTu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    xuatXu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatTu", x => x.maVatTu);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhap",
                columns: table => new
                {
                    soPhieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ngayLap = table.Column<DateTime>(type: "date", nullable: false),
                    loaiPhieu = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    maKho = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhap", x => x.soPhieu);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_Kho",
                        column: x => x.maKho,
                        principalTable: "Kho",
                        principalColumn: "maKho");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuNhap",
                columns: table => new
                {
                    soPhieu = table.Column<int>(type: "int", nullable: false),
                    maVatTu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    soLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuNhap", x => new { x.soPhieu, x.maVatTu });
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhap_PhieuNhap",
                        column: x => x.soPhieu,
                        principalTable: "PhieuNhap",
                        principalColumn: "soPhieu");
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhap_VatTu",
                        column: x => x.maVatTu,
                        principalTable: "VatTu",
                        principalColumn: "maVatTu");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhap_maVatTu",
                table: "ChiTietPhieuNhap",
                column: "maVatTu");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_maKho",
                table: "PhieuNhap",
                column: "maKho");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietPhieuNhap");

            migrationBuilder.DropTable(
                name: "PhieuNhap");

            migrationBuilder.DropTable(
                name: "VatTu");

            migrationBuilder.DropTable(
                name: "Kho");
        }
    }
}
