using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.DAL.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 9, 17, 10, 32, 58, 877, DateTimeKind.Unspecified).AddTicks(2058), new TimeSpan(0, 0, 0, 0, 0))),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    Culture = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Domain = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 9, 17, 10, 32, 58, 874, DateTimeKind.Unspecified).AddTicks(2825), new TimeSpan(0, 0, 0, 0, 0))),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    SiteId = table.Column<Guid>(nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    ApiStatus = table.Column<string>(nullable: true),
                    ApiMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participation_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FailedTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 9, 17, 10, 32, 58, 867, DateTimeKind.Unspecified).AddTicks(1603), new TimeSpan(0, 0, 0, 0, 0))),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ParticipationId = table.Column<Guid>(nullable: true),
                    TermsConsent = table.Column<bool>(nullable: false),
                    NewsletterOptin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailedTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FailedTransaction_Participation_ParticipationId",
                        column: x => x.ParticipationId,
                        principalTable: "Participation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 9, 17, 10, 32, 58, 871, DateTimeKind.Unspecified).AddTicks(2386), new TimeSpan(0, 0, 0, 0, 0))),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    EmailHash = table.Column<string>(maxLength: 450, nullable: false),
                    ParticipationId = table.Column<Guid>(nullable: true),
                    ConsumerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participant_Participation_ParticipationId",
                        column: x => x.ParticipationId,
                        principalTable: "Participation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FailedTransaction_ParticipationId",
                table: "FailedTransaction",
                column: "ParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_EmailHash",
                table: "Participant",
                column: "EmailHash");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_ParticipationId",
                table: "Participant",
                column: "ParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_SiteId",
                table: "Participation",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_Status",
                table: "Participation",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Site_Culture",
                table: "Site",
                column: "Culture");

            migrationBuilder.CreateIndex(
                name: "IX_Site_Domain",
                table: "Site",
                column: "Domain");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FailedTransaction");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Participation");

            migrationBuilder.DropTable(
                name: "Site");
        }
    }
}
