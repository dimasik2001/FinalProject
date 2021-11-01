using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OlxAPI.Data.Entities;
using OlxAPI.Models.PostModels;
using OlxAPI.Models.ViewModels;
using OlxAPI.Services.Models;

namespace OlxAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //PL
            CreateMap<AdModel, AdViewModel>();
            CreateMap<CategoryModel, CategoryViewModel>();
            CreateMap<AdPostModel, AdModel>();
            CreateMap<CategoryPostModel, CategoryModel>();

            //BL
            CreateMap<AdsCategories, CategoryModel>()
                .ForMember(model => model.Id,
                opts => opts.MapFrom(entity => entity.CategoryId))
                .ForMember(model => model.Name,
                 opts => opts.MapFrom(entity => entity.Category.Name));
            CreateMap<CategoryModel, AdsCategories>().ForMember(entity => entity.CategoryId,
               opts => opts.MapFrom(model => model.Id))
                .ForMember(entity => entity.Id, opts => opts.Ignore());
            CreateMap<Image, string>();
            CreateMap<Ad, AdModel>()
                .ForMember(model => model.Categories,
                opts => opts
                .MapFrom(entity => entity.AdsCategories))
                .ForMember(model => model.Images,
                opts => opts
                .MapFrom(entity => entity.Images.Select(t => new { Image = t.Path })));
            CreateMap<CategoryModel, Category>();
            CreateMap<Category, CategoryModel>();
            CreateMap<Image, string>();
            CreateMap<AdModel, Ad>()
                .ForMember(entity => entity.AdsCategories,
                opts => opts
                .MapFrom(model => model.Categories))
                .ForMember(entity => entity.Images,
                opts => opts
                .MapFrom(model => model.Images.Select(t => new {Path = t})));
        }
    }


}
