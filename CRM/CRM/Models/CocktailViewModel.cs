using CRM.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CRM.ViewModels
{
    public class CocktailViewModel
    {
        public int DrinkId { get; set; }

        [Required]
        public string DrinkName { get; set; }

        [Required]
        public string DrinkRecipe { get; set; }

        [AllowNull]
        public string? LiqIns { get; set; }

        [AllowNull]
        public string? MixIns { get; set; }

        [Required]
        public int BartenderId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;

        // Use DTOs instead of direct models
        public List<BartenderDto> Bartenders { get; set; } = new List<BartenderDto>();
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

    }
}
