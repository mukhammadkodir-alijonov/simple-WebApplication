using AutoMapper;
using simple_Web.DataAccess.Interfaces.Common;
using simple_Web.Service.Common.Exceptions;
using simple_Web.Service.Common.Helpers;
using simple_Web.Service.Dtos;
using simple_Web.Service.Interfaces.Common;
using simple_Web.Service.Interfaces;
using simple_Web.Service.ViewModels;
using System.Net;
using simple_Web.DataAccess.DbContexts;
using simple_Web.Domain.Entities;
using simple_Web.Service.Common.Utils;
using simple_Web.Domain.Enums;

namespace simple_Web.Service.Services
{
    }
public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppDbContext _appDbContext;
    public UserService(IMapper imapper, AppDbContext appDbContext, IUnitOfWork unitOfWork)
    {
        this._mapper = imapper;
        this._unitOfWork = unitOfWork;
        this._appDbContext = appDbContext;
    }
    public async Task<bool> DeleteAsync(List<int> ids)
    {
        foreach(var iteam in ids)
        {
            var temp = await _unitOfWork.Users.FindByIdAsync(iteam);
            if (temp is not null)
            {
                _unitOfWork.Users.Delete(iteam);
            }
        }
        var res = await _unitOfWork.SaveChangesAsync();
        return res > 0;
    }
    public async Task<bool> BlockAsync(List<int> ids)
    {
        foreach (var iteam in ids)
        {
            var temp = await _unitOfWork.Users.FindByIdAsync(iteam);
            if (temp is not null)
            {
                if(temp.Status != StatusType.Blocked)
                {
                    temp.Status = StatusType.Blocked;
                    _unitOfWork.Users.Update(iteam,temp);
                }
            }
        }
        var res = await _unitOfWork.SaveChangesAsync();
        return res > 0;
    }
    public async Task<bool> ActiveAsync(List<int> ids)
    {
        foreach (var iteam in ids)
        {
            var temp = await _unitOfWork.Users.FindByIdAsync(iteam);
            if (temp is not null)
            {
                if (temp.Status != StatusType.Active)
                {
                    temp.Status = StatusType.Active;
                    _unitOfWork.Users.Update(iteam, temp);
                }
            }
        }
        var res = await _unitOfWork.SaveChangesAsync();
        return res > 0;
    }
}
