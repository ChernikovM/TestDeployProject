using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SS.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackLabels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    RecCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StickerPacks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Count = table.Column<long>(type: "bigint", nullable: true),
                    SourceLink = table.Column<string>(type: "text", nullable: true),
                    TelegramLink = table.Column<string>(type: "text", nullable: true),
                    RecCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StickerPacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackLabelStickerPack",
                columns: table => new
                {
                    LabelsId = table.Column<long>(type: "bigint", nullable: false),
                    StickerPacksId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackLabelStickerPack", x => new { x.LabelsId, x.StickerPacksId });
                    table.ForeignKey(
                        name: "FK_PackLabelStickerPack_PackLabels_LabelsId",
                        column: x => x.LabelsId,
                        principalTable: "PackLabels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackLabelStickerPack_StickerPacks_StickerPacksId",
                        column: x => x.StickerPacksId,
                        principalTable: "StickerPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackLabelStickerPack_StickerPacksId",
                table: "PackLabelStickerPack",
                column: "StickerPacksId");

            migrationBuilder.CreateIndex(
                name: "IX_StickerPacks_TelegramLink",
                table: "StickerPacks",
                column: "TelegramLink",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackLabelStickerPack");

            migrationBuilder.DropTable(
                name: "PackLabels");

            migrationBuilder.DropTable(
                name: "StickerPacks");
        }
    }
}