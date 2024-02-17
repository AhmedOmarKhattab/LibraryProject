using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using DLL;
using DLL.Specifications;
using LibraryApi.Dto;
using DLL.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository,IMapper mapper)
        {
           _bookRepository = bookRepository;
            _mapper = mapper;
        }
        [HttpGet] 
        public async Task<ActionResult<Pagination<BookWithGenerAndAuthorDto>>> GetAll([FromQuery]BookSpecParams bookSpecParams) 
        {
            var spec=new BookSpecifications(bookSpecParams);
            var books = await _bookRepository.GetAllWithSpecAsync(spec);

            var BooksMapped=_mapper.Map<IReadOnlyList<Book>,IReadOnlyList<BookWithGenerAndAuthorDto>>(books);
            var SpecCount = new BookSpecifications(bookSpecParams, true);
            var count=await _bookRepository.GetCountWithSpecAsync(SpecCount);
            var result =  new Pagination<BookWithGenerAndAuthorDto>(bookSpecParams.PageSize,bookSpecParams.PageIndex,count,BooksMapped);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookWithGenerAndAuthorDto>>GetBookById(int id)
        {
            var spec=new BookSpecifications(B=>B.Id==id);
            var book = await _bookRepository.GetWithSpecAsync(spec);
            if (book==null)
                return NotFound(new Response(404));
            var BookMapped=_mapper.Map<Book,BookWithGenerAndAuthorDto>(book);
            return Ok(BookMapped);
        }
        [HttpGet("{Title}:alpha")]
        public async Task<ActionResult<IEnumerable<BookWithGenerAndAuthorDto>>> GetBookByName(string Title)
        {
            var spec = new BookSpecifications(B => B.Title==Title);
            var books = await _bookRepository.GetBookByNameAsync(spec);
            var BooksMapped=_mapper.Map<IEnumerable<Book>,IEnumerable<BookWithGenerAndAuthorDto>>(books);
            return Ok(BooksMapped);
        }

    }
}
