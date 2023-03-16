using System.ComponentModel.DataAnnotations;

namespace PIMPER.Models
{
    public class QuantitiesTable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
