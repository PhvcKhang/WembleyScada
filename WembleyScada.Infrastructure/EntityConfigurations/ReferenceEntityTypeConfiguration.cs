namespace WembleyScada.Infrastructure.EntityConfigurations;

public class ReferenceEntityTypeConfiguration : IEntityTypeConfiguration<Reference>
{
    public void Configure(EntityTypeBuilder<Reference> builder)
    {
        builder.HasKey(r => r.ReferenceId);
        builder.Property(r => r.ReferenceId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasIndex(r => r.ReferenceName)
            .IsUnique();

        builder.HasMany(r => r.Lots)
            .WithOne()
            .HasForeignKey(l => l.ReferenceId);

        builder.HasMany(r => r.UsableLines)
            .WithMany();
    }
}
