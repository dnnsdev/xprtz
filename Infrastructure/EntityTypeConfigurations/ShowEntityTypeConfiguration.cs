using System.ComponentModel.DataAnnotations.Schema;
using Domain.Shows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

/// <summary>
/// EntityTypeConfiguration for Show.
/// </summary>
/// <remarks>Property configs are estimated for now</remarks>
public sealed class ShowEntityTypeConfiguration : IEntityTypeConfiguration<Show>
{
    public void Configure(EntityTypeBuilder<Show> builder)
    {
        builder.Property(show => show.Id).IsRequired().ValueGeneratedNever();
        builder.Property(show => show.Name).IsRequired().HasMaxLength(250);
        builder.Property(show => show.Language).IsRequired().HasMaxLength(100);
        builder.Property(show => show.Premiered);
        builder.Property(show => show.Summary).HasMaxLength(10000);
        builder.Property(show => show.Genres);

        builder.HasIndex(show => show.Name);
        builder.HasIndex(show => show.Id);
    }
}
