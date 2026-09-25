using GM.Domain.Common;

namespace GM.Domain.Entities;

public enum RequestStatus
{
    Pending = 0,
    InProgress = 1,
    Completed = 2,
    Cancelled = 3
}

public class CustomerRequest : BaseEntity
{
    public int UserId { get; private set; }

    public int ServiceId { get; private set; }

    public string Description { get; private set; }

    public RequestStatus Status { get; private set; }

    public DateTime? PreferredDate { get; private set; }

    private CustomerRequest()
    {
        // required by EF
    }

    public CustomerRequest(
        int userId,
        int serviceId,
        string description,
        DateTime? preferredDate = null)
    {
        UserId = userId;
        ServiceId = serviceId;
        Description = description;
        PreferredDate = preferredDate;
        Status = RequestStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(RequestStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string description, DateTime? preferredDate)
    {
        Description = description;
        PreferredDate = preferredDate;
        UpdatedAt = DateTime.UtcNow;
    }
}
