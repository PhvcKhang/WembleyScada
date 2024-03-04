namespace WembleyScada.Infrastructure.EntityConfigurations
{
    public class LineEntityTypeConfiguration : IEntityTypeConfiguration<Line>
    {
        public void Configure(EntityTypeBuilder<Line> builder)
        {
            builder.HasKey(l => l.LineId);

            builder.HasMany(l => l.Stations)
                .WithOne(s => s.Line)
                .HasForeignKey(s => s.LineId);

        }
    }
}
