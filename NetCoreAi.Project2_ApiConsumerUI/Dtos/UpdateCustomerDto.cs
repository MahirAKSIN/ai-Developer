namespace NetCoreAi.Project2_ApiConsumerUI.Dtos
{
    public class UpdateCustomerDto
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurName { get; set; }
        public decimal CustomerBalance { get; set; }
    }
}
