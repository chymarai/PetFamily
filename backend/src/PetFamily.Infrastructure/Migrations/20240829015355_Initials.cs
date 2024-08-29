using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "volunteer",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    FULL_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    YEARS_OF_EXPERIENCE = table.Column<int>(type: "integer", nullable: false),
                    COUNT_OF_SHELTER_ANIMALS = table.Column<int>(type: "integer", nullable: false),
                    COUNT_OF_HOMELESS_ANIMALS = table.Column<int>(type: "integer", nullable: false),
                    COUNT_OF_ILL_ANIMALS = table.Column<int>(type: "integer", nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOLUNTEER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "pet",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TYPE = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    BREED = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    COLOR = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    HEALTH_INFORMATION = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ADDRESS = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    WEIGHT = table.Column<int>(type: "integer", nullable: false),
                    HEIGHT = table.Column<int>(type: "integer", nullable: false),
                    PHONE_NUMBER = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    IS_CASTRATED = table.Column<bool>(type: "boolean", nullable: false),
                    IS_VACCINATION = table.Column<bool>(type: "boolean", nullable: false),
                    ASSISTANCE_STATUS = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BIRTH_DATE = table.Column<DateOnly>(type: "date", nullable: false),
                    DATE_OF_CREATION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VOLUNTEER_ID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PET", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PET_VOLUNTEERS_VOLUNTEER_ID",
                        column: x => x.VOLUNTEER_ID,
                        principalTable: "volunteer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SOCIAL_NETWORK",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    NAME = table.Column<string>(type: "text", nullable: false),
                    URL = table.Column<string>(type: "text", nullable: false),
                    VOLUNTEER_ID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOCIAL_NETWORK", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SOCIAL_NETWORK_VOLUNTEERS_VOLUNTEER_ID",
                        column: x => x.VOLUNTEER_ID,
                        principalTable: "volunteer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "petphoto",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    STORAGE = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IS_MAIN = table.Column<bool>(type: "boolean", nullable: false),
                    PET_ID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PETPHOTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PETPHOTO_PET_PET_ID",
                        column: x => x.PET_ID,
                        principalTable: "pet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REQUISITE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    NAME = table.Column<string>(type: "text", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "text", nullable: false),
                    PET_ID = table.Column<Guid>(type: "uuid", nullable: true),
                    VOLUNTEER_ID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUISITE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REQUISITE_PET_PET_ID",
                        column: x => x.PET_ID,
                        principalTable: "pet",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_REQUISITE_VOLUNTEERS_VOLUNTEER_ID",
                        column: x => x.VOLUNTEER_ID,
                        principalTable: "volunteer",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PET_VOLUNTEER_ID",
                table: "pet",
                column: "VOLUNTEER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PETPHOTO_PET_ID",
                table: "petphoto",
                column: "PET_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REQUISITE_PET_ID",
                table: "REQUISITE",
                column: "PET_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REQUISITE_VOLUNTEER_ID",
                table: "REQUISITE",
                column: "VOLUNTEER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SOCIAL_NETWORK_VOLUNTEER_ID",
                table: "SOCIAL_NETWORK",
                column: "VOLUNTEER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "petphoto");

            migrationBuilder.DropTable(
                name: "REQUISITE");

            migrationBuilder.DropTable(
                name: "SOCIAL_NETWORK");

            migrationBuilder.DropTable(
                name: "pet");

            migrationBuilder.DropTable(
                name: "volunteer");
        }
    }
}
