using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MFI.Repository
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity, bool flush = true);
        void Update(T entity, bool flush = true);
        void Delete(T entity, bool flush = true);
        void Save();
    }
}
