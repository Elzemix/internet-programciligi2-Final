﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaberPortali.Dtos;
using HaberPortali.Models;

namespace HaberPortali.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public CategoryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<CategoryDto> GetList()
        {
            var categories = _context.Categories.ToList();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public CategoryDto Get(int id)
        {
            var category = _context.Categories.Where(s => s.Id == id).SingleOrDefault();
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
        [HttpGet]
        [Route("{id}/News")]
        public List<NewsDto> GetNews(int id)
        {
            var news = _context.News.Where(s => s.CategoryId == id).ToList();
            var newsDtos = _mapper.Map<List<NewsDto>>(news);
            return newsDtos;
        }
        [HttpPost]
        public ResultDto Post(CategoryDto dto)
        {
            if (_context.Categories.Count(c => c.Name == dto.Name) > 0)
            {
                result.Status = false;
                result.Message = "The entered category name is registered!";
                return result;
            }
            var category = _mapper.Map<Category>(dto);
            category.Updated = DateTime.Now;
            category.Created = DateTime.Now;
            _context.Categories.Add(category);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Category Added";
            return result;
        }


        [HttpPut]
        public ResultDto Put(CategoryDto dto)
        {
            var category = _context.Categories.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (category == null)
            {
                result.Status = false;
                result.Message = "Category Not Found!";
                return result;
            }
            category.Name = dto.Name;
            category.IsActive = dto.IsActive;
            category.Updated = DateTime.Now;

            _context.Categories.Update(category);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Category Edited";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        public ResultDto Delete(int id)
        {
            var category = _context.Categories.Where(s => s.Id == id).SingleOrDefault();
            if (category == null)
            {
                result.Status = false;
                result.Message = "Category Not Found!";
                return result;
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Category Deleted";
            return result;
        }
    }
}
