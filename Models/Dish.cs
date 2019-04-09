using System;
using System.ComponentModel.DataAnnotations;

namespace crudelicious.Models
{

    public class Dish
    {
        [Key]
        public int DishId {get;set;}

        [Required]
        [Display(Name="Name of Dish")]
        public string Name {get;set;}

        [Required]
        [Display(Name="Chef's Name")]
        public string Chef {get;set;}

        [Required]
        [Range(1,5)]
        [Display(Name="Tastiness")]
        public int Tastiness {get;set;}

        [Required]
        [Range(0,double.PositiveInfinity)]
        [Display(Name="# of Calories")]
        public int Calories {get;set;}

        [Required]
        [Display(Name="Description")]
        public string Description {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}