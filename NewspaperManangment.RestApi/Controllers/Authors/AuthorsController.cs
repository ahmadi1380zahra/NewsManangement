﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;

namespace NewspaperManangment.RestApi.Controllers.Authors
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _service;
        public AuthorsController(AuthorService service)
        {
            _service=service;
        }
        [HttpPost]
        public async Task Add([FromBody]AddAuthorDto dto)
        {
           await _service.Add(dto);
        }
        [HttpPut("{id}")]
        public async Task Update([FromRoute]int id,[FromBody] UpdateAuthorDto dto)
        {
            await _service.Update(id,dto);
        }
    }
}
