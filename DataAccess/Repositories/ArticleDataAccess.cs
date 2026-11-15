using System;
using AutoMapper;
using MarketApp.Constants;
using MarketApp.DTO.Article;
using MarketApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketApp.DataAccess.Repositories;

public class ArticleDataAccess
{
    private readonly PostgresContext _contexte;
    private readonly IMapper _mapper;

    public ArticleDataAccess(PostgresContext contexte, IMapper mapper)
    {
        _contexte = contexte;
        _mapper = mapper;
    }

    /// <summary>
    /// Fetches all articles from the database.
    /// </summary>
    /// <returns>List of all articles.</returns>
    public async Task<List<ArticleDto>> GetAllArticlesAsync()
    {
        List<Article> articles = await _contexte.Articles.ToListAsync();
        return _mapper.Map<List<ArticleDto>>(articles);
    }

    /// <summary>
    /// Fetches an article by its ID.
    /// </summary>
    /// <param name="id">The ID of the article to fetch.</param>
    /// <returns>The article with the specified ID, or null if not found.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the article is not found.</exception>
    public async Task<ArticleDto> GetArticleByIdAsync(long id)
    {
        Article? response = await _contexte.Articles.FirstOrDefaultAsync(c => c.Id == id);

        if (response == null)
        {
            throw new KeyNotFoundException($"Article with ID {id} not found.");
        }

        return _mapper.Map<ArticleDto>(response);
    }

    /// <summary>
    /// Creates a new article.
    /// </summary>
    /// <param name="article">The article to create.</param>
    /// <returns>The created article.</returns>
    /// <exception cref="Exception">Thrown when the article title is already used.</exception>
    public async Task<ArticleDto> CreateArticleAsync(CreateArticleDto article)
    {
        // Check if an article with the same name already exists
        Article existingArticle = await _contexte.Articles.FirstOrDefaultAsync(a => a.Title == article.Title);
        if (existingArticle != null)
        {
            throw new Exception($"{ErrorConstants.ELEMENT_ALREADY_EXISTS} (Title, {article.Title})");
        }

        Article newArticle = _mapper.Map<Article>(article);
        _contexte.Articles.Add(newArticle);
        await _contexte.SaveChangesAsync();
        ArticleDto createdArticleDto = _mapper.Map<ArticleDto>(newArticle);
        return createdArticleDto;
    }

    
}
