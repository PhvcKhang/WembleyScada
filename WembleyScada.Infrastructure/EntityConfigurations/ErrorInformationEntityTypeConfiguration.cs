namespace WembleyScada.Infrastructure.EntityConfigurations
{
    public class ErrorInformationEntityTypeConfiguration : IEntityTypeConfiguration<ErrorInformation>
    {
        public void Configure(EntityTypeBuilder<ErrorInformation> builder)
        {
            builder.HasKey(ei => ei.ErrorId);

            builder.HasOne(ei => ei.Station)
                .WithMany()
                .HasForeignKey(ei => ei.StationId);

            builder.HasMany(ei => ei.ErrorStatuses)
                .WithOne(es => es.ErrorInformation)
                .HasForeignKey(es => es.ErrorId);

        }
    }
}
