
namespace WembleyScada.Infrastructure.EntityConfigurations
{
    public class WorkRecordEntityTypeConfiguration : IEntityTypeConfiguration<WorkRecord>
    {
        public void Configure(EntityTypeBuilder<WorkRecord> builder)
        {
            builder.HasKey(wr => wr.WorkRecordId);
            builder.Property(wr => wr.WorkRecordId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasOne(wr => wr.Station)
                .WithMany(s => s.EmployeeWorkRecords)
                .HasForeignKey(wr => wr.StationId);
        }
    }
}
