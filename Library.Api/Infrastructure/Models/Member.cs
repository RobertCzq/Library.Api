using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Api.Infrastructure.Models
{
    [Table("Member")]
    public class Member
    {
        [Column("Name")]
        public string Name { get; set; }

        [Column("JoinedDate")]
        public DateTime JoinedDate { get; set; }

    }
}
