using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class SubForumFileDao : ISubForumDao
{
    private readonly FileContext context;

    public SubForumFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<SubForum> CreateAsync(SubForum subForum)
    {
        int id = 1;
        if (context.SubForums.Any())
        {
            id = context.SubForums.Max(t => t.Id);
            id++;
        }

        subForum.Id = id;

        context.SubForums.Add(subForum);
        context.SaveChanges();

        return Task.FromResult(subForum);
    }

    public Task<IEnumerable<SubForum>> GetAsync(SearchSubForumParametersDto searchParams)
    {
        IEnumerable<SubForum> result = context.SubForums.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            result = context.SubForums.Where(subForum =>
                subForum.Owner.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParams.UserId != null)
        {
            result = result.Where(t => t.Owner.Id == searchParams.UserId);
        }

        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParams.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task<SubForum?> GetByIdAsync(int subForumId)
    {
        SubForum? existing = context.SubForums.FirstOrDefault(t => t.Id == subForumId);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        SubForum? existing = context.SubForums.FirstOrDefault(subForum => subForum.Id == id);
        if (existing == null)
        {
            throw new Exception($"SubForum with id {id} does not exist!");
        }

        context.SubForums.Remove(existing); 
        context.SaveChanges();

        return Task.CompletedTask;
    }

    public Task UpdateAsync(SubForum toUpdate)
    {
        SubForum? existing = context.SubForums.FirstOrDefault(subForum => subForum.Id == toUpdate.Id);
        if (existing == null)
        {
            throw new Exception($"SubForum with id {toUpdate.Id} does not exist!");
        }

        context.SubForums.Remove(existing);
        context.SubForums.Add(toUpdate);
        
        context.SaveChanges();
        
        return Task.CompletedTask;
    }
}