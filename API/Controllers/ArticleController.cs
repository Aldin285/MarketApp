using MarketApp.DataAccess.Repositories;
using MarketApp.DTO.Article;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Tags("Articles")]
    public class ArticleController : ControllerBase
    {
        
        ArticleDataAccess _articleDataAccess;

        public ArticleController(ArticleDataAccess articleDataAccess)
        {
            _articleDataAccess = articleDataAccess;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ArticleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = false)]
        
        public async Task<IActionResult> Get()
        {
            List<ArticleDto> articles = await _articleDataAccess.GetAllArticlesAsync();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = false)]
        
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                ArticleDto article = await _articleDataAccess.GetArticleByIdAsync(id);
                return Ok(article);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a new article.
        /// </summary>
        /// <param name="article">The article to create.</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Todo 
        ///     {
        ///         "CategoryId": 1,
        ///         "Title": "Test",
        ///         "Description": "Test",
        ///         "Price": 67,
        ///         "Unit": "Piece",
        ///         "StockQuantity": 1,
        ///         "MinimumStock": 1,
        ///         "IsActive": true
        ///     }
        /// </remarks>
        /// <response code="201">Returns the created article.</response>
        /// <response code="400">If the article is invalid or the title is already used</response>
        /// <returns>The created article.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = false)]
        
        public async Task<IActionResult> Post([FromBody] CreateArticleDto article)
        {
            try
            {
                ArticleDto createdArticle = await _articleDataAccess.CreateArticleAsync(article);
                return CreatedAtAction(nameof(Get), new { id = createdArticle.Id }, createdArticle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
