namespace WembleyScada.Infrastructure.EntityConfigurations;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductId);
        builder.Property(p => p.ProductId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasMany(p => p.UsableLines)
            .WithMany();

        builder.HasMany(p => p.References)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId);

        builder.HasMany(p => p.Stations)
            .WithMany();
    }
}
