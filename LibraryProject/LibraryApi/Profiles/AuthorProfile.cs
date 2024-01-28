using AutoMapper;
using DAL.Models;
using LibraryApi.Dto;

namespace LibraryApi.Profiles
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorWithBooksDto>()
          .ForMember(G => G.Books, o => o.MapFrom(s => s.Books.Select(B => B.Title)));
        }


    }
}
