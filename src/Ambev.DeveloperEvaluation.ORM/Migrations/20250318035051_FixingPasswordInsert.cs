using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class FixingPasswordInsert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("c34d2387-e900-4704-9fe4-09bfaa50f0e1"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 995, DateTimeKind.Utc).AddTicks(9986));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("e49c076d-3046-4399-8d41-94cc9bb65dc0"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 995, DateTimeKind.Utc).AddTicks(9983));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("f2495571-1390-411a-9bfd-669185afea64"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 995, DateTimeKind.Utc).AddTicks(9988));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0f038ef4-2b67-4b7b-9454-801ab6581f54"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(150));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("184c3336-c66e-42e5-8738-11cbe96f527a"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(154));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1ce1afda-b45d-41ea-a2f4-3c4554154aa6"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(158));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2555343d-facf-4c33-b8a6-5fad76be36a3"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(149));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4e662887-962a-46d8-9aed-aa6c0efb7e76"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(153));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5d0b27be-eaeb-4483-8960-489e65f2452d"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(155));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("955f7c88-fe46-483b-9411-19ee00271974"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(157));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ccc9047b-23d4-4260-b29e-2b9c0229bf86"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(152));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d7dae433-b37b-41c4-8e86-330fe3259c03"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(156));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("eb938eb0-572a-4ae2-953a-2bd55e1709bc"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(160));

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("019e40fb-fd1a-4edb-a061-b036060c1a64"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(179), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(174) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("1063acfd-e15c-4f6f-8825-ae7f9036e188"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(213), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(212) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("26fe8552-e4ca-4036-8470-9abdd06f48b6"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(208), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(207) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("2edf25e5-a58d-4e3b-b93c-3d1957433b31"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(201), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(200) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("4a93decd-8ce8-4a58-bc82-d159769f9965"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(210), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(209) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("4b263c2b-d9ad-4149-9e41-0ca45977f7f2"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(196), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(194) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("4dfbec1a-adbb-4148-ab8d-4b23d692f549"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(204), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(203) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("60e1e8fd-8346-4210-a01e-8a1ee795435b"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(198), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(197) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("bc70cb2d-e0cd-4b3d-a610-1c101f846926"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(206), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(205) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("e22d970d-a3ce-43ba-85e1-14ced4c267fe"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(215), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(214) });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("62bad870-cf24-4dee-8470-988fb0ee3361"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(344), new DateTime(2025, 3, 14, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(342) });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("6fc34cef-afed-4edb-ab5f-44a1e2eea0a9"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(224), new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(216) });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("a37f6397-3a09-435f-829d-a19c19728a9b"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(321), new DateTime(2025, 3, 16, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(318) });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("acfeb46e-4383-48dc-af5a-9d1d64445042"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(332), new DateTime(2025, 3, 15, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(330) });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("e8765c98-ce78-4090-ae9a-903101d023c1"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(306), new DateTime(2025, 3, 17, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(296) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0af35800-836a-418a-8a44-f9d21b832fa2"),
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(128), "$2a$11$FNkpDRmEjWrMDD2zZdX7BOZQ2z/nYo04bwW3rFxGb8u7/8YHb6cTG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bb6f91f2-1720-4191-9022-24254953fa18"),
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(126), "$2a$11$FNkpDRmEjWrMDD2zZdX7BOZQ2z/nYo04bwW3rFxGb8u7/8YHb6cTG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ffb140dc-53be-48d5-8c6e-5d3f93271bff"),
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 3, 18, 3, 50, 50, 996, DateTimeKind.Utc).AddTicks(124), "$2a$11$FNkpDRmEjWrMDD2zZdX7BOZQ2z/nYo04bwW3rFxGb8u7/8YHb6cTG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("c34d2387-e900-4704-9fe4-09bfaa50f0e1"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4777));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("e49c076d-3046-4399-8d41-94cc9bb65dc0"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4775));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("f2495571-1390-411a-9bfd-669185afea64"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4779));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0f038ef4-2b67-4b7b-9454-801ab6581f54"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4894));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("184c3336-c66e-42e5-8738-11cbe96f527a"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4897));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1ce1afda-b45d-41ea-a2f4-3c4554154aa6"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4901));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2555343d-facf-4c33-b8a6-5fad76be36a3"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4892));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4e662887-962a-46d8-9aed-aa6c0efb7e76"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4896));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5d0b27be-eaeb-4483-8960-489e65f2452d"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4898));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("955f7c88-fe46-483b-9411-19ee00271974"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4900));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ccc9047b-23d4-4260-b29e-2b9c0229bf86"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4895));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d7dae433-b37b-41c4-8e86-330fe3259c03"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4899));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("eb938eb0-572a-4ae2-953a-2bd55e1709bc"),
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4902));

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("019e40fb-fd1a-4edb-a061-b036060c1a64"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4923), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4916) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("1063acfd-e15c-4f6f-8825-ae7f9036e188"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4956), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4955) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("26fe8552-e4ca-4036-8470-9abdd06f48b6"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4952), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4951) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("2edf25e5-a58d-4e3b-b93c-3d1957433b31"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4944), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4943) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("4a93decd-8ce8-4a58-bc82-d159769f9965"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4954), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4953) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("4b263c2b-d9ad-4149-9e41-0ca45977f7f2"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4939), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4937) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("4dfbec1a-adbb-4148-ab8d-4b23d692f549"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4947), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4946) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("60e1e8fd-8346-4210-a01e-8a1ee795435b"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4941), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("bc70cb2d-e0cd-4b3d-a610-1c101f846926"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4949), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4948) });

            migrationBuilder.UpdateData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("e22d970d-a3ce-43ba-85e1-14ced4c267fe"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4959), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4958) });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("62bad870-cf24-4dee-8470-988fb0ee3361"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5121), new DateTime(2025, 3, 13, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5119) });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("6fc34cef-afed-4edb-ab5f-44a1e2eea0a9"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4968), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4959) });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("a37f6397-3a09-435f-829d-a19c19728a9b"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5097), new DateTime(2025, 3, 15, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5094) });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("acfeb46e-4383-48dc-af5a-9d1d64445042"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5109), new DateTime(2025, 3, 14, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5106) });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("e8765c98-ce78-4090-ae9a-903101d023c1"),
                columns: new[] { "CreatedAt", "DateSold" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5082), new DateTime(2025, 3, 16, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5046) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0af35800-836a-418a-8a44-f9d21b832fa2"),
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4874), "senha789" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bb6f91f2-1720-4191-9022-24254953fa18"),
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4872), "senha456" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ffb140dc-53be-48d5-8c6e-5d3f93271bff"),
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4870), "senha123" });
        }
    }
}
