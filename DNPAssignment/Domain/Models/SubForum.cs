namespace Domain.Models;

public class SubForum
{
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }

    public SubForum(User owner, string title)
    {
        this.Owner = owner;
        this.Title = title;
    }
}