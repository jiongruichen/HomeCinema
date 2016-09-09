using AutoMapper;
using HomeCinema.Entities;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Movie, MovieViewModel>()
                    .ForMember(vm => vm.Genre, map => map.MapFrom(m => m.Genre.Name))
                    .ForMember(vm => vm.GenreId, map => map.MapFrom(m => m.Genre.ID))
                    .ForMember(vm => vm.IsAvailable, map => map.MapFrom(m => m.Stocks.Any(s => s.IsAvailable)))
                    .ForMember(vm => vm.NumberOfStocks, map => map.MapFrom(m => m.Stocks.Count))
                    .ForMember(vm => vm.Image, map => map.MapFrom(m => string.IsNullOrEmpty(m.Image) == true ? "unknown.jpg" : m.Image));

            CreateMap<Genre, GenreViewModel>()
            .ForMember(vm => vm.NumberOfMovies, map => map.MapFrom(g => g.Movies.Count()));

            CreateMap<Customer, CustomerViewModel>();
        }
    }
}