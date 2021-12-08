using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OlxAPI.Data.Entities;
using OlxAPI.Data.Parameters;
using OlxAPI.Enums;
using OlxAPI.Models.DeleteModels;
using OlxAPI.Models.PostModels;
using OlxAPI.Models.PostModels.Parameters;
using OlxAPI.Models.ViewModels;
using OlxAPI.Models.ViewModels.Parameters;
using OlxAPI.Services.Models;

namespace OlxAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //PL
            CreateMap<AdDeleteModel, AdModel>();
            CreateMap<AdModel, AdViewModel>();
            CreateMap<CategoryModel, CategoryViewModel>();
            CreateMap<AdPostModel, AdModel>();
            CreateMap<CategoryPostModel, CategoryModel>();
            CreateMap<PaginationQueryParameters, PaginationParametersModel>();
            CreateMap<PaginationParametersModel, PaginationParametersViewModel>();
            CreateMap<FilterQueryParameters, FilterParametersModel>();
            CreateMap<SortQueryParameters, SortParametersModel>();
            CreateMap<User, UserViewModel>();

            //BL
            CreateMap<PaginationParametersModel, PaginationParameters>();
            CreateMap<PaginationParameters,PaginationParametersModel>();
            CreateMap<FilterParametersModel, FilterParameters>()
                .ConvertUsing((src, dest, ctx) => 
                {
                    dest = new FilterParameters();
                    dest.Predicates = new List<Expression<Func<Ad, bool>>>();
                    if(src.DateFrom.HasValue)
                    {
                        dest.Predicates.Add(ad => ad.ChangeDate >= src.DateFrom);
                    }
                    if(src.DateTo.HasValue)
                    {
                        dest.Predicates.Add(ad => ad.ChangeDate <= src.DateTo);
                    }
                    if (src.PriceFrom.HasValue)
                    {
                        dest.Predicates.Add(ad => ad.Price >= src.PriceFrom);
                    }
                    if (src.PriceTo.HasValue)
                    {
                        dest.Predicates.Add(ad => ad.Price <= src.PriceTo);
                    }
                    if(src.FilterItem.HasValue && !string.IsNullOrEmpty(src.ItemId))
                    {
                        switch (src.FilterItem)
                        {
                            case FilterItemEnum.UserId:
                                dest.Predicates.Add(ad => ad.UserId == src.ItemId);
                                break;
                            case FilterItemEnum.CategoryId:
                                if(int.TryParse(src.ItemId, out var id))
                                {
                                    dest.Predicates.Add(ad => ad.AdsCategories.Any(c => c.CategoryId == id));
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if(!string.IsNullOrEmpty(src.KeyWords))
                    {
                        dest.Predicates.Add(ad => ad.Header.Contains(src.KeyWords));
                    }

                    if(dest.Predicates.Count == 0)
                    {
                        return null;
                    }
                    return dest;
                });
            CreateMap<SortParametersModel, SortParameters>()
                .ConvertUsing((src, dest, ctx) =>
                {
                    if(!src.SortItem.HasValue)
                    {
                        return null;
                    }
                    dest = new SortParameters();
                    dest.IsAscending = src.SortDirection == SortDirectionEnum.Ascending;
                    switch (src.SortItem)
                    {
                        case SortItemEnum.Price:
                            dest.SortFunc = ad => ad.Price;
                            break;
                        case SortItemEnum.ChangeDate:
                            dest.SortFunc = ad => ad.ChangeDate;
                            break;
                        default:
                            break;
                    }
                    
                    return dest;
                }
                ); 
            CreateMap<AdsCategories, CategoryModel>()
                .ForMember(model => model.Id,
                opts => opts.MapFrom(entity => entity.CategoryId))
                .ForMember(model => model.Name,
                 opts => opts.MapFrom(entity => entity.Category.Name));
            CreateMap<CategoryModel, AdsCategories>().ForMember(entity => entity.CategoryId,
               opts => opts.MapFrom(model => model.Id))
                .ForMember(entity => entity.Id, opts => opts.Ignore());
            CreateMap<ICollection<Image>, IEnumerable<string>>()
                .ConvertUsing((src, dest, ctx) =>
                {
                    var list = new List<string>();
                    foreach (var image in src)
                    {
                        list.Add(image.Path);
                    }
                    dest = list;
                    return dest;
                });
            CreateMap<Image, string>()
                .ConvertUsing((src, dest, ctx) =>
                {
                    dest = src.Path;
                    return dest;
                });
            CreateMap<Ad, AdModel>()
                .ForMember(model => model.Categories,
                opts => opts
                .MapFrom(entity => entity.AdsCategories));
            CreateMap<CategoryModel, Category>();
            CreateMap<Category, CategoryModel>();
            CreateMap<Image, string>();
            CreateMap<AdModel, Ad>()
                .ForMember(entity => entity.AdsCategories,
                opts => opts
                .MapFrom(model => model.Categories));
        }
    }


}
