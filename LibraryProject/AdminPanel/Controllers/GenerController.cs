using AdminPanel.Helpers;
using AdminPanel.Models;
using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using DLL.Errors;
using DLL.Repositories;
using DLL.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class GenerController : Controller
    {
        private readonly IAdminRepository<Gener> _adminRepository;
        private readonly IMapper _mapper;

        public GenerController(IAdminRepository<Gener> adminRepository,IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Geners = await _adminRepository.GenerRepository.GetAllAsync();
            var GenersMapped = _mapper.Map<IEnumerable< Gener>, IEnumerable< GenerViewModel>>(Geners);
            return View(GenersMapped);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenerViewModel authorViewModel)
        {
            if(ModelState.IsValid)
            {
                var GenerMapped=_mapper.Map<GenerViewModel,Gener>(authorViewModel);
                await _adminRepository.Add(GenerMapped);
                await _adminRepository.Complete();
                return RedirectToAction("Index");
            }
            return View(authorViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int? id)
        {
            if (id == null)
                return BadRequest(new Response(400));
            var gener =await _adminRepository.GenerRepository.GetAsync(id.Value);
            if (gener == null)
                return NotFound(new Response(404));
            var GenerMapped = _mapper.Map<Gener, GenerViewModel>(gener);
            return View(GenerMapped);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id,GenerViewModel GenerViewModel)
        {
            if(id!=GenerViewModel.Id) 
                return BadRequest(new Response(400));
            try
            {
                var Gener = _mapper.Map<GenerViewModel, Gener>(GenerViewModel);
                 _adminRepository.Update(Gener);
                await _adminRepository.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(GenerViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details([FromRoute]int?id,string ViewName="Details")
        {
            if (id==null)
                return BadRequest(new Response(400));
            var spec = new GenerSpecifications(g=>g.Id==id);
            var gener =await  _adminRepository.GenerRepository.GetWithSpecAsync(spec);
            if (gener==null)
                return NotFound(new Response(404));
            var genermapped = _mapper.Map<Gener, GenerViewModel>(gener);
            return View(ViewName,genermapped);
        }
        [HttpGet]
        public async Task<IActionResult>Delete([FromRoute]int?id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult>Delete([FromRoute]int?id, GenerViewModel gener)
        {
            if (id!=gener.Id)
                return BadRequest(new Response(400)  );
           var GenerMapped=_mapper.Map<GenerViewModel,Gener>(gener);
            try
            {
                _adminRepository.Delete(GenerMapped);
                int count = await _adminRepository.Complete();
                if (count > 0)
                {
                    foreach (var item in GenerMapped.Books)
                        DocumentSetting.DeleteFile(item.PictureUrl, "Images");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(gener);
        }
    }
}
