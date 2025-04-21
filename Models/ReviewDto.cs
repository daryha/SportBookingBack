// DTO-класс, в отдельном файле, например Models/ReviewDto.cs
public class ReviewDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
    public string? CoachId { get; set; }
    public string? CoachName { get; set; }
    public string? FacilityId { get; set; }
    public string? FacilityName { get; set; }
}
