using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Prueba.Model
{
    public partial class ISO3166DBContext : DbContext
    {
        public ISO3166DBContext()
        {
        }

        public ISO3166DBContext(DbContextOptions<ISO3166DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Timezone> Timezones { get; set; }
        public virtual DbSet<Translation> Translations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ISO3166DB;Persist Security Info=True;User ID=lflores;Password=S3ph1r0z_2611");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Latitude)
                    .HasMaxLength(31)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(31)
                    .HasColumnName("longitude");

                entity.Property(e => e.Name)
                    .HasMaxLength(29)
                    .HasColumnName("name");

                entity.Property(e => e.StateId).HasColumnName("stateId");

                entity.Property(e => e.WikiDataId)
                    .HasMaxLength(28)
                    .HasColumnName("wikiDataId");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Cities_Sates");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Capital)
                    .HasMaxLength(25)
                    .HasColumnName("capital");

                entity.Property(e => e.Currency)
                    .HasMaxLength(23)
                    .HasColumnName("currency");

                entity.Property(e => e.CurrencyName)
                    .HasMaxLength(34)
                    .HasColumnName("currency_name");

                entity.Property(e => e.CurrencySymbol)
                    .HasMaxLength(21)
                    .HasColumnName("currency_symbol");

                entity.Property(e => e.Emoji)
                    .HasMaxLength(24)
                    .HasColumnName("emoji");

                entity.Property(e => e.EmojiU)
                    .HasMaxLength(35)
                    .HasColumnName("emojiU");

                entity.Property(e => e.Iso2)
                    .HasMaxLength(22)
                    .HasColumnName("iso2");

                entity.Property(e => e.Iso3)
                    .HasMaxLength(23)
                    .HasColumnName("iso3");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(31)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(31)
                    .HasColumnName("longitude");

                entity.Property(e => e.Name)
                    .HasMaxLength(31)
                    .HasColumnName("name");

                entity.Property(e => e.Native)
                    .HasMaxLength(29)
                    .HasColumnName("native");

                entity.Property(e => e.NumericCode)
                    .HasMaxLength(23)
                    .HasColumnName("numeric_code");

                entity.Property(e => e.PhoneCode)
                    .HasMaxLength(22)
                    .HasColumnName("phone_code");

                entity.Property(e => e.Region)
                    .HasMaxLength(24)
                    .HasColumnName("region");

                entity.Property(e => e.Subregion)
                    .HasMaxLength(33)
                    .HasColumnName("subregion");

                entity.Property(e => e.Tld)
                    .HasMaxLength(23)
                    .HasColumnName("tld");
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK__Sates__C3BA3B3A15DFEBEB");

                entity.Property(e => e.CountryId).HasColumnName("countryId");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(31)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(31)
                    .HasColumnName("longitude");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.StateCode)
                    .HasMaxLength(23)
                    .HasColumnName("state_code");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Sates_Countries");
            });

            modelBuilder.Entity<Timezone>(entity =>
            {
                entity.HasKey(e => e.TimezonesId)
                    .HasName("PK__timezone__F948AB7F2E3574C2");

                entity.ToTable("timezones");

                entity.Property(e => e.TimezonesId).HasColumnName("timezonesId");

                entity.Property(e => e.Abbreviation)
                    .HasMaxLength(23)
                    .HasColumnName("abbreviation");

                entity.Property(e => e.GmtOffset).HasColumnName("gmtOffset");

                entity.Property(e => e.GmtOffsetName)
                    .HasMaxLength(29)
                    .HasColumnName("gmtOffsetName");

                entity.Property(e => e.TzName)
                    .HasMaxLength(36)
                    .HasColumnName("tzName");

                entity.Property(e => e.ZoneName)
                    .HasMaxLength(30)
                    .HasColumnName("zoneName");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Timezones)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_timezones_Countries");
            });

            modelBuilder.Entity<Translation>(entity =>
            {
                entity.HasKey(e => e.TranslationsId)
                    .HasName("PK__translat__34615F6D663DCF86");

                entity.ToTable("translations");

                entity.Property(e => e.TranslationsId).HasColumnName("translationsId");

                entity.Property(e => e.Br)
                    .HasMaxLength(31)
                    .HasColumnName("br");

                entity.Property(e => e.Cn)
                    .HasMaxLength(23)
                    .HasColumnName("cn");

                entity.Property(e => e.De)
                    .HasMaxLength(31)
                    .HasColumnName("de");

                entity.Property(e => e.Es)
                    .HasMaxLength(30)
                    .HasColumnName("es");

                entity.Property(e => e.Fa)
                    .HasMaxLength(29)
                    .HasColumnName("fa");

                entity.Property(e => e.Fr)
                    .HasMaxLength(31)
                    .HasColumnName("fr");

                entity.Property(e => e.Hr)
                    .HasMaxLength(30)
                    .HasColumnName("hr");

                entity.Property(e => e.It)
                    .HasMaxLength(31)
                    .HasColumnName("it");

                entity.Property(e => e.Ja)
                    .HasMaxLength(27)
                    .HasColumnName("ja");

                entity.Property(e => e.Kr)
                    .HasMaxLength(26)
                    .HasColumnName("kr");

                entity.Property(e => e.Nl)
                    .HasMaxLength(31)
                    .HasColumnName("nl");

                entity.Property(e => e.Pt)
                    .HasMaxLength(31)
                    .HasColumnName("pt");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Translations)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_translations_Countries");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
