using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddDonHangSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TinhTrangDonHang = table.Column<int>(type: "int", nullable: false),
                    NguoiNhanHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiGiao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false) 
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.MaDh);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    MaHH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    GiamGia = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => new { x.MaDh, x.MaHH });
                    table.ForeignKey(
                        name: "FK_CTDH_DonHang",
                        column: x => x.MaDh,
                        principalTable: "DonHang",
                        principalColumn: "MaDh",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTDH_HangHoa",
                        column: x => x.MaHH,
                        principalTable: "HangHoa",
                        principalColumn: "MaHH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaHH",
                table: "ChiTietDonHang",
                column: "MaHH");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "DonHang");
        }
    }
}
