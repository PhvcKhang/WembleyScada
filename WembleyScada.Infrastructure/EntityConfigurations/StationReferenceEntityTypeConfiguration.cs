namespace WembleyScada.Infrastructure.EntityConfigurations;

public class StationReferenceEntityTypeConfiguration : IEntityTypeConfiguration<StationReference>
{
    public void Configure(EntityTypeBuilder<StationReference> builder)
    {
        builder.HasKey(sr => new {sr.ReferenceId, sr.StationId});

        builder.HasOne(sr => sr.Station)
            .WithMany()
            .HasForeignKey(sr => sr.StationId);

        builder.HasOne(sr => sr.Reference)
            .WithMany()
            .HasForeignKey(sr => sr.ReferenceId);

        builder.HasMany(sr => sr.MFCs)
            .WithOne()
            .HasForeignKey(mfc => new { mfc.ReferenceId, mfc.StationId });
    }
}
