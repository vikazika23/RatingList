using Media.Domain.Contracts;

namespace Media.Domain.Models
{
    public class RatingUpdateModel : IRatingIdentity
    {
        public int Id { get; set; }
        
        //Название предмета
        public string Name { get; set; }

        //Адрес
        public string Address { get; set; }
    }
}