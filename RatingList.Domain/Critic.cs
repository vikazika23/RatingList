using Media.Domain.Contracts;

namespace Media.Domain
{
    public class Critic : IRatingContainer
    {
        //идентификатор
        public int Id { get; set; }
        
        //Название книги
        public string Title { get; set; }
        
        //Автор
        public string Author { get; set; }

        //Длительность
        public int Duration { get; set; }
        public Rating Rating { get; set; }
        public int? RatingId => Rating.Id;

    }
}