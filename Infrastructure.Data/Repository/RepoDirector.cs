using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.Data;
using Infrastructure.Data.GenericRepository;


namespace Infrastructure.Data.Repository
{
    public class RepoDirector : GenericRepository<director>
    {
        public RepoDirector(DataEntities context) : base(context) { }

        public ICollection<Genere> getAllGenreOfDirector(string dir_name)
        {
            var dir = GetByID(dir_name);
            return dir.Generes;
        }
    }
}
