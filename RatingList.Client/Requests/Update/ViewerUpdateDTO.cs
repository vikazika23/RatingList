using Media.Client.Requests.Create;

namespace Media.Client.Requests.Update
{
    public class ViewerUpdateDTO : ViewerCreateDTO
    {
        public int Id { get; set; }
    }
}