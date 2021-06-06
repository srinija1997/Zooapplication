using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using ZooApplication.Data_Abstraction;

namespace ZooApplication.Controllers
{
    public class Zoo : Controller
    {
        private readonly IPriceCalculation _priceCalculation = null;
        
        public Zoo(IPriceCalculation priceCalculation)
        {
           this._priceCalculation = priceCalculation;
            
        }
        public string Index()
        {
            double cost = _priceCalculation.CalculatePrice();
            return "The total cost of food is " + cost;

        }
    }
}
