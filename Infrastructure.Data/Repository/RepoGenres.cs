using Infrastructure.Data.Data;
using Infrastructure.Data.GenericRepository;
using System.Collections.Generic;
using System;

namespace Infrastructure.Data.Repository
{
    public class RepoGenres : GenericRepository<Genere>
    {
        //specific method
        public RepoGenres(DataEntities context) : base(context)
        {
        }

        public bool ManyToManyDirectorGenre(director director)
        {
            try
            {
                foreach (var genres in director.Generes)
                {
                    DbSet.Attach(genres);
                }
                return true;
            }catch(Exception e)
            {
                return false;
            }

        }
    }
}
