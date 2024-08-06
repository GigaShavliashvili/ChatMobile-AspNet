using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatmobile.entites
{
    [Table("testTable", Schema = "test")]
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public required string Name { get; set; }
    }
}