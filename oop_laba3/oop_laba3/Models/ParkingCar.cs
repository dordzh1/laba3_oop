using System.ComponentModel.DataAnnotations.Schema;

namespace oop_laba3.Models
{
    public class ParkingCar
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car? Car { get; set; }
        public int ParkingId { get; set; }
        [ForeignKey(nameof(ParkingId))]
        public Parking? Parking { get; set; }
    }
}
