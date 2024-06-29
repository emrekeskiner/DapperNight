using DapperNight.Dtos.ProductDtos;
using DapperNight.Services.CategoryServices;
using DapperNight.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DapperNight.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetAllProductAsync();
            return View(values);
        }

        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.categories = await _categoryService.GetAllCategoryAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var category = createProductDto.CategoryId;
            if (category < 1)
            {
                ModelState.AddModelError("CategoryId", "Kategori Seçili değil.");
                ViewBag.Categories = await _categoryService.GetAllCategoryAsync();
                return View(createProductDto);
            }
            await _productService.CreateProductAsync(createProductDto);
            return RedirectToAction("ProductList");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            List<SelectListItem> category = (from i in await _categoryService.GetAllCategoryAsync()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryId.ToString()
                                             }).ToList();
            ViewBag.categories = category;
           var value = await _productService.GetByIdProductAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return RedirectToAction("ProductList");
        }
    }
}
