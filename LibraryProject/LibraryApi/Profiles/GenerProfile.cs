using AutoMapper;
using DAL.Models;
using LibraryApi.Dto;

namespace LibraryApi.Profiles
{
    public class GenerProfile:Profile
    {
        public GenerProfile()
        {
            CreateMap<Gener, GenerWithBooksDto>()
           .ForMember(G => G.Books, o => o.MapFrom(s => s.Books.Select(B => B.Title)));
        }


    }
}
