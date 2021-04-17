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
    public class CategoryController : ControllerBase
    {
        private ICategoryService categoryService;
        public CategoryController(ICategoryService catService)
        {
            categoryService = catService;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddCategoryRequestModel options)
        {
            var cat = await categoryService.Add(options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return BadRequest(cat);
        }

        [HttpPut]
        [Route("Update/{catId}")]
        public async Task<IActionResult> Update(int catId, [FromBody] AddCategoryRequestModel options)
        {
            var cat = await categoryService.Update(catId,options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);

        }

        [HttpPut]
        [Route("Updatestatus/{catId}")]
        public async Task<IActionResult> UpdateStatus(int catId,[FromBody] UpdateStatusRequestModel options)
        {
            var cat = await categoryService.UpdateStatus(catId,options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);

        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories([FromQuery]GetCategoriesRequestModel options)
        {
            var cat = await categoryService.GetCategories(options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);

        }

        [HttpGet]
        [Route("GetCategory/{catId:int}")]
        public async Task<IActionResult> GetCategory(int catId)
        {
            var cat = await categoryService.GetCategory(catId);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);

        }

        [HttpDelete]
        [Route("Delete/{catId:int}")]
        public async Task<IActionResult> Delete(int catId)
        {
            var cat = await categoryService.Delete(catId);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);

        }
    }
}
