using System;
using System.Collections.Generic;
using Health_Insurance.Areas.AdminDashboard.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Health_Insurance.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }
    public virtual DbSet<AboutUsPage> AboutUsPages { get; set; }

    public virtual DbSet<ContactUsPage> ContactUsPages { get; set; }

    public virtual DbSet<HomePage> HomePages { get; set; }

    public virtual DbSet<SahtakBank> SahtakBanks { get; set; }

    public virtual DbSet<SahtakBeneficiary> SahtakBeneficiaries { get; set; }

    public virtual DbSet<SahtakContactU> SahtakContactUs { get; set; }

    public virtual DbSet<SahtakPayment> SahtakPayments { get; set; }

    public virtual DbSet<SahtakRole> SahtakRoles { get; set; }

    public virtual DbSet<SahtakSubscription> SahtakSubscriptions { get; set; }

    public virtual DbSet<SahtakTestimonial> SahtakTestimonials { get; set; }

    public virtual DbSet<SahtakUser> SahtakUsers { get; set; }

    public virtual DbSet<TestimonialPage> TestimonialPages { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=C##MVC_SAHTAK;Password=SAH123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##MVC_SAHTAK")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AboutUsPage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008472");

            entity.ToTable("ABOUT_US_PAGE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.FirstParagraph)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("FIRST_PARAGRAPH");
            entity.Property(e => e.FirstTitle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FIRST_TITLE");
            entity.Property(e => e.ImagePath)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.PointFive)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_FIVE");
            entity.Property(e => e.PointFiveE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_FIVE_E");
            entity.Property(e => e.PointFiveEText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_FIVE_E_TEXT");
            entity.Property(e => e.PointFour)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_FOUR");
            entity.Property(e => e.PointFourE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_FOUR_E");
            entity.Property(e => e.PointFourEText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_FOUR_E_TEXT");
            entity.Property(e => e.PointOne)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_ONE");
            entity.Property(e => e.PointOneE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_ONE_E");
            entity.Property(e => e.PointOneEText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_ONE_E_TEXT");
            entity.Property(e => e.PointSix)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_SIX");
            entity.Property(e => e.PointSixE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_SIX_E");
            entity.Property(e => e.PointSixEText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_SIX_E_TEXT");
            entity.Property(e => e.PointThree)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_THREE");
            entity.Property(e => e.PointThreeE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_THREE_E");
            entity.Property(e => e.PointThreeEText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_THREE_E_TEXT");
            entity.Property(e => e.PointTwo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_TWO");
            entity.Property(e => e.PointTwoE)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POINT_TWO_E");
            entity.Property(e => e.PointTwoEText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_TWO_E_TEXT");
            entity.Property(e => e.SecondParagraph)
                .IsUnicode(false)
                .HasColumnName("SECOND_PARAGRAPH");
            entity.Property(e => e.SecondTitle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("SECOND_TITLE");
            entity.Property(e => e.SixPointParagraph)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("SIX_POINT_PARAGRAPH");
            entity.Property(e => e.ThirdParagraph)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("THIRD_PARAGRAPH");
            entity.Property(e => e.ThirdTitle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("THIRD_TITLE");
        });

        modelBuilder.Entity<ContactUsPage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008476");

            entity.ToTable("CONTACT_US_PAGE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Header)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HEADER");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOCATION");
            entity.Property(e => e.LocationDesc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LOCATION_DESC");
            entity.Property(e => e.Paragraph)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TITLE");
            entity.Property(e => e.WorkingDays)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WORKING_DAYS");
            entity.Property(e => e.WorkingDaysDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WORKING_DAYS_DESC");
        });

        modelBuilder.Entity<HomePage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008487");

            entity.ToTable("HOME_PAGE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.BgImageFive)
                .IsUnicode(false)
                .HasColumnName("BG_IMAGE_FIVE");
            entity.Property(e => e.BgImageFour)
                .IsUnicode(false)
                .HasColumnName("BG_IMAGE_FOUR");
            entity.Property(e => e.BgImageOne)
                .IsUnicode(false)
                .HasColumnName("BG_IMAGE_ONE");
            entity.Property(e => e.BgImageThree)
                .IsUnicode(false)
                .HasColumnName("BG_IMAGE_THREE");
            entity.Property(e => e.BgImageTwo)
                .IsUnicode(false)
                .HasColumnName("BG_IMAGE_TWO");
            entity.Property(e => e.Feedback)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("FEEDBACK");
            entity.Property(e => e.IntroText)
                .IsUnicode(false)
                .HasColumnName("INTRO_TEXT");
            entity.Property(e => e.IntroTitle)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("INTRO_TITLE");
            entity.Property(e => e.PointOneText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_ONE_TEXT");
            entity.Property(e => e.PointOneTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_ONE_TITLE");
            entity.Property(e => e.PointThreeText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_THREE_TEXT");
            entity.Property(e => e.PointThreeTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_THREE_TITLE");
            entity.Property(e => e.PointTwoText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_TWO_TEXT");
            entity.Property(e => e.PointTwoTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POINT_TWO_TITLE");
            entity.Property(e => e.TextCardOne)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TEXT_CARD_ONE");
            entity.Property(e => e.TextCardThree)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TEXT_CARD_THREE");
            entity.Property(e => e.TextEmer)
                .IsUnicode(false)
                .HasColumnName("TEXT_EMER");
            entity.Property(e => e.TextSubs)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TEXT_SUBS");
            entity.Property(e => e.TitleCardOne)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE_CARD_ONE");
            entity.Property(e => e.TitleCardThree)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE_CARD_THREE");
            entity.Property(e => e.TitleEmer)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TITLE_EMER");
            entity.Property(e => e.TitleSubs)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TITLE_SUBS");
        });

        modelBuilder.Entity<SahtakBank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008443");

            entity.ToTable("SAHTAK_BANK");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.AccountName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ACCOUNT_NAME");
            entity.Property(e => e.Amount)
                .HasColumnType("FLOAT")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Password)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
        });

        modelBuilder.Entity<SahtakBeneficiary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008450");

            entity.ToTable("SAHTAK_BENEFICIARY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.ProofOfRelationship)
                .IsUnicode(false)
                .HasColumnName("PROOF_OF_RELATIONSHIP");
            entity.Property(e => e.Relationship)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("RELATIONSHIP");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.SubscriptionId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUBSCRIPTION_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SahtakBeneficiaries)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("SYS_C008451");

            entity.HasOne(d => d.User).WithMany(p => p.SahtakBeneficiaries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USER");
        });

        modelBuilder.Entity<SahtakContactU>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008439");

            entity.ToTable("SAHTAK_CONTACT_US");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.Subject)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("SUBJECT");
        });

        modelBuilder.Entity<SahtakPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008445");

            entity.ToTable("SAHTAK_PAYMENT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Amount)
                .HasColumnType("FLOAT")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.BankId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BANK_ID");
            entity.Property(e => e.CardNameholder)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CARD_NAMEHOLDER");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CARD_NUMBER");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("DATE")
                .HasColumnName("PAYMENT_DATE");
            entity.Property(e => e.Status)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.SubscriptionId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUBSCRIPTION_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Bank).WithMany(p => p.SahtakPayments)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("SYS_C008447");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SahtakPayments)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("SYS_C008448");

            entity.HasOne(d => d.User).WithMany(p => p.SahtakPayments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C008446");
        });

        modelBuilder.Entity<SahtakRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008425");

            entity.ToTable("SAHTAK_ROLE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<SahtakSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008433");

            entity.ToTable("SAHTAK_SUBSCRIPTION");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Period)
                .IsUnicode(false)
                .HasColumnName("PERIOD");
            entity.Property(e => e.Price)
                .HasColumnType("FLOAT")
                .HasColumnName("PRICE");
        });

        modelBuilder.Entity<SahtakTestimonial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008436");

            entity.ToTable("SAHTAK_TESTIMONIAL");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.SubmitDate)
                .HasColumnType("DATE")
                .HasColumnName("SUBMIT_DATE");
            entity.Property(e => e.Text)
                .IsUnicode(false)
                .HasColumnName("TEXT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.SahtakTestimonials)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C008437");
        });

        modelBuilder.Entity<SahtakUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008430");

            entity.ToTable("SAHTAK_USER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.JoinDate)
                .HasColumnType("DATE")
                .HasColumnName("JOIN_DATE");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.SahtakUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("SYS_C008431");
        });

        modelBuilder.Entity<TestimonialPage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008474");

            entity.ToTable("TESTIMONIAL_PAGE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Header)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HEADER");
            entity.Property(e => e.Paragraph)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008459");

            entity.ToTable("USER_SUBSCRIPTION");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.DateFrom)
                .HasColumnType("DATE")
                .HasColumnName("DATE_FROM");
            entity.Property(e => e.DateTo)
                .HasColumnType("DATE")
                .HasColumnName("DATE_TO");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("DATE")
                .HasColumnName("PAYMENT_DATE");
            entity.Property(e => e.SubscriptionId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUBSCRIPTION_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Subscription).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("SYS_C008461");

            entity.HasOne(d => d.User).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C008460");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
