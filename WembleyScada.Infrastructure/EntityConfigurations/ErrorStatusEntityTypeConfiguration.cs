namespace WembleyScada.Infrastructure.EntityConfigurations;

public class ErrorStatusEntityTypeConfiguration : IEntityTypeConfiguration<ErrorStatus>
{
    public void Configure(EntityTypeBuilder<ErrorStatus> builder)
    {
        builder.HasKey(es => es.ErrorStatusId);
        builder.Property(es => es.ErrorStatusId)
            .IsRequired()
            .ValueGeneratedOnAdd();
    }
}
