
namespace WembleyScada.Infrastructure.EntityConfigurations;

public class LotEntityTypeConfiguration : IEntityTypeConfiguration<Lot>
{
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        builder.HasKey(l => l.LotId);
        builder.Property(l => l.LotId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasIndex(l => l.LotCode)
            .IsUnique();
    }
}
