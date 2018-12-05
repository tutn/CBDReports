using CBD.DAL.Entities;
using CBD.DAL.Repositories;
using System;

namespace CBD.DAL.Common
{
    public interface IUnitOfWork : IDisposable
    {
        CBDDbContext DbContext { get; }
        IRepository<TBL_SYS_DIMDATE, int> DimDateRepository { get; }
        IPageRepository PageRepository { get; }
        IUnitRepository UnitRepository { get; }
        IUserRepository UserRepository { get; }
        IGroupRepository GroupRepository { get; }
        IUnitUserRepository UnitUserRepository { get; }
        IRepository<TBL_SYS_GROUP_USERS, int> GroupUserRepository { get; }
        IUnitGroupPageRepository UnitGroupPageRepository { get; }
        void SaveChanges();
    }
}