using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace medical_appointment_system.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Update(T entity);
        void Delete(int id);
    }
}