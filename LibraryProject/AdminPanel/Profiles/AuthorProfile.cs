using AdminPanel.Models;
using AutoMapper;
using DAL.Models;

namespace AdminPanel.Profiles
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
        CreateMap<AuthorViewModel, Author>().ReverseMap();

        }
    }
}
