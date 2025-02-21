using AutoMapper;
using AzureTutorials.AppService.DataLayer;
using AzureTutorials.AppService.Dtos;

namespace AzureTutorials.AppService.Infrastructures
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<Todo, TodoDto>();
        }
    }
}