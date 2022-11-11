namespace Domain.DTOs;

public class SubForumUpdateDto
{
    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }

    public SubForumUpdateDto(int id)
    {
        Id = id;
    }
}