using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.GenericRepository.IntRep
{
    public interface IRepository<T>  where T : BaseEntity
    {
        void Save();
        void Add(T item);
        void AddRange(List<T> list);
        void Update(T item);
        void UpdateRange(List<T> list);
        /// <summary>
        /// Pasife ceker
        /// </summary>
        /// <param name="item"></param>
        void Delete(T item);
        void DeleteRange(List<T> list);

        List<T> GetAll();
        List<T> GetActives();
        List<T> GetModifieds();
        List<T> GetPassives();

        List<T> GetFirstDatas(int number);
        List<T> GetLastDatas(int number);
        T Find(int id);

        bool Any(Expression<Func<T,bool>> exp);
        IQueryable<X> Select<X>(Expression<Func<T,X>> exp);
        object Select(Expression<Func<T,object>> exp);
        List<T> Where(Expression<Func<T,bool>> exp);
        T FirstOrDefault(Expression<Func<T, bool>> exp);

        

    }
}
