namespace NetCoreAi.Project2_ApiConsumerUI.Dtos
{
    public class ResultCustomerDto
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurName { get; set; }
        [System.Text.Json.Serialization.JsonNumberHandling(System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString)]
        public decimal CustomerBalance { get; set; }
    }
}