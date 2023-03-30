using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PizzaCabinInc.Model
{
    public class WorkForceScheduleRequest
    {
        [Required]       
        public DateTime date { get; set; }

        [Required]
        [Range(1, 16, ErrorMessage = "The quantity has to be between 1 and 16.")]
        public int quantity { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The quantity has to be greater than 1.")]
        public int leaderID { get; set; }
    }
}
