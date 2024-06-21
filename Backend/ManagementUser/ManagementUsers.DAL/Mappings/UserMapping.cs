using ManagementUsers.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ManagementUsers.DAL.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id).ValueGeneratedOnAdd();

            builder.Property(user => user.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(user => user.Age)
                .IsRequired()
                .HasMaxLength(160);

            builder.Property(user => user.CreatedAt)
                .IsRequired();

            builder.Property(user => user.LastUpdatedAt)
                .IsRequired();

            builder.HasMany(user => user.Dependents)
                .WithOne(dependent => dependent.User)
                .HasForeignKey(dependent => dependent.UserId);
        }
    }
}
