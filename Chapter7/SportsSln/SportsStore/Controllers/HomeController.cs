﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;
        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(string category ,int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = repository.Products
                         .Where(p => category == null || p.Category == category)
                         .OrderBy(p => p.ProductID)
                         .Skip((productPage - 1) * PageSize)
                         .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            });
        }

    }
}
