using AdminPanel.Helpers;
using AdminPanel.Models;
using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using DLL.Errors;
using DLL.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAdminRepository<Author> _adminRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAdminRepository<Author> adminRepository,IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Authors =  await _adminRepository.AuthorRepository.GetAllAsync();
            var AuthorsMapped=_mapper.Map<IEnumerable< Author>,IEnumerable< AuthorViewModel>>(Authors);
            return View(AuthorsMapped);
        }
        [HttpGet]
        public  IActionResult Create()
        {
            return  View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuthorViewModel authorViewModel)
        {
            if(ModelState.IsValid)
            {
                var AuthorMapped=_mapper.Map<AuthorViewModel,Author>(authorViewModel);
                await _adminRepository.Add(AuthorMapped);
                await _adminRepository.Complete();
                return RedirectToAction("Index");
            }
            return View(authorViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            if (id == null)
                return BadRequest(new Response(400));
            var author = await _adminRepository.AuthorRepository.GetAsync(id.Value);
            if (author == null)
                return NotFound(new Response(404));
            var authorMapped = _mapper.Map<Author, AuthorViewModel>(author);
            return View(authorMapped);
        }
        [HttpPost]

        public async Task<IActionResult> Edit([FromRoute]int id, AuthorViewModel authorViewModel)
        {
            if (id != authorViewModel.Id)
                return BadRequest(new Response(400));
            try
            {
                var author = _mapper.Map<AuthorViewModel, Author>(authorViewModel);
                _adminRepository.Update(author);
                await _adminRepository.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(authorViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details([FromRoute]int?id,string ViewName="Details")
        {
            if(id == null)
                return BadRequest(new Response(400));
            var spec=new AuthorSpecifications(A=>A.Id==id);
            var Author=await _adminRepository.AuthorRepository.GetWithSpecAsync(spec);
            if (Author==null)
                return  NotFound(new Response(404));
            var AuthorMapped = _mapper.Map<Author, AuthorViewModel>(Author);
            return View(ViewName,AuthorMapped);
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]int?id)
        {
            return await Details(id,"Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int?id,AuthorViewModel authorViewModel)
        {
            if(id!=authorViewModel.Id)
                return BadRequest(new Response(400));
            var AuthorMapped = _mapper.Map<AuthorViewModel, Author>(authorViewModel);
            try
            {
                _adminRepository.Delete(AuthorMapped);
                 await _adminRepository.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(authorViewModel);
        }
    }
}
