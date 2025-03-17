using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddingDefaultItens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("b641ba94-31ab-44b6-bcaa-455a38f37fd1"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("c33f2593-6f48-4c34-8278-2c899fc9a99c"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("f5d7a7fb-df50-4711-a476-25254f6d414a"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("08a461f5-7b38-4b7a-b919-1e1d73cada5b"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("0db29710-71c4-4e26-b249-79311f02126b"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("9f9bea4b-2ed3-4c6f-a4c9-165ab6f4d287"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("a8c7297b-61d5-4b72-818f-d773afd3d93d"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("b3f9690d-cf52-4c2b-9ec1-25dc1c0150b5"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("b7e72c9a-3477-4069-b61e-1fa118ef1a87"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("c27556a9-c8b2-4cd1-83cf-543b821ce93f"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("ed5682c2-309d-4a4a-a5dc-3c6700606319"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("f8c72b27-0c67-4792-a2f3-da5497d98f7f"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("fa88d16f-1de4-41fc-829e-ec1de64cea59"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("31147cf1-5468-4864-a52e-da62ff037d51"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87b9759b-9488-44ae-9528-17884c611ebd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b18deef9-9539-4bbe-bf56-fa0922ed7f0b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("42b30f27-259d-4610-b5d4-0044ae09b52d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("76bf7d3e-4c71-4a48-9ced-60ae26eacf3c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8690e783-adea-476e-969a-140993dc1800"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("894ba2c9-f0ce-4b59-9e7e-4384392a804d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a18c559a-3ff8-4a2c-a176-b62183198d5d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a69fe058-c26c-40fd-8f21-e873c934f90d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cf2a7cfa-eb37-439f-9437-ce2f53cf4b0b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dfb1bd3d-223a-4ec2-b59a-d251ed375856"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e0da5c4e-bfc9-47db-87f1-332710e010c2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f25f98ec-6123-4f15-a4ee-011ad09ba585"));

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("3a9fabb9-5a7e-421b-af1e-4a61ee5bbd5d"));

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("ad906992-789c-4633-97cf-c813c3774852"));

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("eacebeb9-6782-4227-b801-7a3865c07944"));

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("f3982878-e530-45a8-9c03-442183915b62"));

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("f429c942-39f9-462c-bb65-10f98678e80a"));

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("c34d2387-e900-4704-9fe4-09bfaa50f0e1"), "Florianópolis - SC, Trindade, nº 195", new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4777), "Filial Florianópolis", null },
                    { new Guid("e49c076d-3046-4399-8d41-94cc9bb65dc0"), "São Paulo - SP, Bairro Velha Guarda, nº 60", new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4775), "Filial São Paulo", null },
                    { new Guid("f2495571-1390-411a-9bfd-669185afea64"), "Curitiba - PR, Centro, nº 300", new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4779), "Filial Curitiba", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0f038ef4-2b67-4b7b-9454-801ab6581f54"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4894), "Feijão preto, pacote de 1kg", "Feijão Preto", null },
                    { new Guid("184c3336-c66e-42e5-8738-11cbe96f527a"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4897), "Leite integral, caixa de 1L", "Leite Integral", null },
                    { new Guid("1ce1afda-b45d-41ea-a2f4-3c4554154aa6"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4901), "Desinfetante aroma pinho, frasco de 2L", "Desinfetante", null },
                    { new Guid("2555343d-facf-4c33-b8a6-5fad76be36a3"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4892), "Arroz integral orgânico, pacote de 5kg", "Arroz Integral", null },
                    { new Guid("4e662887-962a-46d8-9aed-aa6c0efb7e76"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4896), "Macarrão espaguete, pacote de 500g", "Macarrão Espaguete", null },
                    { new Guid("5d0b27be-eaeb-4483-8960-489e65f2452d"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4898), "Café em grãos torrado, pacote de 1kg", "Café em Grãos", null },
                    { new Guid("955f7c88-fe46-483b-9411-19ee00271974"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4900), "Papel higiênico folha dupla, pacote com 12 rolos", "Papel Higiênico", null },
                    { new Guid("ccc9047b-23d4-4260-b29e-2b9c0229bf86"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4895), "Azeite extra virgem, garrafa de 500ml", "Azeite de Oliva", null },
                    { new Guid("d7dae433-b37b-41c4-8e86-330fe3259c03"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4899), "Sabão em pó para roupas, pacote de 2kg", "Sabão em Pó", null },
                    { new Guid("eb938eb0-572a-4ae2-953a-2bd55e1709bc"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4902), "Detergente líquido neutro, frasco de 500ml", "Detergente Líquido", null }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "BranchId", "ClientId", "CreatedAt", "DateSold", "Number", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("62bad870-cf24-4dee-8470-988fb0ee3361"), new Guid("c34d2387-e900-4704-9fe4-09bfaa50f0e1"), new Guid("bb6f91f2-1720-4191-9022-24254953fa18"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5121), new DateTime(2025, 3, 13, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5119), "SALE005", 1, null },
                    { new Guid("6fc34cef-afed-4edb-ab5f-44a1e2eea0a9"), new Guid("e49c076d-3046-4399-8d41-94cc9bb65dc0"), new Guid("ffb140dc-53be-48d5-8c6e-5d3f93271bff"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4968), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4959), "SALE001", 1, null },
                    { new Guid("a37f6397-3a09-435f-829d-a19c19728a9b"), new Guid("f2495571-1390-411a-9bfd-669185afea64"), new Guid("0af35800-836a-418a-8a44-f9d21b832fa2"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5097), new DateTime(2025, 3, 15, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5094), "SALE003", 1, null },
                    { new Guid("acfeb46e-4383-48dc-af5a-9d1d64445042"), new Guid("e49c076d-3046-4399-8d41-94cc9bb65dc0"), new Guid("ffb140dc-53be-48d5-8c6e-5d3f93271bff"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5109), new DateTime(2025, 3, 14, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5106), "SALE004", 1, null },
                    { new Guid("e8765c98-ce78-4090-ae9a-903101d023c1"), new Guid("c34d2387-e900-4704-9fe4-09bfaa50f0e1"), new Guid("bb6f91f2-1720-4191-9022-24254953fa18"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5082), new DateTime(2025, 3, 16, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(5046), "SALE002", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Phone", "Role", "Status", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("0af35800-836a-418a-8a44-f9d21b832fa2"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4874), "carlos.oliveira@example.com", "senha789", "11955556666", "Customer", "Active", null, "carlos.oliveira" },
                    { new Guid("bb6f91f2-1720-4191-9022-24254953fa18"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4872), "maria.souza@example.com", "senha456", "11912345678", "Manager", "Inactive", null, "maria.souza" },
                    { new Guid("ffb140dc-53be-48d5-8c6e-5d3f93271bff"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4870), "joao.silva@example.com", "senha123", "11987654321", "Admin", "Active", null, "joao.silva" }
                });

            migrationBuilder.InsertData(
                table: "SaleProducts",
                columns: new[] { "Id", "CreatedAt", "DateSold", "FixedDiscount", "PercentageDiscount", "ProductId", "Quantity", "SaleId", "TotalCost", "TotalCostWithDiscount", "TotalDiscount", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("019e40fb-fd1a-4edb-a061-b036060c1a64"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4923), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4916), 0m, 0.10m, new Guid("2555343d-facf-4c33-b8a6-5fad76be36a3"), 5, new Guid("6fc34cef-afed-4edb-ab5f-44a1e2eea0a9"), 125.00m, 112.5000m, 12.5000m, 25.00m, null },
                    { new Guid("1063acfd-e15c-4f6f-8825-ae7f9036e188"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4956), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4955), 0m, 0.10m, new Guid("1ce1afda-b45d-41ea-a2f4-3c4554154aa6"), 5, new Guid("62bad870-cf24-4dee-8470-988fb0ee3361"), 50.00m, 45.0000m, 5.0000m, 10.00m, null },
                    { new Guid("26fe8552-e4ca-4036-8470-9abdd06f48b6"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4952), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4951), 0m, 0.20m, new Guid("d7dae433-b37b-41c4-8e86-330fe3259c03"), 10, new Guid("acfeb46e-4383-48dc-af5a-9d1d64445042"), 150.00m, 120.0000m, 30.0000m, 15.00m, null },
                    { new Guid("2edf25e5-a58d-4e3b-b93c-3d1957433b31"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4944), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4943), 0m, 0.20m, new Guid("4e662887-962a-46d8-9aed-aa6c0efb7e76"), 20, new Guid("e8765c98-ce78-4090-ae9a-903101d023c1"), 100.00m, 80.0000m, 20.0000m, 5.00m, null },
                    { new Guid("4a93decd-8ce8-4a58-bc82-d159769f9965"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4954), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4953), 0m, 0m, new Guid("955f7c88-fe46-483b-9411-19ee00271974"), 3, new Guid("acfeb46e-4383-48dc-af5a-9d1d64445042"), 60.00m, 60.00m, 0.00m, 20.00m, null },
                    { new Guid("4b263c2b-d9ad-4149-9e41-0ca45977f7f2"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4939), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4937), 0m, 0.20m, new Guid("0f038ef4-2b67-4b7b-9454-801ab6581f54"), 10, new Guid("6fc34cef-afed-4edb-ab5f-44a1e2eea0a9"), 80.00m, 64.0000m, 16.0000m, 8.00m, null },
                    { new Guid("4dfbec1a-adbb-4148-ab8d-4b23d692f549"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4947), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4946), 0m, 0.20m, new Guid("184c3336-c66e-42e5-8738-11cbe96f527a"), 14, new Guid("a37f6397-3a09-435f-829d-a19c19728a9b"), 63.00m, 50.4000m, 12.6000m, 4.50m, null },
                    { new Guid("60e1e8fd-8346-4210-a01e-8a1ee795435b"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4941), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4940), 0m, 0m, new Guid("ccc9047b-23d4-4260-b29e-2b9c0229bf86"), 2, new Guid("e8765c98-ce78-4090-ae9a-903101d023c1"), 60.00m, 60.00m, 0.00m, 30.00m, null },
                    { new Guid("bc70cb2d-e0cd-4b3d-a610-1c101f846926"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4949), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4948), 0m, 0.20m, new Guid("5d0b27be-eaeb-4483-8960-489e65f2452d"), 11, new Guid("a37f6397-3a09-435f-829d-a19c19728a9b"), 550.00m, 440.0000m, 110.0000m, 50.00m, null },
                    { new Guid("e22d970d-a3ce-43ba-85e1-14ced4c267fe"), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4959), new DateTime(2025, 3, 17, 2, 18, 23, 720, DateTimeKind.Utc).AddTicks(4958), 0m, 0m, new Guid("eb938eb0-572a-4ae2-953a-2bd55e1709bc"), 2, new Guid("62bad870-cf24-4dee-8470-988fb0ee3361"), 10.00m, 10.00m, 0.00m, 5.00m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("c34d2387-e900-4704-9fe4-09bfaa50f0e1"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("e49c076d-3046-4399-8d41-94cc9bb65dc0"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("f2495571-1390-411a-9bfd-669185afea64"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("019e40fb-fd1a-4edb-a061-b036060c1a64"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("1063acfd-e15c-4f6f-8825-ae7f9036e188"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("26fe8552-e4ca-4036-8470-9abdd06f48b6"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("2edf25e5-a58d-4e3b-b93c-3d1957433b31"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("4a93decd-8ce8-4a58-bc82-d159769f9965"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("4b263c2b-d9ad-4149-9e41-0ca45977f7f2"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("4dfbec1a-adbb-4148-ab8d-4b23d692f549"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("60e1e8fd-8346-4210-a01e-8a1ee795435b"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("bc70cb2d-e0cd-4b3d-a610-1c101f846926"));

            migrationBuilder.DeleteData(
                table: "SaleProducts",
                keyColumn: "Id",
                keyValue: new Guid("e22d970d-a3ce-43ba-85e1-14ced4c267fe"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0af35800-836a-418a-8a44-f9d21b832fa2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bb6f91f2-1720-4191-9022-24254953fa18"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ffb140dc-53be-48d5-8c6e-5d3f93271bff"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0f038ef4-2b67-4b7b-9454-801ab6581f54"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("184c3336-c66e-42e5-8738-11cbe96f527a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1ce1afda-b45d-41ea-a2f4-3c4554154aa6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2555343d-facf-4c33-b8a6-5fad76be36a3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4e662887-962a-46d8-9aed-aa6c0efb7e76"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5d0b27be-eaeb-4483-8960-489e65f2452d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("955f7c88-fe46-483b-9411-19ee00271974"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ccc9047b-23d4-4260-b29e-2b9c0229bf86"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d7dae433-b37b-41c4-8e86-330fe3259c03"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("eb938eb0-572a-4ae2-953a-2bd55e1709bc"));

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("62bad870-cf24-4dee-8470-988fb0ee3361"));

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("6fc34cef-afed-4edb-ab5f-44a1e2eea0a9"));

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("a37f6397-3a09-435f-829d-a19c19728a9b"));

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("acfeb46e-4383-48dc-af5a-9d1d64445042"));

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: new Guid("e8765c98-ce78-4090-ae9a-903101d023c1"));

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b641ba94-31ab-44b6-bcaa-455a38f37fd1"), "Florianópolis - SC, Trindade, nº 195", new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6870), "Filial Florianópolis", null },
                    { new Guid("c33f2593-6f48-4c34-8278-2c899fc9a99c"), "São Paulo - SP, Bairro Velha Guarda, nº 60", new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6868), "Filial São Paulo", null },
                    { new Guid("f5d7a7fb-df50-4711-a476-25254f6d414a"), "Curitiba - PR, Centro, nº 300", new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6871), "Filial Curitiba", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("42b30f27-259d-4610-b5d4-0044ae09b52d"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6979), "Arroz integral orgânico, pacote de 5kg", "Arroz Integral", null },
                    { new Guid("76bf7d3e-4c71-4a48-9ced-60ae26eacf3c"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6987), "Papel higiênico folha dupla, pacote com 12 rolos", "Papel Higiênico", null },
                    { new Guid("8690e783-adea-476e-969a-140993dc1800"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6985), "Café em grãos torrado, pacote de 1kg", "Café em Grãos", null },
                    { new Guid("894ba2c9-f0ce-4b59-9e7e-4384392a804d"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6984), "Leite integral, caixa de 1L", "Leite Integral", null },
                    { new Guid("a18c559a-3ff8-4a2c-a176-b62183198d5d"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6991), "Detergente líquido neutro, frasco de 500ml", "Detergente Líquido", null },
                    { new Guid("a69fe058-c26c-40fd-8f21-e873c934f90d"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6989), "Desinfetante aroma pinho, frasco de 2L", "Desinfetante", null },
                    { new Guid("cf2a7cfa-eb37-439f-9437-ce2f53cf4b0b"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6986), "Sabão em pó para roupas, pacote de 2kg", "Sabão em Pó", null },
                    { new Guid("dfb1bd3d-223a-4ec2-b59a-d251ed375856"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6980), "Feijão preto, pacote de 1kg", "Feijão Preto", null },
                    { new Guid("e0da5c4e-bfc9-47db-87f1-332710e010c2"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6982), "Azeite extra virgem, garrafa de 500ml", "Azeite de Oliva", null },
                    { new Guid("f25f98ec-6123-4f15-a4ee-011ad09ba585"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6983), "Macarrão espaguete, pacote de 500g", "Macarrão Espaguete", null }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "BranchId", "ClientId", "CreatedAt", "DateSold", "Number", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3a9fabb9-5a7e-421b-af1e-4a61ee5bbd5d"), new Guid("c33f2593-6f48-4c34-8278-2c899fc9a99c"), new Guid("87b9759b-9488-44ae-9528-17884c611ebd"), new DateTime(2025, 3, 14, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7031), new DateTime(2025, 3, 14, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7030), "SALE004", 1, null },
                    { new Guid("ad906992-789c-4633-97cf-c813c3774852"), new Guid("b641ba94-31ab-44b6-bcaa-455a38f37fd1"), new Guid("31147cf1-5468-4864-a52e-da62ff037d51"), new DateTime(2025, 3, 13, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7033), new DateTime(2025, 3, 13, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7032), "SALE005", 1, null },
                    { new Guid("eacebeb9-6782-4227-b801-7a3865c07944"), new Guid("b641ba94-31ab-44b6-bcaa-455a38f37fd1"), new Guid("31147cf1-5468-4864-a52e-da62ff037d51"), new DateTime(2025, 3, 16, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7026), new DateTime(2025, 3, 16, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7021), "SALE002", 1, null },
                    { new Guid("f3982878-e530-45a8-9c03-442183915b62"), new Guid("f5d7a7fb-df50-4711-a476-25254f6d414a"), new Guid("b18deef9-9539-4bbe-bf56-fa0922ed7f0b"), new DateTime(2025, 3, 15, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7029), new DateTime(2025, 3, 15, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7028), "SALE003", 1, null },
                    { new Guid("f429c942-39f9-462c-bb65-10f98678e80a"), new Guid("c33f2593-6f48-4c34-8278-2c899fc9a99c"), new Guid("87b9759b-9488-44ae-9528-17884c611ebd"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7019), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7018), "SALE001", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Phone", "Role", "Status", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("31147cf1-5468-4864-a52e-da62ff037d51"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6957), "maria.souza@example.com", "senha456", "11912345678", "Manager", "Inactive", null, "maria.souza" },
                    { new Guid("87b9759b-9488-44ae-9528-17884c611ebd"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6955), "joao.silva@example.com", "senha123", "11987654321", "Admin", "Active", null, "joao.silva" },
                    { new Guid("b18deef9-9539-4bbe-bf56-fa0922ed7f0b"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(6959), "carlos.oliveira@example.com", "senha789", "11955556666", "Customer", "Active", null, "carlos.oliveira" }
                });

            migrationBuilder.InsertData(
                table: "SaleProducts",
                columns: new[] { "Id", "CreatedAt", "DateSold", "FixedDiscount", "PercentageDiscount", "ProductId", "Quantity", "SaleId", "TotalCost", "TotalCostWithDiscount", "TotalDiscount", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("08a461f5-7b38-4b7a-b919-1e1d73cada5b"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7097), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7095), 0m, 0m, new Guid("dfb1bd3d-223a-4ec2-b59a-d251ed375856"), 10, new Guid("f429c942-39f9-462c-bb65-10f98678e80a"), 80.00m, 80.00m, 0.00m, 8.00m, null },
                    { new Guid("0db29710-71c4-4e26-b249-79311f02126b"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7119), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7118), 0m, 0m, new Guid("a18c559a-3ff8-4a2c-a176-b62183198d5d"), 2, new Guid("ad906992-789c-4633-97cf-c813c3774852"), 10.00m, 10.00m, 0.00m, 5.00m, null },
                    { new Guid("9f9bea4b-2ed3-4c6f-a4c9-165ab6f4d287"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7114), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7113), 0m, 0m, new Guid("76bf7d3e-4c71-4a48-9ced-60ae26eacf3c"), 3, new Guid("3a9fabb9-5a7e-421b-af1e-4a61ee5bbd5d"), 60.00m, 60.00m, 0.00m, 20.00m, null },
                    { new Guid("a8c7297b-61d5-4b72-818f-d773afd3d93d"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7100), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7098), 0m, 0m, new Guid("e0da5c4e-bfc9-47db-87f1-332710e010c2"), 2, new Guid("eacebeb9-6782-4227-b801-7a3865c07944"), 60.00m, 60.00m, 0.00m, 30.00m, null },
                    { new Guid("b3f9690d-cf52-4c2b-9ec1-25dc1c0150b5"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7106), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7105), 0m, 0m, new Guid("894ba2c9-f0ce-4b59-9e7e-4384392a804d"), 14, new Guid("f3982878-e530-45a8-9c03-442183915b62"), 63.00m, 63.00m, 0.00m, 4.50m, null },
                    { new Guid("b7e72c9a-3477-4069-b61e-1fa118ef1a87"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7078), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7072), 0m, 0m, new Guid("42b30f27-259d-4610-b5d4-0044ae09b52d"), 5, new Guid("f429c942-39f9-462c-bb65-10f98678e80a"), 125.00m, 125.00m, 0.00m, 25.00m, null },
                    { new Guid("c27556a9-c8b2-4cd1-83cf-543b821ce93f"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7104), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7102), 0m, 0m, new Guid("f25f98ec-6123-4f15-a4ee-011ad09ba585"), 20, new Guid("eacebeb9-6782-4227-b801-7a3865c07944"), 100.00m, 100.00m, 0.00m, 5.00m, null },
                    { new Guid("ed5682c2-309d-4a4a-a5dc-3c6700606319"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7117), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7116), 0m, 0m, new Guid("a69fe058-c26c-40fd-8f21-e873c934f90d"), 5, new Guid("ad906992-789c-4633-97cf-c813c3774852"), 50.00m, 50.00m, 0.00m, 10.00m, null },
                    { new Guid("f8c72b27-0c67-4792-a2f3-da5497d98f7f"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7112), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7110), 0m, 0m, new Guid("cf2a7cfa-eb37-439f-9437-ce2f53cf4b0b"), 10, new Guid("3a9fabb9-5a7e-421b-af1e-4a61ee5bbd5d"), 150.00m, 150.00m, 0.00m, 15.00m, null },
                    { new Guid("fa88d16f-1de4-41fc-829e-ec1de64cea59"), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7109), new DateTime(2025, 3, 17, 1, 52, 38, 691, DateTimeKind.Utc).AddTicks(7108), 0m, 0m, new Guid("8690e783-adea-476e-969a-140993dc1800"), 11, new Guid("f3982878-e530-45a8-9c03-442183915b62"), 550.00m, 550.00m, 0.00m, 50.00m, null }
                });
        }
    }
}
