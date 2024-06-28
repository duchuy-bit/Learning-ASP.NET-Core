﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManager.Models;
using ShopManager.ViewModels;
using System.Diagnostics;

namespace ShopManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region TinhTong
        public IActionResult Index()
        {
            SoNguyen soNguyen = new SoNguyen();

            return View(soNguyen);
        }

        [HttpPost]
        public IActionResult TinhTong(SoNguyen soNguyen)
        {
            int tong = soNguyen.soNguyenA + soNguyen.soNguyenB;

            return Content(tong.ToString());
        }
        #endregion


        #region SelectInput
        public IActionResult SelectInput()
        {
            List<Country> countries = new List<Country>()
            {
                new Country()
                {
                    Id =1,
                    Name="Việt Nam",
                },
                new Country()
                {
                    Id= 2,
                    Name = "Hàn Quốc"
                },
                new Country()
                {
                    Id= 3,
                    Name = "Nga"
                }
            };

            var model = new CountryViewModel() ;
            model.myListCountry = new List<SelectListItem>();

            foreach (var country in countries)
            {
                model.myListCountry.Add(
                    new SelectListItem
                    {
                        Text = country.Name,
                        Value = country.Id.ToString(),
                    }
                );
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult SelectInput(CountryViewModel countryViewModel)
        {
            var selected = countryViewModel.SelectedCountry;

            return Content(selected);
        }

        #endregion


        #region CheckBoxInput_SingleCheckBox

        public IActionResult CheckBoxInput()
        {
            var model = new AcceptTermsViewModel()
            {
                AcceptTerms = true,
                Title = "I Accept The Terms"
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CheckBoxInput(AcceptTermsViewModel modelInput)
        {
            var value = modelInput.AcceptTerms == true ? "True" : "False";
            return Content(value);
        }



        #endregion


        #region CheckBoxInput_ListCheckBox

        public IActionResult ListCheckBoxInput()
        {
            var model = new CheckBoxesViewModel()
            {
                CheckBoxes=new List<CheckBoxOption> { 
                    new CheckBoxOption 
                    {
                        IsChecked = true,
                        Text = "Cricket",
                        Value= "Cricket_Value"
                    } ,
                    new CheckBoxOption
                    {
                        IsChecked = false,
                        Text = "FootBall",
                        Value= "FootBall_Value"
                    },
                    new CheckBoxOption
                    {
                        IsChecked = true,
                        Text = "Hockey",
                        Value= "Hockey_Value"
                    }
                }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ListCheckBoxInput(CheckBoxesViewModel checkBoxes)
        {
            string value ="";

            foreach (string item in checkBoxes.sports) {
                value = value + item + "     ";
            }

            return Content(value);
        }



        #endregion



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}