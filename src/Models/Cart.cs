using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models {
    public class Cart {
        public int CartID {get; set;}
        public int quantity {get; set;}
        public string? ApplicationUserID {get; set;} 
        public int ProductID {get; set;}

        [ForeignKey("ApplicationUserID")]
        public ApplicationUser? applicationUser {get; set;}
        public Product? product {get; set;}
    }
}