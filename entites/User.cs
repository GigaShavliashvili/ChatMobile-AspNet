using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatmobile.entites
{
    [Table("user", Schema = "userS")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        [MinLength(12)]
        public required string UserName { get; set; }

        [MaxLength(256)]
        [MinLength(8)]
        public required string Password { get; set; }
        public int VerificationCode { get; set; }
        public bool IsVerify { get; set; } = false;
        public DateTime CreateDate { get; set; }
        public DateTime? VerificationDate { get; set; }

    }
}