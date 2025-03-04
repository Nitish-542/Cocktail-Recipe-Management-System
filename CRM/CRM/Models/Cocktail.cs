using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CRM.Models
{

    public class Cocktail
    {
        [Key]
        public int DrinkId { get; set; }

        [ForeignKey("Bartender")]
        public int BartenderId { get; set; }

        public string? DrinkName { get; set; }  // Make nullable if DrinkName can be empty
        public string? DrinkRecipe { get; set; }  // Make nullable if DrinkRecipe can be empty
        public string? LiqIns { get; set; }  // Same here for LiqIns
        public string? MixIns { get; set; }  // Same here for MixIns

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public DateTime DatePosted { get; set; }

        // Navigation Property
        public virtual Category Category { get; set; }
        public virtual Bartender Bartender { get; set; }
    }

    public class CocktailDTO
    {
        public int DrinkId { get; set; }
        public int BartenderId { get; set; }
        public BartenderDto Bartender { get; set; }  // Add this property
        public string DrinkName { get; set; }
        public string DrinkRecipe { get; set; }
        public string LiqIns { get; set; }
        public string MixIns { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public CategoryDTO Category { get; set; } // Add this property
        public DateTime DatePosted { get; set; }
    }


}
