using System.Collections.Generic;
using Domain.Entities;
namespace Services.Interfaces
{
    public interface IDirectorServices
    {
        ICollection<DirectorGenre> GetDirectorByName(string director_name);
        IEnumerable<DirectorEntity> GetAllDirectors();
        string createDirector(DirectorEntity directorEntity);
        bool UpdateDirector(string director_name, DirectorEntity directorEntity);
        bool DeleteDirector(string director_name);
    }
}
