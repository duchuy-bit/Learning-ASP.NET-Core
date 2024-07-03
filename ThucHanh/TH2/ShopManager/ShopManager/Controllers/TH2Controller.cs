using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManager.Models;
using ShopManager.ViewModels;
using static NuGet.Packaging.PackagingConstants;

namespace ShopManager.Controllers
{
    public class TH2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region TinhTong

        public IActionResult TinhTong()
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

        #region AcceptTerm
        public IActionResult AcceptTerm()
        {
            AcceptTerm acceptTerm = new AcceptTerm();
            acceptTerm.IsAccept = false;

            return View(acceptTerm);
        }
        [HttpPost]
        public IActionResult AcceptTerm(AcceptTerm modelInput)
        {
            var result = modelInput.IsAccept == true ? "Accept Term" :
                "Don't Accept Term";

            return Content(result);
        }
        #endregion

        #region CheckBoxInput_ListCheckBox

        public IActionResult ListCheckBox()
        {
            var model = new CheckBoxSport()
            {
                listSport = new List<Sport> {
                    new Sport
                    {
                        IsChecked = true,
                        Text = "Volleyball",
                        Value= "Volleyball_Value"
                    } ,
                    new Sport
                    {
                        IsChecked = false,
                        Text = "FootBall",
                        Value= "FootBall_Value"
                    },
                    new Sport
                    {
                        IsChecked = true,
                        Text = "Basketball",
                        Value= "Basketball_Value"
                    }
                }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ListCheckBox(CheckBoxSport checkBoxes)
        {
            string value = "";

            if (checkBoxes.sportChecked != null)
            {
                foreach (string item in checkBoxes.sportChecked)
                {
                    value = value + item + "     ";
                }

                return Content(value);
            }
            else
            {
                return Content("Null");
            }
            
        }

        #endregion

        #region Single_RadioButon
        public IActionResult SingleRadio()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult SingleRadio(User user)
        {
            return Content("Gender: "+ user.Gender);
        }

        #endregion


        #region ListRadioButton

        public IActionResult ListRadioButton()
        {
            CountryRadio objCountry = new CountryRadio();

            List<CountryModel> listCountry = new List<CountryModel>();
            listCountry.Add(new CountryModel() { Id = 1, Name = "Việt Nam", IsSelected = false });
            listCountry.Add(new CountryModel() { Id = 2, Name = "Nga", IsSelected = false });
            listCountry.Add(new CountryModel() { Id = 3, Name = "Thai Lan", IsSelected = true });
            listCountry.Add(new CountryModel() { Id = 4, Name = "Nhat Ban", IsSelected = false });
            listCountry.Add(new CountryModel() { Id = 5, Name = "Trung Quóc", IsSelected = false });
            listCountry.Add(new CountryModel() { Id = 6, Name = "Sigapore", IsSelected = false });

            objCountry.ListCountry = listCountry;

            return View(objCountry);
        }

        [HttpPost]
        public IActionResult ListRadioButton(CountryRadio countryRadio)
        {
            return Content(countryRadio.CountryName);
        }

        #endregion


        #region SelectInput
        public IActionResult SelectInput()
        {
            List<Phone> phones = new List<Phone>()
            {
                new Phone()
                {
                    Id =1,
                    Name="IPhone 16 Pro Max",
                },
                new Phone()
                {
                    Id= 2,
                    Name = "Xiaomi"
                },
                new Phone()
                {
                    Id= 3,
                    Name = "SamSung"
                },
                new Phone()
                {
                    Id= 4,
                    Name = "Huawei"
                }
            };

            var model = new PhoneSelect();
            model.listPhone = new List<SelectListItem>();

            foreach (var item in phones)
            {
                model.listPhone.Add(
                    new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Name
                    }
                );
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult SelectInput(PhoneSelect phoneSelect)
        {
            var selected = phoneSelect.phoneSelected;

            return Content(selected);
        }

        #endregion

        #region Upload_Image

        public IActionResult UpLoadImage(ImageUpload? imgUpload)
        {
            Console.WriteLine("---------PATH: " + imgUpload?.Path);

            if (imgUpload?.Path != null)
            {
                
                return View(imgUpload);
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpLoadImage(IFormFile Hinh)
        {
            string folder = "HinhUpLoad";
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    "Hinh", folder, Hinh.FileName);
                using (var myFile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myFile);
                }
                ImageUpload imageUpload = new ImageUpload();
                imageUpload.Path = Hinh.FileName;
                return View(imageUpload);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Upload Error "+ex.Message);
                return View();
            }
        }


        #endregion


    }
}
