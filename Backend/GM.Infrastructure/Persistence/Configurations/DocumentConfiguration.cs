using GM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GM.Infrastructure.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.StoredFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.FileSizeBytes)
            .IsRequired();

        builder.Property(x => x.StoragePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
