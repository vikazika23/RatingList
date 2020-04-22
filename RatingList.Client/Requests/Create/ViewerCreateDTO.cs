using System.ComponentModel.DataAnnotations;

namespace Media.Client.Requests.Create
{
    public class ViewerCreateDTO
    {
        //Название книги
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        
        //Автор
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }
        
        //Возрастное ограниечение
        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }

        //Информация о библиотеке
        public int? RatingId { get; set; }
    }
}