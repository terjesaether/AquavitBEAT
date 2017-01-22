using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquavitBEAT.Models
{

    interface IArtistRepository
    {
        IEnumerable<Artist> GetAllArtists();
        Artist GetArtistById(int id);
        void Delete(Artist artist);
        void Update(Artist artist);
    }

    interface IDatabaseRepository
    {
        IEnumerable<Artist> GetArtists(DbContext context);
        Artist GetArtistById(int id, DbContext context);
        void Delete(object type, DbContext context);
        void Update(object type, DbContext context);
    }
}
