using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        HomeCinemaContext Init();
    }
}