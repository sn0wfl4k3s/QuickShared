using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickShared.Domain.Entities;
using QuickShared.Infrastructure.Persistance.Abstractions;

namespace QuickShared.Infrastructure.Persistance.Configurations;

internal sealed class ManagerFileConfiguration : BaseEntityConfiguration<ManagerFile>
{
    public override void Configure(EntityTypeBuilder<ManagerFile> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.FileExtension)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.FileUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.FileSize)
            .IsRequired();
    }
}
