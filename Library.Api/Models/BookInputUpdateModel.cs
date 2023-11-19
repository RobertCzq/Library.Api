namespace Library.Api.Models
{
    public class BookInputUpdateModel : BookInputModel
    {
        public bool IsAvailable { get; internal set; }
    }
}
