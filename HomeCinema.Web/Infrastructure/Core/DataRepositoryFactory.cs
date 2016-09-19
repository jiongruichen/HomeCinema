using HomeCinema.Data.Infrastructure;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Core
{
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        public IEntityBaseRepository<T> GetDataRepository<T>(HttpRequestMessage request) where T : class, IEntityBase, new()
        {
            return request.GetDataRepository<T>();
        }
    }

    public interface IDataRepositoryFactory
    {
        IEntityBaseRepository<T> GetDataRepository<T>(HttpRequestMessage request) where T : class, IEntityBase, new();
    }
}