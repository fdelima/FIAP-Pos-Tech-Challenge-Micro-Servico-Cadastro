using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Mappings;

internal class ProdutoImagensMap : IEntityTypeConfiguration<ProdutoImagens>
{
    public void Configure(EntityTypeBuilder<ProdutoImagens> builder)
    {
        builder.HasKey(e => e.IdProdutoImagem);

        builder.ToTable("produto_imagens");

        builder.Property(e => e.IdProdutoImagem)
            .ValueGeneratedNever()
            .HasColumnName("id_produto_imagem");
        builder.Property(e => e.IdProduto).HasColumnName("id_produto");
        builder.Property(e => e.ImagemBase64)
            .IsUnicode(false)
            .HasColumnName("imagem_base64");

        builder.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.ProdutoImagens)
            .HasForeignKey(d => d.IdProduto)
            .HasConstraintName("FK_produto_imagens_produto");
    }
}
