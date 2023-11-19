namespace Library.Api.Models
{
    public record BookInputUpdateModel : BookInputModel
    {
        public BookInputUpdateModel(string Title, string Author, int PublicationYear, bool IsAvailable)
            : base(Title, Author, PublicationYear)
        {
        }

        public bool IsAvailable { get; internal set; }
    }
}
