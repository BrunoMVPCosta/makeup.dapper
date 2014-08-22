using MakeUpORM.Mapping;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUp
{
    public class DatabaseModel
    {
        private readonly ConcurrentDictionary<Type, IEntityMapper> _typeMapping;

        public DatabaseModel()
        {
            _typeMapping = new ConcurrentDictionary<Type, IEntityMapper>(); 
        }

        public void AddMapping<T>(EntityMapper<T> mapper) where T : class
        {
            this._typeMapping[typeof(T)] = mapper;
        }

        public ICollection<IEntityMapper> Mappers()
        {
           return this._typeMapping.Select(p=> p.Value).ToList();
        }

        public IEntityMapper MapperWithType(Type type)
        {
            return this._typeMapping[type];
        }
    }
}
