using AdminPanel.Models;
using AutoMapper;
using DAL.Models;

namespace AdminPanel.Profiles
{
    public class GenerProfile:Profile
    {
        public GenerProfile()
        {
        CreateMap<GenerViewModel, Gener>().ReverseMap();

        }
    }
}
