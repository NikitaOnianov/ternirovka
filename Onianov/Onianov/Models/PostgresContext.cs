using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Onianov.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Delivere> Deliveres { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<OrderTovar> OrderTovars { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }

    public virtual DbSet<Tovar> Tovars { get; set; }

    public virtual DbSet<TovarType> TovarTypes { get; set; }

    public virtual DbSet<TypeUser> TypeUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Username=userdb;Password=1234;Port=5432;Database=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("address_pk");

            entity.ToTable("address", "trenirovca");

            entity.Property(e => e.AddressId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("address_id");
            entity.Property(e => e.AddressName)
                .HasColumnType("character varying")
                .HasColumnName("address_name");
        });

        modelBuilder.Entity<Delivere>(entity =>
        {
            entity.HasKey(e => e.DelivereId).HasName("deliveres_pk");

            entity.ToTable("deliveres", "trenirovca");

            entity.Property(e => e.DelivereId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("delivere_id");
            entity.Property(e => e.DelivereName)
                .HasColumnType("character varying")
                .HasColumnName("delivere_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pk");

            entity.ToTable("orders", "trenirovca");

            entity.Property(e => e.OrderId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("order_id");
            entity.Property(e => e.OrderAddress).HasColumnName("order_address");
            entity.Property(e => e.OrderCode).HasColumnName("order_code");
            entity.Property(e => e.OrderDateCreate).HasColumnName("order_date_create");
            entity.Property(e => e.OrderDateDelivery).HasColumnName("order_date_delivery");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.OrderUser).HasColumnName("order_user");

            entity.HasOne(d => d.OrderAddressNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_address_fk");

            entity.HasOne(d => d.OrderStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_order_status_fk");

            entity.HasOne(d => d.OrderUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_users_fk");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("order_status_pk");

            entity.ToTable("order_status", "trenirovca");

            entity.Property(e => e.OrderStatusId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("order_status_id");
            entity.Property(e => e.OrderStatusName)
                .HasColumnType("character varying")
                .HasColumnName("order_status_name");
        });

        modelBuilder.Entity<OrderTovar>(entity =>
        {
            entity.HasKey(e => e.OrderTovarId).HasName("order_tovar_pk");

            entity.ToTable("order_tovar", "trenirovca");

            entity.Property(e => e.OrderTovarId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("order_tovar_id");
            entity.Property(e => e.OrderTovarOrder).HasColumnName("order_tovar_order");
            entity.Property(e => e.OrderTovarTovar)
                .HasColumnType("character varying")
                .HasColumnName("order_tovar_tovar");

            entity.HasOne(d => d.OrderTovarOrderNavigation).WithMany(p => p.OrderTovars)
                .HasForeignKey(d => d.OrderTovarOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_tovar_orders_fk");

            entity.HasOne(d => d.OrderTovarTovarNavigation).WithMany(p => p.OrderTovars)
                .HasForeignKey(d => d.OrderTovarTovar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_tovar_tovars_fk");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.ProducerId).HasName("producers_pk");

            entity.ToTable("producers", "trenirovca");

            entity.Property(e => e.ProducerId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("producer_id");
            entity.Property(e => e.ProducerName)
                .HasColumnType("character varying")
                .HasColumnName("producer_name");
        });

        modelBuilder.Entity<Tovar>(entity =>
        {
            entity.HasKey(e => e.TovarId).HasName("tovars_pk");

            entity.ToTable("tovars", "trenirovca");

            entity.Property(e => e.TovarId)
                .HasColumnType("character varying")
                .HasColumnName("tovar_id");
            entity.Property(e => e.TovarCount).HasColumnName("tovar_count");
            entity.Property(e => e.TovarDelivary).HasColumnName("tovar_delivary");
            entity.Property(e => e.TovarDesription).HasColumnName("tovar_desription");
            entity.Property(e => e.TovarName)
                .HasColumnType("character varying")
                .HasColumnName("tovar_name");
            entity.Property(e => e.TovarPhoto)
                .HasColumnType("character varying")
                .HasColumnName("tovar_photo");
            entity.Property(e => e.TovarPrice).HasColumnName("tovar_price");
            entity.Property(e => e.TovarProducer).HasColumnName("tovar_producer");
            entity.Property(e => e.TovarSkidka).HasColumnName("tovar_skidka");
            entity.Property(e => e.TovarType).HasColumnName("tovar_type");

            entity.HasOne(d => d.TovarDelivaryNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.TovarDelivary)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tovars_deliveres_fk");

            entity.HasOne(d => d.TovarProducerNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.TovarProducer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tovars_producers_fk");

            entity.HasOne(d => d.TovarTypeNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.TovarType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tovars_tovar_type_fk");
        });

        modelBuilder.Entity<TovarType>(entity =>
        {
            entity.HasKey(e => e.TovarId).HasName("tovar_type_pk");

            entity.ToTable("tovar_type", "trenirovca");

            entity.Property(e => e.TovarId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("tovar_id");
            entity.Property(e => e.TovarName)
                .HasColumnType("character varying")
                .HasColumnName("tovar_name");
        });

        modelBuilder.Entity<TypeUser>(entity =>
        {
            entity.HasKey(e => e.TypeUserId).HasName("type_user_pk");

            entity.ToTable("type_user", "trenirovca");

            entity.Property(e => e.TypeUserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("type_user_id");
            entity.Property(e => e.TypeUserName)
                .HasColumnType("character varying")
                .HasColumnName("type_user_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pk");

            entity.ToTable("users", "trenirovca");

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
            entity.Property(e => e.UserLogin)
                .HasColumnType("character varying")
                .HasColumnName("user_login");
            entity.Property(e => e.UserName)
                .HasColumnType("character varying")
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasColumnType("character varying")
                .HasColumnName("user_password");
            entity.Property(e => e.UserType).HasColumnName("user_type");

            entity.HasOne(d => d.UserTypeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_type_user_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
