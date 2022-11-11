namespace Domain.DTOs;

public class SearchSubForumParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    public string? TitleContains { get;}

    public SearchSubForumParametersDto(string? username, int? userId, string? titleContains)
    {
        Username = username;
        UserId = userId;
        TitleContains = titleContains;
    }
}