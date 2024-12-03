using Microsoft.AspNetCore.Mvc;
using UniversityForumAPI.Services;
using UniversityForumAPI.DTOs.TopicDTOs;
using UniversityForumAPI.DTOs.GroupDTOs;

namespace UniversityForumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _searchService;

        public SearchController(SearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string query, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("A pesquisa não pode ser vazia.");
            }

            // Chama o serviço de busca
            var (topics, groups) = await _searchService.SearchAsync(query, page, pageSize);

            // Verifica se não há resultados
            if (!topics.Any() && !groups.Any())
            {
                return NotFound("Nenhum tópico ou grupo encontrado.");
            }

            // Retorna os resultados
            var result = new
            {
                Topics = topics,
                Groups = groups
            };

            return Ok(result);
        }
    }
}
