using EaindrayDotNetCore.ConsoleApp.RefitExamples;
using Microsoft.AspNetCore.Mvc;

namespace EaindrayDotNetCore.MvcApp.Controllers
{
    public class BlogRefitController : Controller
    {
        private readonly IBlogApi _blogApi;

        public BlogRefitController(IBlogApi blogApi)
        {
            _blogApi = blogApi;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _blogApi.GetBlogs();
            return View(model);
        }
    }
}