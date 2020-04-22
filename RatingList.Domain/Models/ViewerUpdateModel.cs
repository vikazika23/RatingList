using Media.Domain.Contracts;

namespace Media.Domain.Models
{
    public class ViewerUpdateModel : IViewerIdentity, IRatingContainer
    {
        public int Id { get; set; }

        //Название практической работы
        public string Title { get; set; }

        //Имя студента
        public string Author { get; set; }

        //оценка
        public int Rating { get; set; }

        public int? RatingId { get; set; }
    }
}