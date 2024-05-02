using System.ComponentModel.DataAnnotations.Schema;

namespace oop_laba3.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int HumanId { get; set; }
        [ForeignKey(nameof(HumanId))]
        public Human? Human { get; set; }
    }
}