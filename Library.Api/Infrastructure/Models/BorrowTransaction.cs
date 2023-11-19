using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Api.Infrastructure.Models
{
    [Table("BorrowTransaction")]
    public class BorrowTransaction
    {
        [Column("BookID")]
        public int BookId { get; set; }

        [Column("MemberID")]
        public int MemberId { get; set; }

        [Column("BorrowDate")]
        public DateTime BorrowDate { get; set; }

        [Column("ReturnDate")]
        public DateTime ReturnDate { get; set; }
    }
}
