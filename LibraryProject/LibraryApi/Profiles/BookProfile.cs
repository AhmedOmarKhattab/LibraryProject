using AutoMapper;
using DAL.Models;
using LibraryApi.Dto;

namespace LibraryApi.Profiles
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {
            CreateMap<Book,BookWithGenerAndAuthorDto>()
                .ForMember(B=>B.AuthorName,o=>o.MapFrom(s=>s.Author.Name))
                .ForMember(B=>B.Gener,o=>o.MapFrom(s=>s.Gener.Name));
        }

    }
}
