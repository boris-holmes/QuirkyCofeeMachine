using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuirkyCoffeeMachine.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuirkyCoffeeMachine.Controllers
{
    [Route("brew-coffee")]
    [ApiController]
    public class BrewCoffeeController : ControllerBase
    {
        [HttpGet]
        //public BrewCoffeeViewModel Get()
        public IActionResult Get()
        {
            BrewCoffeeModel newBrew = ProcessGet();

            if (newBrew.StatusCode == 200)
                return Ok(new BrewCoffeeViewModel()
                {
                    Message = newBrew.Message,
                    Prepared = newBrew.CreatedDate.ToString("o") //or .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK") to be exact with the spec, but I prefer the inbuilt formatting for ISO-8601
                });
            else
                return StatusCode(newBrew.StatusCode);
        }

        private BrewCoffeeModel ProcessGet()
        {
            string message = "Your piping hot coffee is ready";         
            int statusCode = 200;           

            //Get persisted data 
            List<BrewCoffeeModel> brewCoffeeData = BrewCoffeeModel.GetBrewCoffeeData();

            //If 1st April, respond with joke
            if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
            {
                message = "";
                statusCode = 418;                
            }
            else
            {
                //Find how many brews have been poured after last unavailable brew. I am assuming here that someone refill the coffee bean everytime the machien rans out 
                if (brewCoffeeData.Count > 0)
                {
                    List<BrewCoffeeModel> brewCoffeeDataCopy = brewCoffeeData.ToList();
                    brewCoffeeDataCopy.Reverse();
                    brewCoffeeDataCopy = brewCoffeeDataCopy.TakeWhile(x => x.StatusCode != 503).ToList();

                    if (brewCoffeeDataCopy.Where(x => x.StatusCode == 200).ToList().Count >= 5)
                    {
                        message = "";
                        statusCode = 503;                        
                    }
                }
            }

            //Create result, save and return
            BrewCoffeeModel newBrew = new BrewCoffeeModel()
            {
                Message = message,
                StatusCode = statusCode
            };

            brewCoffeeData.Add(newBrew);

            BrewCoffeeModel.SaveBrewCoffeeData(brewCoffeeData);

            return newBrew;

        }
    }
}
