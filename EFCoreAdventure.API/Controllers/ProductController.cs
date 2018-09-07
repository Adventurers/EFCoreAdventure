using System;
using System.Linq;
using System.Threading.Tasks;
using Adventurers.Design.Repository;
using EFCoreAdventure.Models;
using Microsoft.AspNetCore.Mvc;
using EFCoreAdventure.UOW;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAdventure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IRepository<Product> ProductRepository { get; set; }


        public ProductController(IRepository<Product> productRepository)
        {
            ProductRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            //ProductRepository.Query.GetPageListAsync<(int Id, string Name)>(

            PageList<Tuple<int, string>> result = await ProductRepository.Query.GetPageListAsync(
                selector : p => new Tuple<int, string>(p.Id, p.Name),
                predicate: p => p.Name.Contains("Addidas"), 
                orderBy: o => o.OrderBy(p => p.Id), 
                include: s => s.Include(p => p.Brand), 
                pageSize: 20);

            return new JsonResult(result);
        }
    }
}