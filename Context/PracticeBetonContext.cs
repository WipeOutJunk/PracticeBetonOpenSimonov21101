using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PracticeBetonNetV
{
    public partial class PracticeBetonContext : DbContext
    {
        public PracticeBetonContext()
        {
        }

        public PracticeBetonContext(DbContextOptions<PracticeBetonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Materialinventory> Materialinventories { get; set; } = null!;
        public virtual DbSet<Materialtransaction> Materialtransactions { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Productionsection> Productionsections { get; set; } = null!;
        public virtual DbSet<Qualitycontrol> Qualitycontrols { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<Supplierpayment> Supplierpayments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PracticeBeton;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");

                entity.HasIndex(e => e.Login, "clients_login_key")
                    .IsUnique();

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");

                entity.Property(e => e.Login)
                    .HasMaxLength(255)
                    .HasColumnName("login");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(255)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("deliveries");

                entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");

                entity.Property(e => e.DeliveryTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("delivery_time");

                entity.Property(e => e.MaterialId).HasColumnName("material_id");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deliveries_material_id_fkey");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deliveries_supplier_id_fkey");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.Login, "employees_login_key")
                    .IsUnique();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");

                entity.Property(e => e.Login)
                    .HasMaxLength(255)
                    .HasColumnName("login");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(255)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employees_role_id_fkey");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("materials");

                entity.Property(e => e.MaterialId).HasColumnName("material_id");

                entity.Property(e => e.MeasureUnit)
                    .HasMaxLength(50)
                    .HasColumnName("measure_unit");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PricePerUnit).HasColumnName("price_per_unit");
            });

            modelBuilder.Entity<Materialinventory>(entity =>
            {
                entity.HasKey(e => e.InventoryId)
                    .HasName("materialinventory_pkey");

                entity.ToTable("materialinventory");

                entity.Property(e => e.InventoryId).HasColumnName("inventory_id");

                entity.Property(e => e.ArrivalDate).HasColumnName("arrival_date");

                entity.Property(e => e.BatchNumber)
                    .HasMaxLength(255)
                    .HasColumnName("batch_number");

                entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");

                entity.Property(e => e.MaterialId).HasColumnName("material_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Materialinventories)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("materialinventory_material_id_fkey");
            });

            modelBuilder.Entity<Materialtransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("materialtransactions_pkey");

                entity.ToTable("materialtransactions");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.InventoryId).HasColumnName("inventory_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.RelatedOrderId).HasColumnName("related_order_id");

                entity.Property(e => e.TransactionDate).HasColumnName("transaction_date");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(50)
                    .HasColumnName("transaction_type");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.Materialtransactions)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("materialtransactions_inventory_id_fkey");

                entity.HasOne(d => d.RelatedOrder)
                    .WithMany(p => p.Materialtransactions)
                    .HasForeignKey(d => d.RelatedOrderId)
                    .HasConstraintName("materialtransactions_related_order_id_fkey");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");

                entity.Property(e => e.OrderDate).HasColumnName("order_date");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orders_client_id_fkey");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orders_product_id_fkey");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PaymentDate).HasColumnName("payment_date");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(255)
                    .HasColumnName("payment_method");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("payments_client_id_fkey");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("payments_order_id_fkey");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductionTime).HasColumnName("production_time");

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("products_section_id_fkey");
            });

            modelBuilder.Entity<Productionsection>(entity =>
            {
                entity.HasKey(e => e.SectionId)
                    .HasName("productionsections_pkey");

                entity.ToTable("productionsections");

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.Property(e => e.Function).HasColumnName("function");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Qualitycontrol>(entity =>
            {
                entity.HasKey(e => e.QcId)
                    .HasName("qualitycontrol_pkey");

                entity.ToTable("qualitycontrol");

                entity.Property(e => e.QcId).HasColumnName("qc_id");

                entity.Property(e => e.CheckDate).HasColumnName("check_date");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Result)
                    .HasMaxLength(50)
                    .HasColumnName("result");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Qualitycontrols)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("qualitycontrol_product_id_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Supplierpayment>(entity =>
            {
                entity.ToTable("supplierpayments");

                entity.Property(e => e.SupplierPaymentId).HasColumnName("supplier_payment_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");

                entity.Property(e => e.PaymentDate).HasColumnName("payment_date");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(255)
                    .HasColumnName("payment_method");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.Supplierpayments)
                    .HasForeignKey(d => d.DeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplierpayments_delivery_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
