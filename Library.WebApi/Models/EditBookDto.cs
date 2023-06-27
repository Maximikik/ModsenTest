﻿using AutoMapper;
using Library.Application.Books.Commands.CreateBook;
using Library.Application.Books.Commands.EditBook;
using Library.Application.Common.Mapping;

namespace Library.WebApi.Models
{
    public class EditBookDto: IMapWith<EditBookCommand>
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime RentStart { get; set; }
        public DateTime RentEnd { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditBookDto, EditBookCommand>()
                .ForMember(bookCommand => bookCommand.ISBN,
                opt => opt.MapFrom(book => book.ISBN))
                .ForMember(bookCommand => bookCommand.Name,
                opt => opt.MapFrom(book => book.Name))
                .ForMember(bookCommand => bookCommand.Genre,
                opt => opt.MapFrom(book => book.Genre))
                .ForMember(bookCommand => bookCommand.Description,
                opt => opt.MapFrom(book => book.Description))
                .ForMember(bookVm => bookVm.Author,
                opt => opt.MapFrom(book => book.Author))
                .ForMember(bookCommand => bookCommand.RentStart,
                opt => opt.MapFrom(book => book.RentStart))
                .ForMember(bookCommand => bookCommand.RentEnd,
                opt => opt.MapFrom(book => book.RentEnd));
        }
    }
}
