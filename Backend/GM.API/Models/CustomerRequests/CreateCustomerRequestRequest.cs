namespace GM.API.Models.CustomerRequests;

public class CreateCustomerRequestRequest
{
    public int ServiceId { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime? PreferredDate { get; set; }
}
