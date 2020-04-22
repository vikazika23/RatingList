using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Media.DataAccess.Entities
{
    public class Critic
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //идентификатор
        public int Id { get; set; }
        
        //Название фильма
        public string Title { get; set; }

        //Автор
        public string Author { get; set; }

        //Длительность
        public int Duration { get; set; }
        //public int? RatingId => Rating.Id;
        public virtual Rating Rating { get; set; }
        public int? RatingId { get; set; }
    }
}