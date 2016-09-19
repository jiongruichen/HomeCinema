using AutoMapper;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HomeCinema.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/rentalsextended")]
    public class RentalsExtendedController : ApiControllerBaseExtended
    {
        public RentalsExtendedController(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork)
        : base(dataRepositoryFactory, unitOfWork)
        {
        }

        [HttpPost]
        [Route("rent/{customerId:int}/{stockId:int}")]
        public HttpResponseMessage Rent(HttpRequestMessage request, int customerId, int stockId)
        {
            _requiredRepositories = new List<Type>() { typeof(Customer), typeof(Stock), typeof(Rental) };

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;

                var customer = _customersRepository.GetSingle(customerId);
                var stock = _stocksRepository.GetSingle(stockId);

                if (customer == null || stock == null)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Customer or Stock");
                }
                else
                {
                    if (stock.IsAvailable)
                    {
                        var rental = new Rental()
                        {
                            CustomerId = customerId,
                            StockId = stockId,
                            RentalDate = DateTime.Now,
                            Status = "Borrowed"
                        };

                        _rentalsRepository.Add(rental);

                        stock.IsAvailable = false;

                        _unitOfWork.Commit();

                        var rentalVm = Mapper.Map<Rental, RentalViewModel>(rental);

                        response = request.CreateResponse<RentalViewModel>(HttpStatusCode.Created, rentalVm);
                    }
                    else
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Selected stock is not available anymore");
                    }
                }

                return response;
            });
        }
    }
}