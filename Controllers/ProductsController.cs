using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestfulAPI.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestfulAPI.Controllers
{
    //[Route("[controller]")]
    [Route("products")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        IProductsRepo ProductsRepo { get; }
        IOptionsSnapshot<Settings.Products> Settings { get; }
        IMapper Mapper { get; }
        ILogger Logger { get; }

        public ProductsController(IProductsRepo productsRepo, IOptionsSnapshot<Settings.Products> options, IMapper mapper, ILogger<ProductsController> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            ProductsRepo = productsRepo ?? throw new ArgumentNullException(nameof(productsRepo));
            Settings = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <response code="200">List of all products</response>
        [HttpGet]
        public IEnumerable<Dto.Product> Get()
        {
            var prod1 = new Dto.Product
            {
                Id = 1001,
                Name = "Josephine Darakjy",
                Activity = 37,
                Country = new Dto.Country { Name = "Egypt", Code = "eg" },
                Company = "Chanay, Jeffrey A Esq",
                Date = DateTime.Now,
                Status = "new",
                Representative = new Dto.Representative { Name = "Amy Elsner", Image = "amyelsner.png" }
            };
            var prod2 = new Dto.Product
            {
                Id = 1003,
                Name = "Lenna Paprocki",
                Activity = 37,
                Country = new Dto.Country { Name = "Slovenia", Code = "si" },
                Company = "Feltz Printing Service",
                Date = DateTime.Now,
                Status = "new",
                Representative = new Dto.Representative { Name = "Xuxue Feng", Image = "xuxuefeng.png" }
            };
            var prod3 = new Dto.Product
            {
                Id = 1003,
                Name = "Lenna Paprocki",
                Activity = 37,
                Country = new Dto.Country { Name = "Slovenia", Code = "si" },
                Company = "Feltz Printing Service",
                Date = DateTime.Now,
                Status = "new",
                Representative = new Dto.Representative { Name = "Xuxue Feng", Image = "xuxuefeng.png" }
            };

            var products = new List<Dto.Product>();
            products.Add(prod1);
            products.Add(prod2);
            products.Add(prod3);
            return products;
            //return ProductsRepo.Get().Select(Mapper.Map<Dto.Product>);
        }
        
        


        /// <summary>
        /// Get a product by id
        /// </summary>
        /// <param name="id">A product id</param>
        [ProducesResponseType(typeof(Dto.Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id}")]
        public Dto.Product GetById(int id)
        {
            return Mapper.Map<Dto.Product>(ProductsRepo.GetById(id));
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="id">A new product id</param>
        /// <param name="newProductDto">New product data</param>
        /// <response code="201">The created product</response>
        [ProducesResponseType(typeof(Dto.Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("{id}")]
        public IActionResult Create(int id, [FromBody] Dto.UpdateProduct newProductDto)
        {
            var newProduct = new Model.Product(id);
            Mapper.Map(newProductDto, newProduct);
            ProductsRepo.Create(newProduct);

            var createdProduct = ProductsRepo.GetById(id);

            Logger.LogInformation("New product was created: {@product}", createdProduct);

            return Created($"{id}", Mapper.Map<Dto.Product>(createdProduct));
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id">Id of the product to update</param>
        /// <param name="productDto">Product data</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] Dto.UpdateProduct productDto)
        {
            var product = ProductsRepo.GetById(id);
            Mapper.Map(productDto, product);
            ProductsRepo.Update(product);
            return Ok();
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">Id of the product to delete</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            ProductsRepo.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Example of an exception handling
        /// </summary>
        [HttpGet("ThrowAnException")]
        public IActionResult ThrowAnException()
        {
            throw new Exception("Example exception");
        }

        /// <summary>
        /// Demonstrate how to use application settings
        /// </summary>
        /// <returns>Application settings</returns>
        /// <remarks>Don't do this in production! You can unintentionally unclose sensitive information</remarks>
        [HttpGet("Settings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Settings.Products GetSettings()
        {
            return Settings.Value;
        }
    }
}
