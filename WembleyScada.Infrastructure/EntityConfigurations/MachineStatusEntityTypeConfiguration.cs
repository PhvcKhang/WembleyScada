namespace WembleyScada.Infrastructure.EntityConfigurations;

public class MachineStatusEntityTypeConfiguration : IEntityTypeConfiguration<MachineStatus>
{
    public void Configure(EntityTypeBuilder<MachineStatus> builder)
    {
        builder.HasKey(ms => ms.MachineStatusId);
        builder.Property(ms => ms.MachineStatusId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasOne(ms => ms.Station)
            .WithMany()
            .HasForeignKey(ms => ms.StationId);
    }
}
