using Media.Domain.Contracts;

namespace Media.Domain.Models
{
    public class CriticUpdateModel : ICriticIdentity, IRatingContainer
    {
        public int Id { get; set; }

        //Название лабораторной работы
        public string Title { get; set; }

        //Имя студента
        public string Author { get; set; }

        //оценка
        public int Rating { get; set; }

        public int? RatingId { get; set; }
    }
}