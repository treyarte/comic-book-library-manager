﻿using ComicBookShared.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Data
{
    public class ComicBookArtistRepository : BaseRepository<ComicBookArtist>
    {

        public ComicBookArtistRepository(Context context) : base(context)
        {
        }

 

        public override ComicBookArtist Get(int id, bool includeRelatedEntities = true)
        {
            var comicBookArtist = Context.ComicBookArtists.AsQueryable();

            if (includeRelatedEntities)
            {
                comicBookArtist = comicBookArtist
                       .Include(cba => cba.Artist)
                       .Include(cba => cba.Role)
                       .Include(cba => cba.ComicBook.Series);
                      
            }

            return      comicBookArtist
                        .Where(cba => cba.Id == id)
                       .SingleOrDefault();
        }

        public override IList<ComicBookArtist> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
