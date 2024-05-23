using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AboutU> AboutUs { get; set; }

    public virtual DbSet<Category> Categorys { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Home> Homes { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeUser> RecipeUsers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Visa> Visas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("USER ID=C##MVCFinalRecipe;PASSWORD=Test321;DATA SOURCE=localhost:1521/xe");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##MVCFINALRECIPE")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AboutU>(entity =>
        {
            entity.HasKey(e => e.AboutusId).HasName("SYS_C008495");

            entity.ToTable("ABOUT_US");

            entity.Property(e => e.AboutusId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ABOUTUS_ID");
            entity.Property(e => e.Description1)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION1");
            entity.Property(e => e.Imageee1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGEEE1");
            entity.Property(e => e.Imageee2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGEEE2");
            entity.Property(e => e.Titleeee1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLEEEE1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("SYS_C008496");

            entity.ToTable("CATEGORYS");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_NAME");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("SYS_C008497");

            entity.ToTable("CONTACT");

            entity.Property(e => e.ContactId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONTACT_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Message)
                .HasColumnType("CLOB")
                .HasColumnName("MESSAGE");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USER_EMAIL");

            entity.HasOne(d => d.UserEmailNavigation).WithMany(p => p.Contacts)
                .HasPrincipalKey(p => p.Email)
                .HasForeignKey(d => d.UserEmail)
                .HasConstraintName("FK_CONTACT_USER");
        });

        modelBuilder.Entity<Home>(entity =>
        {
            entity.HasKey(e => e.HomeId).HasName("SYS_C008498");

            entity.ToTable("HOME");

            entity.Property(e => e.HomeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOME_ID");
            entity.Property(e => e.Description1)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION1");
            entity.Property(e => e.Description2)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION2");
            entity.Property(e => e.Image1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE1");
            entity.Property(e => e.Image2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE2");
            entity.Property(e => e.Image3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE3");
            entity.Property(e => e.Title1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE1");
            entity.Property(e => e.Title2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE2");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("SYS_C008478");

            entity.ToTable("LOGIN");

            entity.HasIndex(e => e.UserId, "SYS_C008479").IsUnique();

            entity.Property(e => e.LoginId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("LOGIN_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.Logins)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_LOGIN_ROLE");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("SYS_C008499");

            entity.ToTable("RECIPE");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_ID");
            entity.Property(e => e.CategoryId)
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CreateDate)
                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.ImagePath1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH1");
            entity.Property(e => e.Ingredients)
                .HasColumnType("CLOB")
                .HasColumnName("INGREDIENTS");
            entity.Property(e => e.Instructions)
                .HasColumnType("CLOB")
                .HasColumnName("INSTRUCTIONS");
            entity.Property(e => e.Isaccepted)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ISACCEPTED");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PRICE");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RECIPE_NAME");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Category).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_RECIPE_CATEGORYS");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_ID");
        });

        modelBuilder.Entity<RecipeUser>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("SYS_C008500");

            entity.ToTable("RECIPE_USER");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("DATE")
                .HasColumnName("PURCHASE_DATE");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.RecipeUsers)
                .HasPrincipalKey(p => p.Email)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK_RECIPE_USER_USERS");

            entity.HasOne(d => d.Recipe).WithOne(p => p.RecipeUser)
                .HasForeignKey<RecipeUser>(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RECIPE_USER_RECIPE");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("SYS_C008480");

            entity.ToTable("ROLE");

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.TestimonialId).HasName("SYS_C008501");

            entity.ToTable("TESTIMONIAL");

            entity.Property(e => e.TestimonialId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("Testimonial_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Rating)
                .HasColumnType("NUMBER")
                .HasColumnName("RATING");
            entity.Property(e => e.Testi)
                .HasMaxLength(355)
                .IsUnicode(false)
                .HasColumnName("TESTI");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.TestimonialEmailNavigations)
                .HasPrincipalKey(p => p.Email)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK_TESTIMONIAL_USERS");

            entity.HasOne(d => d.User).WithMany(p => p.TestimonialUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TESTIMONIAL_USER");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("SYS_C008481");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Fname, "SYS_C008482").IsUnique();

            entity.HasIndex(e => e.Lname, "SYS_C008483").IsUnique();

            entity.HasIndex(e => e.Email, "SYS_C008484").IsUnique();

            entity.HasIndex(e => new { e.Email, e.Password }, "UK_USERS_EMAIL_PASSWORD").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_USERS_ROLE");
        });

        modelBuilder.Entity<Visa>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("VISA");

            entity.Property(e => e.Balance)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BALANCE");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CARD_NUMBER");
            entity.Property(e => e.Cvc)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CVC");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRY_DATE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
