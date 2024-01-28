using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using DLL.Specifications;
using LibraryApi.Dto;
using DLL.Errors;
using LibraryRepositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerController : ControllerBase
    {
        private readonly IGenerRepository _generReopsitory;
        private readonly IMapper _mapper;

        public GenerController(IGenerRepository generReopsitory,IMapper mapper)
        {
            _generReopsitory = generReopsitory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gener>>> GetAll()
        {
            var Geners=await _generReopsitory.GetAllAsync();
            return Ok(Geners);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Gener>> Get(int id)
        {
            var gener = await _generReopsitory.GetAsync(id);
            if (gener == null)
                return NotFound(new Response(404));
            return Ok(gener);
          
        }
        [HttpGet("Details/{id}")]

        public async Task<ActionResult<GenerWithBooksDto>> GetWithSpec(int id)
        {
            var spec = new GenerSpecifications(G => G.Id == id);
            var gener = await _generReopsitory.GetWithSpecAsync(spec);
            if (gener == null)
                return NotFound(new Response(404));
            var GenerMapped = _mapper.Map<Gener, GenerWithBooksDto>(gener);

            return Ok(GenerMapped);
        }
        [HttpGet("Details")]
        public async Task<ActionResult<IEnumerable<GenerWithBooksDto>>> GetAllWithSpec()
        {
            var spec = new GenerSpecifications();
            var Geners = await _generReopsitory.GetAllWithSpecAsync(spec);
            var GenersMapped=_mapper.Map<IEnumerable<Gener>,IEnumerable< GenerWithBooksDto >> (Geners);
            return Ok(GenersMapped);
        }

    }
}
