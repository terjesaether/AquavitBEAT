using System.Data.Entity;

namespace AquavitBEAT.Models
{
    public class AquavitBeatContext : DbContext
    {
        public AquavitBeatContext() : base("AquavitBeatConnection")
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<AquavitBeatContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<AquavitBeatContext>());
        }
        public DbSet<Release> Releases { get; set; }

        public DbSet<ReleaseType> ReleaseTypes { get; set; }

        public DbSet<FormatType> FormatsTypes { get; set; }

        public DbSet<ReleaseFormat> ReleaseFormats { get; set; }

        public DbSet<Song> Songs { get; set; }

        public DbSet<Artwork> Artworks { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<SocialMedia> SocialMedias { get; set; }

        public DbSet<ArtistSocialMedia> ArtistSocialMedias { get; set; }

        public DbSet<ReleaseToArtist> ReleaseToArtist { get; set; }

        public DbSet<SongToArtist> SongToArtists { get; set; }

        public DbSet<SongToRemixer> SongToRemixers { get; set; }

        public DbSet<SongToRelease> SongToReleases { get; set; }

        public DbSet<BuyOrStreamSite> BuyOrStreamSites { get; set; }

        public DbSet<BuyOrStreamLink> BuyOrStreamLinks { get; set; }

        public DbSet<UploadedImage> UploadedImages { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Artist>().HasMany(a => a.ArtistSocialMedias);
            modelBuilder.Entity<Release>().HasMany(a => a.BuyOrStreamLinks);

            base.OnModelCreating(modelBuilder);
        }
    }
}