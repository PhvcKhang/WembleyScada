namespace WembleyScada.Infrastructure.EntityConfigurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);

            builder.HasMany(e => e.WorkRecords)
                .WithOne(wr => wr.Employee)
                .HasForeignKey(wr => wr.EmployeeId);
        }
    }
}
