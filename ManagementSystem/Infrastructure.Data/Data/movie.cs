//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Data.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class movie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public movie()
        {
            this.movieDirectors = new HashSet<movieDirector>();
            this.movieGenres = new HashSet<movieGenre>();
        }
    
        public string movie_name { get; set; }
        public Nullable<System.DateTime> online_time { get; set; }
        public Nullable<int> star { get; set; }
        public string cast { get; set; }
        public Nullable<int> price { get; set; }
        public string runtime { get; set; }
        public string description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<movieDirector> movieDirectors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<movieGenre> movieGenres { get; set; }
    }
}
