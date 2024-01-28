using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using DLL.Repositories;
using DLL.Specifications;
using LibraryApi.Dto;
using DLL.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorepository,IMapper mapper )
        {
            _authorRepository = authorepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAll()
        {
            var Authors = await _authorRepository.GetAllAsync();
            return Ok(Authors);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> Get(int id)
        {
            var Author = await _authorRepository.GetAsync(id);
            if(Author == null)
                return NotFound(new Response(404));
            return Ok(Author);

        }
        [HttpGet("Details/{id}")]

        public async Task<ActionResult<AuthorWithBooksDto>> GetWithSpec(int id)
        {
            var spec = new AuthorSpecifications(G => G.Id == id);
            var Author = await _authorRepository.GetWithSpecAsync(spec);
            if(Author == null)
                return NotFound(new Response(404));
            var AuthorMapped = _mapper.Map<Author, AuthorWithBooksDto>(Author);
            
            return Ok(AuthorMapped);
        }
        [HttpGet("Details")]
        public async Task<ActionResult<IEnumerable<AuthorWithBooksDto>>> GetAllWitSpec()
        {
            var spec = new AuthorSpecifications();
            var Authors = await _authorRepository.GetAllWithSpecAsync(spec);
            var AuthorsMapped = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorWithBooksDto>>(Authors);
            return Ok(AuthorsMapped);
        }

    }
}
