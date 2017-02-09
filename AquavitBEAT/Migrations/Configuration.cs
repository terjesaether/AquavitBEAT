namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AquavitBEAT.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<AquavitBeatContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AquavitBeatContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            var robert = new Artist
            {
                ArtistName = "Robert Solheim",
                FirstName = "Robert",
                LastName = "Solheim",
                Mail = "robert@mail.com",

            };
            var socialMedia = new List<ArtistSocialMedia>
                  {
                      new ArtistSocialMedia
                      {
                          Name = "Facebook",
                          Url = "http://www.facebook/robert",
                          Artist = robert
                      },
                      new ArtistSocialMedia
                      {
                          Name = "Soundcloud",
                          Url = "http://www.facebook/robert",
                          Artist = robert
                      },
                      new ArtistSocialMedia
                      {
                          Name = "Twitter",
                          Url = "http://www.facebook/robert",
                          Artist = robert
                      },
                      new ArtistSocialMedia
                      {
                          Name = "Instagram",
                          Url = "http://www.facebook/robert",
                          Artist = robert
                      }
                  };
            robert.SocialMedia = socialMedia;

            var terje = new Artist
            {
                ArtistName = "Terje Saether",
                FirstName = "Terje",
                LastName = "Saether",
                Mail = "terje@mail.com"
            };

            var socialMedia2 = new List<ArtistSocialMedia>
                  {
                      new ArtistSocialMedia
                      {
                          Name = "Facebook",
                          Url = "http://www.facebook/terje",
                          Artist = terje
                      },
                      new ArtistSocialMedia
                      {
                          Name = "Soundcloud",
                          Url = "http://www.facebook/robert",
                          Artist = terje
                      },
                      new ArtistSocialMedia
                      {
                          Name = "Twitter",
                          Url = "http://www.facebook/robert",
                          Artist = terje
                      },
                      new ArtistSocialMedia
                      {
                          Name = "Instagram",
                          Url = "http://www.facebook/robert",
                          Artist = terje
                      }
                  };
            terje.SocialMedia = socialMedia2;

            context.Artists.AddOrUpdate(robert);
            context.Artists.AddOrUpdate(terje);

            context.SocialMedias.AddOrUpdate(
                s => s.Name,
                new SocialMedia { Name = "Facebook", Prefix = "http://www.facebook.com/" },
                new SocialMedia { Name = "Twitter", Prefix = "http://www.twitter.com/" },
                new SocialMedia { Name = "Soundcloud", Prefix = "http://www.soundcloud.com/" },
                new SocialMedia { Name = "Instagram", Prefix = "http://www.instagram.com/" }
                );

            context.FormatsTypes.AddOrUpdate(
                f => f.FormatTypeName,
                new FormatType { FormatTypeName = "Vinyl" },
                new FormatType { FormatTypeName = "Download" },
                new FormatType { FormatTypeName = "Streaming" },
                new FormatType { FormatTypeName = "CD" },
                new FormatType { FormatTypeName = "Cassette" }
                );

            context.BuyOrStreamSites.AddOrUpdate(
                b => b.Name,
                new BuyOrStreamSite { Name = "Juno", Format = "Vinyl" },
                new BuyOrStreamSite { Name = "Beatport", Format = "Download" },
                new BuyOrStreamSite { Name = "Spotify", Format = "Streaming" },
                new BuyOrStreamSite { Name = "Decks.de", Format = "Vinyl" },
                new BuyOrStreamSite { Name = "iTunes", Format = "Download" }
                );

            context.ReleaseTypes.AddOrUpdate(
                r => r.ReleaseTypeName,
                new ReleaseType { ReleaseTypeName = "Single" },
                new ReleaseType { ReleaseTypeName = "EP" },
                new ReleaseType { ReleaseTypeName = "Album" },
                new ReleaseType { ReleaseTypeName = "Compilation" }
                );
        }
    }
}
