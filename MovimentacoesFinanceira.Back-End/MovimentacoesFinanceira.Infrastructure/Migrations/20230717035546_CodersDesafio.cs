using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovimentacoesFinanceira.Infrastructure.Migrations
{
    public partial class CodersDesafio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    LojaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeDaLoja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonoDaLoja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.LojaId);
                });

            migrationBuilder.CreateTable(
                name: "TransacoesFinanceira",
                columns: table => new
                {
                    IdTransacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LojaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Data_Ocorrencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<int>(type: "int", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cartao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hora_Ocorrencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonoDaLoja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeDaLoja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacoesFinanceira", x => x.IdTransacao);
                    table.ForeignKey(
                        name: "FK_TransacoesFinanceira_Lojas_LojaId",
                        column: x => x.LojaId,
                        principalTable: "Lojas",
                        principalColumn: "LojaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Lojas",
                columns: new[] { "LojaId", "DonoDaLoja", "NomeDaLoja" },
                values: new object[,]
                {
                    { new Guid("51301cc7-c2fd-408f-a960-ab9b60946a88"), "MARIA JOSEFINA", "LOJA DO Ó - FILIAL" },
                    { new Guid("69697aef-bf41-4d85-b2f6-15e34a93bfb1"), "JOÃO MACEDO   ", "BAR DO JOÃO       " },
                    { new Guid("6aad4635-c182-42a6-96d6-0c3b65fe0aa6"), "JOSÉ COSTA    ", "MERCEARIA 3 IRMÃOS" },
                    { new Guid("a8ccdced-894e-4c31-a8cf-e7079d56a062"), "MARIA JOSEFINA", "LOJA DO Ó - MATRIZ" },
                    { new Guid("ef988cff-0a16-4a8c-bc3b-02a05c3a82ee"), "MARCOS PEREIRA", "MERCADO DA AVENIDA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesFinanceira_LojaId",
                table: "TransacoesFinanceira",
                column: "LojaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransacoesFinanceira");

            migrationBuilder.DropTable(
                name: "Lojas");
        }
    }
}
