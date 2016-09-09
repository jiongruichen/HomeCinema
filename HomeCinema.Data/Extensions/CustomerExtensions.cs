using HomeCinema.Data.Infrastructure;
using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Data.Extensions
{
    public static class CustomerExtensions
    {
        public static bool UserExists(this IEntityBaseRepository<Customer> customersRepository, string email, string identityCard)
        {
            bool _userExists = false;

            _userExists = customersRepository.GetAll()
                .Any(c => c.Email.ToLower() == email ||
                c.IdentityCard.ToLower() == identityCard);

            return _userExists;
        }
    }
}