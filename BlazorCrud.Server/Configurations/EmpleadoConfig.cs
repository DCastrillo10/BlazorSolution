using BlazorCrud.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorCrud.Server.Configurations
{
    public class EmpleadoConfig : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NombreCompleto).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Sueldo).IsRequired();
            builder.Property(x => x.FechaContrato).IsRequired();

            //Foreigns Keys
            builder.HasOne(d=>d.Departamento).WithMany().HasForeignKey(i=>i.IdDepartamento).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
