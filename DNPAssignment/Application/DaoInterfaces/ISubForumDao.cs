using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ISubForumDao
{
    Task<SubForum> CreateAsync(SubForum subForum);
    Task<IEnumerable<SubForum>> GetAsync(SearchSubForumParametersDto searchParameters);
    Task UpdateAsync(SubForum subForum);
    Task<SubForum?> GetByIdAsync(int subForumId);
    
    Task DeleteAsync(int id);
}