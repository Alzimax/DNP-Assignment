namespace Domain.DTOs;

public class SubForumCreationDto
{
    public int OwnerId { get; }
    public string Title { get; }

    public SubForumCreationDto(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
    }
}