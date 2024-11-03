using FIAP.Pos.Tech.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.Infra.Mappings;

internal class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(e => e.IdProduto);

        builder.ToTable("produto");

        builder.Property(e => e.IdProduto)
            .ValueGeneratedNever()
            .HasColumnName("id_produto");
        builder.Property(e => e.Categoria)
            .HasMaxLength(50)
            .HasColumnName("categoria");
        builder.Property(e => e.Descricao)
            .HasMaxLength(500)
            .HasColumnName("descricao");
        builder.Property(e => e.Nome)
            .HasMaxLength(50)
            .HasColumnName("nome");
        builder.Property(e => e.Preco)
            .HasColumnType("numeric(18, 2)")
            .HasColumnName("preco");
    }
}
