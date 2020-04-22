using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Media.DataAccess.Entities
{
    public class Rating
    {
        public Rating()
        {
            this.Viewer = new HashSet<Viewer>();
            this.Critic = new HashSet<Critic>();
        }
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual ICollection<Viewer> Viewer { get; set; }
        public virtual ICollection<Critic> Critic { get; set; }

        //Идентификатор
        public int Id { get; set; }
        
        //Название библиотеки
        public string Name { get; set; }

        //Адрес
        public string Address { get; set; }
    }
}