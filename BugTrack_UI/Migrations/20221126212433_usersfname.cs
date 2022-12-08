using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackUI.Migrations
{
    /// <inheritdoc />
    public partial class usersfname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27000426-22a6-4f09-84a8-100fea1dc657");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6216efcf-5183-4f2d-9076-e95e677f76ae", 0, "8d998e74-91d7-4ba6-b5cc-401b8e6075f5", "joshua@mvc.tech", false, "Joshua", "Holden", false, null, null, null, null, null, false, "0fed20c3-3837-49f1-9d15-ae14f53de360", false, "joshua@mvc.tech" });

            migrationBuilder.UpdateData(
                table: "Bug",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 26, 21, 24, 33, 724, DateTimeKind.Local).AddTicks(3510));

            migrationBuilder.UpdateData(
                table: "Bug",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 26, 21, 24, 33, 724, DateTimeKind.Local).AddTicks(3565));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6216efcf-5183-4f2d-9076-e95e677f76ae");

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
    }
}