using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalROrnekProje.Models;
using SignalROrnekProje.Models.ViewModel;
using SignalROrnekProje.Services;
using System.Diagnostics;

namespace SignalROrnekProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _appDbContext;
        private readonly FileService _fileService;

        public HomeController(ILogger<HomeController> logger,UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,AppDbContext appDbContext,FileService fileService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {

            if (!ModelState.IsValid) return View(model);

            var hasUser = await _userManager.FindByEmailAsync(model.Email);

            if(hasUser is null)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is wrong!");

            }

            var result = await _signInManager.PasswordSignInAsync(hasUser, model.Password, true, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is wrong");

            }

            return RedirectToAction(nameof(Index));
        }
      
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {

            if(!ModelState.IsValid) return View(model);

            var userToCreate = new IdentityUser()
            {

                UserName = model.Email,
                Email = model.Email

            };

            var result = await _userManager.CreateAsync(userToCreate,model.Password);

            if (!result.Succeeded)
            {

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            return RedirectToAction(nameof(SignIn));
        }

        public async Task<IActionResult> ProductList()
        {
            var user = await _userManager.FindByEmailAsync("nurhak@gmail.com");
            
            if (_appDbContext.Products.Any(x=>x.UserId==user!.Id))
            {
                var products = _appDbContext.Products.Where(x => x.UserId == user.Id).ToList(); 
                
                return View(products);
            
            }


            var productList = new List<Product>()
            {

                new Product() { Name = "kalem1",Description = "Description",Price = 100,UserId = user!.Id},
                new Product() { Name = "kalem2",Description = "Description",Price = 100,UserId = user!.Id},
                new Product() { Name = "kalem3",Description = "Description",Price = 100,UserId = user!.Id},
                new Product() { Name = "kalem4",Description = "Description",Price = 100,UserId = user!.Id},
                new Product() { Name = "kalem5",Description = "Description",Price = 100,UserId = user!.Id}

            };

            _appDbContext.Products.AddRange(productList);
            await _appDbContext.SaveChangesAsync();
         

            return View(productList);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateExcel()
        {

            var response = new { Status = await _fileService.AddMessageToQueue() };

            return Json(response);


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
