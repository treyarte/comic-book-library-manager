using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace ComicBookShared.Data
{
    public class ArtistRepository : BaseRepository<Artist>
    {
        public ArtistRepository(Context context) : base(context)
        {        
        }
        public override Artist Get(int id, bool includeRelatedEntities = true)
        {

            var artist = Context.Artists.AsQueryable();

            if (includeRelatedEntities)
            {
                artist = artist
                    .Include(s => s.ComicBooks.Select(a => a.ComicBook.Series))
                     .Include(s => s.ComicBooks.Select(a => a.Role));


            }
            return artist.Where(a => a.Id == id).SingleOrDefault();

          
        }

        public override IList<Artist> GetList()
        {
            return Context.Artists.OrderBy(a => a.Name).ToList();
        }

        public bool ArtistNameAvailable(int artistId, string artistName)
        {
            return Context.Artists.Any(a => a.Id != artistId && a.Name == artistName);
        }
    }
}
