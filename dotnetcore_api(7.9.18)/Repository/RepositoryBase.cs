using MFI;
using MFI.Repository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDb RepositoryContext { get; set; }
        public string _OldObjString { get; set; }
               
        public RepositoryBase(AppDb repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IEnumerable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression);
        }

        public void Create(T entity, bool flush = true)
        {
            this.RepositoryContext.Set<T>().Add(entity);
            if (flush) this.Save();
        }

        public void Update(T entity, bool flush = true)
        {
            this.RepositoryContext.Set<T>().Update(entity);
            if (flush) this.Save();
        }

        public void Delete(T entity, bool flush = true)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
            if (flush) this.Save();
        }

        public void Save()
        {
            this.RepositoryContext.SaveChanges();
        }

        public void SetOldObjectToString(dynamic OldObj)
        {
            _OldObjString = "";
            JObject _duplicateObj = JObject.FromObject(OldObj);
            var _List = _duplicateObj.ToObject<Dictionary<string, object>>();
            foreach (var item in _List)
            {
                var name = item.Key;
                var val = item.Value;
                string msg = name + " : " + val + "\r\n";
                _OldObjString += msg;
            }
        }

        public string GetOldObjectString()
        {
            return this._OldObjString;
        }
    }
}
