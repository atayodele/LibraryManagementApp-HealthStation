using AutoMapper;
using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Extensions.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserForListDto>();
            CreateMap<Author, AuthorListDto>();
            CreateMap<Book, BookForListDto>();
            //CreateMap<BookFerryViewModel, AddBookDto>();
            //CreateMap<BookFerryUpdateVM, UpdateBookDto>();
            //CreateMap<UserForUpdateDto, AppUser>();
        }
    }
}
