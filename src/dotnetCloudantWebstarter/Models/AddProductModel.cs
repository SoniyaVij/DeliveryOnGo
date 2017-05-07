using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudantDotNet.Models
{
    public class AddProductModel
    {
        public string DeleiveryAddress { get; set; }
        public string DeleiveryPinCode { get; set; }
        public string ParcelLocation { get; set; }
        public string ParcelLocationPinCode { get; set; }
        public string Weight { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DateofDelivery { get; set; }
        public decimal DeliveryCharge	{ get; set; }
	    public List<AddProductModel> AlreadyAddedParcels = new List<AddProductModel>();


    }
}
