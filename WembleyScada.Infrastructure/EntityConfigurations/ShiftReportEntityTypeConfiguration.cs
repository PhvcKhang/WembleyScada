namespace WembleyScada.Infrastructure.EntityConfigurations;

public class ShiftReportEntityTypeConfiguration : IEntityTypeConfiguration<ShiftReport>
{
    public void Configure(EntityTypeBuilder<ShiftReport> builder)
    {
        builder.HasKey(shrpt => shrpt.ShiftReportId);

        builder.Property(shrpt => shrpt.ShiftReportId)
            .HasDefaultValueSql("NEWID()")
            .ValueGeneratedOnAdd();

        builder.HasOne(shrpt => shrpt.Station)
            .WithMany()
            .HasForeignKey(shrpt => shrpt.StationId);
        
        builder.OwnsMany(shrpt => shrpt.Shots, shots =>
        {
            shots.WithOwner();
            shots.Property(sh => sh.TimeStamp).IsRequired();
            shots.Property(sh => sh.ExecutionTime).IsRequired();
            shots.Property(sh => sh.CycleTime).IsRequired();
        });

    }
}
