using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class SubForumLogic : ISubForumLogic
{
    private readonly ISubForumDao iSubForumDao;
    private readonly IUserDao userDao;

    public SubForumLogic(ISubForumDao iSubForumDao, IUserDao userDao)
    {
        this.iSubForumDao = iSubForumDao;
        this.userDao = userDao;
    }

    public async Task<SubForum> CreateAsync(SubForumCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        SubForum sub = new SubForum(user, dto.Title);

        ValidateSubForum(sub);

        SubForum created = await iSubForumDao.CreateAsync(sub);
        return created;
    }

    public Task<IEnumerable<SubForum>> GetAsync(SearchSubForumParametersDto searchParameters)
    {
        return iSubForumDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(SubForumUpdateDto dto)
    {
        SubForum? existing = await iSubForumDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"SubForum with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;

        SubForum updated = new (userToUse, titleToUse)
        {
            Id = existing.Id,
        };

        ValidateSubForum(updated);

        await iSubForumDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        SubForum? subForum = await iSubForumDao.GetByIdAsync(id);
        if (subForum == null)
        {
            throw new Exception($"SubForum with ID {id} was not found!");
        }

        await iSubForumDao.DeleteAsync(id);
    }

    private void ValidateSubForum(SubForum dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}