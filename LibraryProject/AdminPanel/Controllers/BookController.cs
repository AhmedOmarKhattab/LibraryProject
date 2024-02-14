using AdminPanel.Helpers;
using AdminPanel.Models;
using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using DLL;
using DLL.Data;
using DLL.Errors;
using DLL.Repositories;
using DLL.Specifications;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;
namespace AdminPanel.Controllers
{
    public class BookController : Controller
    {
        private readonly IAdminRepository<Book> _adminRepository;
        private readonly IMapper _mapper;

        public BookController(IAdminRepository<Book> adminRepository,IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
           
        }
       
        public async Task<IActionResult> Index(BookSpecParams bookSpecParams,string searchname)
        {
            IEnumerable<BookViewModel> BooksMapped;
            IEnumerable<Book> Books;
            if (!String.IsNullOrEmpty(searchname))
            {
                var spec = new BookSpecifications(e => e.Title.ToLower().Contains(searchname.ToLower()));
                Books = await _adminRepository.BookRepository.GetBookByNameAsync(spec);
            }
            else
            {
                var spec = new BookSpecifications(bookSpecParams);
                Books = await _adminRepository.BookRepository.GetAllWithSpecAsync(spec);
            }
            ViewBag.Geners=await _adminRepository.GenerRepository.GetAllAsync();
            ViewBag.Authors = await _adminRepository.AuthorRepository.GetAllAsync();
            BooksMapped = _mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(Books);
            return View(BooksMapped);
        }
        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            ViewBag.Geners =await  _adminRepository.GenerRepository.GetAllAsync();
            ViewBag.Authors = await _adminRepository.AuthorRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            if(ModelState.IsValid)
            {
                book.PictureUrl = await DocumentSetting.UploadFile(book.Image, "Images");
                var bookmapped =  _mapper.Map<BookViewModel, Book>(book);
               await _adminRepository.Add(bookmapped);
                await _adminRepository.Complete();
                return RedirectToAction("Index");
            }
            return View(book);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id )
        {
            if(id == null)
                return BadRequest(new Response(400));
            ViewBag.Geners = await _adminRepository.GenerRepository.GetAllAsync();
            ViewBag.Authors = await _adminRepository.AuthorRepository.GetAllAsync();
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit([FromRoute] int? id,BookViewModel bookViewModel)
        {
            if (id != bookViewModel.Id)
                return BadRequest(new Response(400));
            if (ModelState.IsValid)
            {

                if (bookViewModel.Image != null)
                {     
                    bookViewModel.PictureUrl =await DocumentSetting.UploadFile(bookViewModel.Image, "Images");
                }
                
                var book = _mapper.Map<BookViewModel, Book>(bookViewModel);

                try
                {
                    _adminRepository.Update(book);
                    await _adminRepository.Complete();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(bookViewModel);
        }
        public async Task<IActionResult> Details([FromRoute]int?id,string ViewName="Details")
        {
            if (id is null)
                return BadRequest(new Response(400));
            var spec = new BookSpecifications(B => B.Id == id);
            var book = await _adminRepository.BookRepository.GetWithSpecAsync(spec);
            if (book == null)
                return NotFound(new Response(404));
            var BookMapped = _mapper.Map<Book, BookViewModel>(book);
            return View(ViewName,BookMapped);
        }
        [HttpGet]
       public async Task<IActionResult> Delete([FromRoute]int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult>Delete([FromRoute]int? id,BookViewModel bookViewModel)
        {
            if(bookViewModel.Id != id)
                return BadRequest(new Response(400));
            var book=_mapper.Map<BookViewModel,Book>(bookViewModel);
            try
            {
                _adminRepository.Delete(book);
                int count = await _adminRepository.Complete();
                if (count > 0)
                    DocumentSetting.DeleteFile(book.PictureUrl, "Images");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(bookViewModel);
        }
    }
}
