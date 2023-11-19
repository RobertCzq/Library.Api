namespace Library.Api.Models
{
    public class BorrowTransactionInputModel
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
    }
}
