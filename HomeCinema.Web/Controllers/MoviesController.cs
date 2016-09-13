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
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Movie> _moviesRepository;

        public MoviesController(IEntityBaseRepository<Movie> moviesRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) :base(errorsRepository, unitOfWork)
        {
            _moviesRepository = moviesRepository;
        }

        [AllowAnonymous]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var movies = _moviesRepository.GetAll().OrderByDescending(m => m.ReleaseDate).Take(6).ToList();
                var moviesVM = Mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(movies);
                
                response = request.CreateResponse(HttpStatusCode.OK, moviesVM);

                return response;
            });
        }

        [AllowAnonymous]
        [Route("{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            var currentPage = page.Value;
            var currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Movie> movies = null;
                var totalMovies = new int();

                if(!string.IsNullOrEmpty(filter))
                {
                    movies = _moviesRepository.GetAll().OrderBy(m => m.ID).Where(m => m.Title.ToLower().Contains(filter.ToLower().Trim())).ToList();
                }
                else
                {
                    movies = _moviesRepository.GetAll().ToList();
                }

                totalMovies = movies.Count();

                movies = movies.Skip(currentPage * currentPageSize).Take(currentPageSize).ToList();

                var moviesVM = Mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(movies);
                var pagedSet = new PaginationSet<MovieViewModel>
                {
                    Page = currentPage,
                    TotalCount = totalMovies,
                    TotalPages = (int)Math.Ceiling((decimal)totalMovies / currentPageSize),
                    Items = moviesVM
                };

                response = request.CreateResponse<PaginationSet<MovieViewModel>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }
    }
}