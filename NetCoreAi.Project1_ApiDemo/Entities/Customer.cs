using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreAi.Project1_ApiDemo.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurName { get; set; }
        public decimal CustomerBalance { get; set; }
    }
}
