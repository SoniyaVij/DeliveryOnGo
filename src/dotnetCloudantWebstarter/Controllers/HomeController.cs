using CloudantDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult AddProduct(AddProductModel model)
        {
            AddProductModel productModel = FilleProductDetails(model);
            return View("AddParcel", productModel);
        }

        [HttpPost]
        public ActionResult LoginUser(UserModel modelUser)
        {
            AddProductModel productModel = FilleProductDetails();
            if (modelUser.UserType == "1")
            {

                return View("AddParcel", productModel);
            }
            else
            {
                return View("DeliverySearch", productModel);
            }
        }

        [HttpPost]
        public ActionResult RegisterUser(UserModel user)
        {
            return View("Login");
        }

        private AddProductModel FilleProductDetails(AddProductModel input = null)
        {
            AddProductModel productModel = new AddProductModel();
            AddProductModel product = new AddProductModel();

            product.DeleiveryAddress = "Marathahalli";
            product.DeleiveryPinCode = "560037";
            product.ParcelLocation = "Indiranagar";
            product.ParcelLocationPinCode = "560074";
            product.PickUpDate = DateTime.Now.Date;
            product.Weight = "1 Kg";
            product.DateofDelivery = DateTime.Now.Date.AddDays(2);
            product.DeliveryCharge = 50;
            product.Status = "Open";
            productModel.AlreadyAddedParcels.Add(product);

            AddProductModel product1 = new AddProductModel();

            product1.DeleiveryAddress = "Domlur";
            product1.DeleiveryPinCode = "560073";
            product1.ParcelLocation = "Indiranagar";
            product1.ParcelLocationPinCode = "560074";
            product1.PickUpDate = DateTime.Now.Date;
            product1.Weight = "1 Kg";
            product1.DeliveryCharge = 30;
            product1.DateofDelivery = DateTime.Now.Date.AddDays(2);
            product1.Status = "Open";
            productModel.AlreadyAddedParcels.Add(product1);

            if (input != null)
            {
                productModel.AlreadyAddedParcels.Add(input);
            }


            return productModel;

        }

        public ActionResult NewUserRegistration()
        {

            return View("RegisterUser");
        }

        public ActionResult Search(AddProductModel model)
        {

            AddProductModel productModel = FilleProductDetails();

            List<AddProductModel> searchRes = new List<AddProductModel>();

            if (model.SearchField != null && model.SearchField != "")
            {
                searchRes = productModel.AlreadyAddedParcels.Select(c => c).Where(c => c.DeleiveryPinCode.StartsWith(model.SearchField)).ToList();
                productModel.AlreadyAddedParcels.Clear();
            }
            
            foreach (var item in searchRes)
            {
                productModel.AlreadyAddedParcels.Add(item);
            }


            return View("DeliverySearch", productModel);

        }
    }
}