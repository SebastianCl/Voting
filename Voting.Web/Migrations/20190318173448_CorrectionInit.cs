using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Web.Migrations
{
    public partial class CorrectionInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Candidates_CandidateId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CandidateId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Candidates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_EventId",
                table: "Candidates",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Events_EventId",
                table: "Candidates",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Events_EventId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_EventId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_CandidateId",
                table: "Events",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Candidates_CandidateId",
                table: "Events",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
