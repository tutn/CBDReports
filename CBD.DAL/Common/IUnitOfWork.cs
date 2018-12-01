using CBD.DAL.Entities;
using CBD.DAL.Repositories;
using System;

namespace CBD.DAL.Common
{
    public interface IUnitOfWork : IDisposable
    {
        CBDDbContext DbContext { get; }
        IRepository<TBL_SYS_DIMDATE, int> DimDateRepository { get; }
        ISys_PagesRepository PageRepository { get; }
        ISys_UnitsRepository UnitRepository { get; }
        ISys_UsersRepository UserRepository { get; }
        void SaveChanges();
    }
}