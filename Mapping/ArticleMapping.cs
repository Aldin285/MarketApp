using System;
using MarketApp.DTO.Article;
using MarketApp.Models;
using AutoMapper;

namespace MarketApp.Mapping;

public class ArticleMapping :Profile
{
    public ArticleMapping()
        {
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticleDto, Article>();

            CreateMap<CreateArticleDto, Article>();
            CreateMap<Article, CreateArticleDto>();
        }
}
