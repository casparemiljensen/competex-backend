﻿using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericsController<TDto> : ControllerBase, IGenericAPI<TDto> where TDto : class
    {
        private IGenericService<TDto> _genericService;

        public GenericsController(IGenericService<TDto> service)
        {
            _genericService = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _genericService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int? pageSize, int? pageNumber)
        {
            var result = await _genericService.GetAllAsync(pageSize, pageNumber);
            if (result.IsSuccess)
            {
                var obj = new PaginationWrapperDTO<IEnumerable<TDto>>(
                    result.Value.Item2,
                    pageSize ?? Defaults.PageSize,
                    pageNumber ?? Defaults.PageNumber,
                    result.Value.Item1);
                return Ok(obj);
            }
            return BadRequest(result.Error);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchAllAsync(int? pageSize, int? pageNumber, [FromBody] Dictionary<string, object>? filters)
        {
            var result = await _genericService.SearchAllAsync(pageSize, pageNumber, filters);
            if (result.IsSuccess)
            {
                //var obj = result.Value.Item2;
                var obj = new PaginationWrapperDTO<IEnumerable<TDto>>(
                    result.Value.Item2,
                    pageSize ?? Defaults.PageSize,
                    pageNumber ?? Defaults.PageNumber,
                    result.Value.Item1);
                return Ok(obj);
            }
            return BadRequest(result.Error);
        }

        [HttpPost]
        public async virtual Task<IActionResult> CreateAsync(TDto obj)
        {
            var result = await _genericService.CreateAsync(obj);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, TDto obj)
        {
            var result = await _genericService.UpdateAsync(id, obj);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _genericService.RemoveAsync(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.Error);
        }
    }
}
