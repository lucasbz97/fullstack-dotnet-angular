using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ManagementUsers.DAL.Entities;

namespace ManagementUsers.DAL.Mappings
{
    public class DependentMapping : IEntityTypeConfiguration<DependentModel>
    {
        public void Configure(EntityTypeBuilder<DependentModel> builder)
        {
            builder.ToTable("Dependents");
            builder.HasKey(dependent => dependent.Id);
            builder.Property(dependent => dependent.Id).ValueGeneratedOnAdd();

            builder.Property(dependent => dependent.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(dependent => dependent.Age)
                .IsRequired();

            // Supondo que você tenha outras propriedades como CreatedAt e LastUpdatedAt
            builder.Property(dependent => dependent.CreatedAt)
                .IsRequired();

            builder.Property(dependent => dependent.LastUpdatedAt)
                .IsRequired();

            // Configuração da chave estrangeira
            builder.HasOne(dependent => dependent.User)
                .WithMany(user => user.Dependents)
                .HasForeignKey(dependent => dependent.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
