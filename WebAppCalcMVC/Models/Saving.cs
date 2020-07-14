using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCalcMVC.Models
{
    public class Saving
    {
        public int Id { get; set; }
        public string CalculationValue { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
