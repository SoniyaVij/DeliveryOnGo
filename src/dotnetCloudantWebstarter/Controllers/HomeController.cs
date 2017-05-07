using CloudantDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudantDotNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return View();
            //return View("AddParcel");
            return View("Login");
        }

        [HttpPost]
        public ActionResult LoginUser(UserModel modelUser)
        {
            if (modelUser.UserType == "1")
            {
                
                AddProductModel productModel = new AddProductModel();
                AddProductModel product = new AddProductModel();
                
                product.DeleiveryAddress = "Marathahalli";
                product.DeleiveryPinCode = "560037";
                product.ParcelLocation = "Indiranagar";
                product.ParcelLocationPinCode = "560074";
                product.PickUpDate = DateTime.Now.Date;
                product.Weight="1 Kg";
                product.DateofDelivery = DateTime.Now.Date.AddDays(2);
                product.DeliveryCharge = 50;
                productModel.AlreadyAddedParcels.Add(product);

                AddProductModel product1 = new AddProductModel();

                product1.DeleiveryAddress = "Domlur";
                product1.DeleiveryPinCode = "560037";
                product1.ParcelLocation = "Indiranagar";
                product1.ParcelLocationPinCode = "560074";
                product1.PickUpDate = DateTime.Now.Date;
                product1.Weight = "1 Kg";
                product1.DeliveryCharge = 30;
                product1.DateofDelivery = DateTime.Now.Date.AddDays(2);
                productModel.AlreadyAddedParcels.Add(product1);






                return View("AddParcel", productModel);
            }
            else
            {
                return View("DeliverySearch");
            }
        }

        
        public ActionResult NewUserRegistration()
        {

            return View("RegisterUser");
        }
    }
}