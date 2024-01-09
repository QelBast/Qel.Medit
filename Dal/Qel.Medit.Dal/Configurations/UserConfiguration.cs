using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using User = Qel.Medit.Dal.Entities.User;

namespace Qel.Medit.Dal.Configurations;

[DbContext(typeof(DbContextMain))]
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();

        builder.Property(e => e.UserName)
               .HasMaxLength(128)
               .IsRequired();
        builder.Property(e => e.PasswordHash)
               .HasMaxLength(128)
               .IsRequired();
        //builder.Property(e => e.EdgesColor)
        //       .HasMaxLength(128)
        //       .IsRequired();
        //builder.Property(e => e.NodesColor)
        //       .HasMaxLength(128)
        //       .IsRequired();
        //builder.Property(e => e.IsDirected)
        //        .HasDefaultValue(false)
        //        .IsRequired();

        //builder.Property(e => e.CreationDateTime)
        //       .IsRequired(false);
        //builder.Property(e => e.ModifyDateTime)
        //       .IsRequired(false);
        //builder.Property(e => e.IsDeleted)
        //       .HasDefaultValue(false)
        //       .IsRequired();
    }
}
