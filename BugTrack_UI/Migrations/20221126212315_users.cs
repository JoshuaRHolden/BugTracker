using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackUI.Migrations
{
    /// <inheritdoc />
    public partial class users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45ae3b51-f68f-40cb-a3d7-c8f43c5e327e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "27000426-22a6-4f09-84a8-100fea1dc657", 0, "98577610-fada-4f29-ad6b-ae7726b95fef", "joshua@mvc.tech", false, "Joshua", "Holden", false, null, null, null, null, null, false, "d811b0ff-2cc3-46e1-bae6-8a051f77bb8c", false, "joshua@mvc.tech" });

            migrationBuilder.UpdateData(
                table: "Bug",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 26, 21, 23, 15, 245, DateTimeKind.Local).AddTicks(2246));

            migrationBuilder.UpdateData(
                table: "Bug",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 26, 21, 23, 15, 245, DateTimeKind.Local).AddTicks(2301));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27000426-22a6-4f09-84a8-100fea1dc657");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "45ae3b51-f68f-40cb-a3d7-c8f43c5e327e", 0, "55e4c471-c9bd-4f47-97a9-7fb5bf3feb58", "joshua@mvc.tech", false, "Joshua", "Holden", false, null, null, null, null, null, false, "7330b6fe-1572-42db-8cc8-64d53f9b2960", false, "joshua@mvc.tech" });

            migrationBuilder.UpdateData(
                table: "Bug",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 26, 21, 21, 18, 849, DateTimeKind.Local).AddTicks(6131));

            migrationBuilder.UpdateData(
                table: "Bug",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 26, 21, 21, 18, 849, DateTimeKind.Local).AddTicks(6184));
        }
    }
}
