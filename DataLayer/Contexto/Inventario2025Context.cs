using System;
using System.Collections.Generic;
using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Contexto;

public partial class Inventario2025Context : DbContext
{
    public Inventario2025Context()
    {
    }

    public Inventario2025Context(DbContextOptions<Inventario2025Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCategoria> TbCategorias { get; set; }

    public virtual DbSet<TbProducto> TbProductos { get; set; }

 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCategoria>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("PK__tb_categ__140587C755AF015F");

            entity.ToTable("tb_categorias");

            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_categoria");
        });

        modelBuilder.Entity<TbProducto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PK__tb_produ__DC53BE3C30DE98F7");

            entity.ToTable("tb_productos");

            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.TbProductos)
                .HasForeignKey(d => d.Idcategoria)
                .HasConstraintName("FK__tb_produc__idcat__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
