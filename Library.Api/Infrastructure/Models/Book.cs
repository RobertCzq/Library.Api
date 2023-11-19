using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Api.Infrastructure.Models
{
    [Table("Book")]
    public class Book
    {
        [Column("Title")]
        public string Title { get; set; }

        [Column("Author")]
        public string Author { get; set; }

        [Column("PublicationYear")]
        public int PublicationYear { get; set; }

        [Column("IsAvailable")]
        public bool IsAvailable { get; set; }
    }
}
