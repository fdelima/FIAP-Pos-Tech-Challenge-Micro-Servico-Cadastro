using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Mappings;

internal class ClienteMap : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(e => e.IdCliente);

        builder.ToTable("cliente");

        builder.Property(e => e.IdCliente)
                .ValueGeneratedNever()
                .HasColumnName("id_cliente");
        builder.Property(e => e.Cpf).HasColumnName("cpf");
        builder.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
        builder.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
    }
}
