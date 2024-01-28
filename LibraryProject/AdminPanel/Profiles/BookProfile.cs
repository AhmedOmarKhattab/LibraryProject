using AdminPanel.Models;
using AutoMapper;
using DAL.Models;

namespace AdminPanel.Profiles
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {
            CreateMap<BookViewModel, Book>().ReverseMap().
                ForMember(o=>o.AuthorName,s=>s.MapFrom(d=>d.Author.Name))
                .ForMember(o=>o.GenerName,s=>s.MapFrom(d=>d.Gener.Name));
            
        }
    }
}
