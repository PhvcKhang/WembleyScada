namespace WembleyScada.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Line> Lines { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<ShiftReport> ShiftReports { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<MachineStatus> MachineStatus { get; set; }
        public DbSet<StationReference> StationReferences { get; set; }
        public DbSet<ErrorInformation> ErrorInformations { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LineEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftReportEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WorkRecordEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LotEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StationReferenceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MachineStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorInformationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorStatusEntityTypeConfiguration());
        }
    }
}
