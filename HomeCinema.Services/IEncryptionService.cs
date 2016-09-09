using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Services
{
    public interface IEncryptionService
    {
        string CreateSalt();
        string EncryptPassword(string password, string salt);
    }
}