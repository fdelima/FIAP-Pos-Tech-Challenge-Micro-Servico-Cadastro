using FIAP.Pos.Tech.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.Infra.Mappings;

internal class DispositivoMap : IEntityTypeConfiguration<Dispositivo>
{
    public void Configure(EntityTypeBuilder<Dispositivo> builder)
    {
        builder.HasKey(e => e.IdDispositivo);

        builder.ToTable("dispositivo");

        builder.Property(e => e.IdDispositivo)
            .ValueGeneratedNever()
            .HasColumnName("id_dispositivo");
        builder.Property(e => e.Identificador)
            .HasMaxLength(100)
            .HasColumnName("identificador");
        builder.Property(e => e.Modelo)
            .HasMaxLength(50)
            .HasColumnName("modelo");
        builder.Property(e => e.Serie)
            .HasMaxLength(50)
            .HasColumnName("serie");
    }
}
