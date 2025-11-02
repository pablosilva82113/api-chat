using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebChat.Models;

public partial class ChatDbContext : DbContext
{
    public ChatDbContext()
    {
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlertNotification> AlertNotifications { get; set; }

    public virtual DbSet<AreaBusiness> AreaBusinesses { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<BusinessBot> BusinessBots { get; set; }

    public virtual DbSet<BusinessCategory> BusinessCategories { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<MessageContactEmployee> MessageContactEmployees { get; set; }

    public virtual DbSet<MessagePlatform> MessagePlatforms { get; set; }

    public virtual DbSet<QuickResponse> QuickResponses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1A1FCC4;Database=ChatMultiplatform;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlertNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AlertNot__3214EC0752A6F345");

            entity.ToTable("AlertNotification");

            entity.Property(e => e.DateInsert)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Business).WithMany(p => p.AlertNotifications)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AlertNoti__Busin__3A81B327");
        });

        modelBuilder.Entity<AreaBusiness>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AreaBusi__3214EC07947BBAF4");

            entity.ToTable("AreaBusiness");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Business).WithMany(p => p.AreaBusinesses)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AreaBusin__Busin__4D94879B");
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Business__3214EC07EA1FE9DC");

            entity.ToTable("Business");

            entity.Property(e => e.BusinessTimeZone).HasMaxLength(100);
            entity.Property(e => e.Key).HasMaxLength(255);
            entity.Property(e => e.Mail).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<BusinessBot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Business__3214EC073CF63D3C");

            entity.ToTable("BusinessBot");

            entity.Property(e => e.TypeChatBot).HasMaxLength(100);

            entity.HasOne(d => d.Business).WithMany(p => p.BusinessBots)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BusinessB__Busin__5070F446");
        });

        modelBuilder.Entity<BusinessCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Business__19093A0B119F944B");

            entity.ToTable("BusinessCategory");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Business).WithMany(p => p.BusinessCategories)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK_BusinessCategory_Business");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contact__3214EC075F59D9A1");

            entity.ToTable("Contact");

            entity.Property(e => e.Mail).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.Business).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Contact_Business");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07E323F949");

            entity.ToTable("Employee");

            entity.Property(e => e.AreaId).HasMaxLength(100);
            entity.Property(e => e.Mail).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PasswordHashed).HasMaxLength(500);
            entity.Property(e => e.RoleId).HasMaxLength(100);

            entity.HasOne(d => d.Business).WithMany(p => p.Employees)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Busine__3D5E1FD2");

        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Employee");

            entity.ToTable("Employee");

            entity.Property(e => e.Mail).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PasswordHashed).HasMaxLength(500);

            // FK con Role
            entity.HasOne(d => d.Role)
                  .WithMany(p => p.Employees)
                  .HasForeignKey(d => d.RoleId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Employee_Role");

            // FK con AreaBusiness
            entity.HasOne(d => d.Area)
                  .WithMany(p => p.Employees)
                  .HasForeignKey(d => d.AreaId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Employee_Area");

            // FK con Business
            entity.HasOne(d => d.Business)
                  .WithMany(p => p.Employees)
                  .HasForeignKey(d => d.BusinessId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Employee_Business");
        });


        modelBuilder.Entity<MessagePlatform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MessageP__3214EC074C8699B9");

            entity.ToTable("MessagePlatform");

            entity.Property(e => e.NamePlatform).HasMaxLength(255);
        });

        modelBuilder.Entity<QuickResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QuickRes__3214EC0745A71F8A");

            entity.Property(e => e.Text).HasMaxLength(1000);

            entity.HasOne(d => d.Business).WithMany(p => p.QuickResponses)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK_QuickResponses_Business");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC075F736969");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
