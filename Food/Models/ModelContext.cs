using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Food.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aboutuspage> Aboutuspages { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contactuspage> Contactuspages { get; set; }

    public virtual DbSet<Homepage> Homepages { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscribe> Subscribes { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<Visa> Visas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("USER ID=C##FIRSTPROJECT;PASSWORD=Test321;DATA SOURCE=localhost:1521/xe");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##FIRSTPROJECT")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Aboutuspage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008614");

            entity.ToTable("ABOUTUSPAGE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Image1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE1");
            entity.Property(e => e.Image2)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE2");
            entity.Property(e => e.Image3)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE3");
            entity.Property(e => e.Image4)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE4");
            entity.Property(e => e.Paragraph1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH1");
            entity.Property(e => e.Paragraph2)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH2");
            entity.Property(e => e.Paragraph3)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH3");
            entity.Property(e => e.Paragraph4)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH4");
            entity.Property(e => e.SectionName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("SECTION_NAME");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008604");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
        });

        modelBuilder.Entity<Contactuspage>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("SYS_C008612");

            entity.ToTable("CONTACTUSPAGE");

            entity.Property(e => e.ContentId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONTENT_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FullName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("FULL_NAME");
            entity.Property(e => e.Message)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
        });

        modelBuilder.Entity<Homepage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008610");

            entity.ToTable("HOMEPAGE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Image1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE1");
            entity.Property(e => e.Image2)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE2");
            entity.Property(e => e.Image3)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE3");
            entity.Property(e => e.Image4)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE4");
            entity.Property(e => e.Link1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("LINK1");
            entity.Property(e => e.Link2)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("LINK2");
            entity.Property(e => e.Link3)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("LINK3");
            entity.Property(e => e.Link4)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("LINK4");
            entity.Property(e => e.Link5)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("LINK5");
            entity.Property(e => e.Paragraph1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH1");
            entity.Property(e => e.Paragraph2)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH2");
            entity.Property(e => e.Paragraph3)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH3");
            entity.Property(e => e.Paragraph4)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH4");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.SectionName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("SECTION_NAME");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008658");

            entity.ToTable("PURCHASES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.BayDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnType("DATE")
                .HasColumnName("BAY_DATE");
            entity.Property(e => e.RecipeId)
                .HasColumnType("NUMBER")
                .HasColumnName("RECIPE_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_RECIPE_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERID");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("SYS_C008649");

            entity.ToTable("RECIPE");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("RECIPE_ID");
            entity.Property(e => e.CategoryId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.ChefId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CHEF_ID");
            entity.Property(e => e.Image1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE1");
            entity.Property(e => e.Image2)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE2");
            entity.Property(e => e.Image3)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE3");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.RecipeDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnType("DATE")
                .HasColumnName("RECIPE_DATE");
            entity.Property(e => e.RecipeIngredients)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RECIPE_INGREDIENTS");
            entity.Property(e => e.RecipePreparation)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RECIPE_PREPARATION");
            entity.Property(e => e.RecipeStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("'pending'")
                .HasColumnName("RECIPE_STATUS");
            entity.Property(e => e.RecipeTime)
                .HasPrecision(6)
                .HasColumnName("RECIPE_TIME");
            entity.Property(e => e.RecipepName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RECIPEP_NAME");

            entity.HasOne(d => d.Category).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CATEGORY_ID");

            entity.HasOne(d => d.Chef).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.ChefId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CHEF_ID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("SYS_C008597");

            entity.ToTable("ROLES");

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Subscribe>(entity =>
        {
            entity.HasKey(e => e.SubscriberId).HasName("SYS_C008620");

            entity.ToTable("SUBSCRIBE");

            entity.HasIndex(e => e.Email, "SYS_C008621").IsUnique();

            entity.Property(e => e.SubscriberId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUBSCRIBER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.SubscriptionDate)
                .HasDefaultValueSql("CURRENT_DATE\n")
                .HasColumnType("DATE")
                .HasColumnName("SUBSCRIPTION_DATE");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.TestimonialId).HasName("SYS_C008631");

            entity.ToTable("TESTIMONIAL");

            entity.Property(e => e.TestimonialId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIAL_ID");
            entity.Property(e => e.TestimonialStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("'pending'")
                .HasColumnName("TESTIMONIAL_STATUS");
            entity.Property(e => e.TestimonialText)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("TESTIMONIAL_TEXT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C008632");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_USERS_ID");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Email, "SYS_C008591").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F_NAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.LName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("L_NAME");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008605");

            entity.ToTable("USER_LOGIN");

            entity.HasIndex(e => e.Username, "SYS_C008601").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROLE_ID");

            entity.HasOne(d => d.User).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USER_ID");
        });

        modelBuilder.Entity<Visa>(entity =>
        {
            entity.HasKey(e => e.VisaId).HasName("SYS_C008627");

            entity.ToTable("VISA");

            entity.HasIndex(e => e.CardNumber, "SYS_C008628").IsUnique();

            entity.Property(e => e.VisaId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("VISA_ID");
            entity.Property(e => e.CardHolderName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CARD_HOLDER_NAME");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CARD_NUMBER");
            entity.Property(e => e.Cvv)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("CVV");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRATION_DATE");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Visas)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C008629");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
