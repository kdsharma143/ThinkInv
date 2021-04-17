using BusinessAccess.Interfaces;
using BusinessAccess.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridgeAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddProductRequestModel options)
        {
            var cat = await productService.Add(options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);
        }

        [HttpPut]
        [Route("Update/{productId}")]
        public async Task<IActionResult> Update(int productId, [FromBody] AddProductRequestModel options)
        {
            var cat = await productService.Update(productId, options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);

        }

        [HttpPut]
        [Route("Updatestatus/{productId}")]
        public async Task<IActionResult> UpdateStatus(int productId, [FromBody] UpdateStatusRequestModel options)
        {
            var cat = await productService.UpdateStatus(productId, options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);

        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductRequestModel options)
        {
            var cat = await productService.GetProducts(options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);

        }

        [HttpGet]
        [Route("GetProduct/{productId:int}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var cat = await productService.GetProduct(productId);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);

        }

        [HttpDelete]
        [Route("Delete/{productId:int}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var cat = await productService.Delete(productId);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);

        }
    }
}
