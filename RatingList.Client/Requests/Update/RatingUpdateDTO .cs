using Media.Client.Requests.Create;

namespace Media.Client.Requests.Update
{
    public class RatingUpdateDTO : RatingCreateDTO
    {
        public int Id { get; set; }
    }
}