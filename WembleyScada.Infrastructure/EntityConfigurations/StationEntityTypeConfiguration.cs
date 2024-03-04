
namespace WembleyScada.Infrastructure.EntityConfigurations
{
    public class StationEntityTypeConfiguration : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(s => s.StationId);

        }
    }
}
