using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Stratsys.SkiBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkiLengthController : ControllerBase
    {
        /// <summary>
        /// Recommends length for ski
        /// </summary>
        /// <param name="age">User age in years</param>
        /// <param name="length">User length in centimeters</param>
        /// <param name="isFreestyle">Freestyle type, default is Classic</param>
        /// <returns>Recommendation interval</returns>
        [HttpGet(Name = "GetSkiLength")]
        public SkiRecommendation Get(int age, int length, bool isFreestyle)
        {
            CheckInputParams(age, length);
            if(age <= 8) 
            {
                //Assumption: Always recommend Classic. Requirements unclear. 
                return RecommendSkiForChild(age, length);
            }
            else
            {
                return RecommendSki(length, isFreestyle);
            }
        }

        private void CheckInputParams(int age, int length)
        {
            if (age < 0) throw new Exception("Age cannot be less than zero");
            if (length < 0) throw new Exception("Length cannot be less than zero");
        }

        private SkiRecommendation RecommendSki(int length, bool isFreestyle)
        {
            if (isFreestyle)
            {
                return new SkiRecommendation
                {
                    MinLength = Math.Min(length + 10, Constants.MaxManufacturedFreestyleSkiLength),
                    MaxLength = Math.Min(length + 15, Constants.MaxManufacturedFreestyleSkiLength),
                };
            }
            return new SkiRecommendation
            {
                MinLength = Math.Min(length + 20, Constants.MaxManufacturedClassicSkiLength),
                MaxLength = Math.Min(length + 20, Constants.MaxManufacturedClassicSkiLength),
            };
        }

        private SkiRecommendation RecommendSkiForChild(int age, int length)
        {
            if(age <= 4) 
            {
                return new SkiRecommendation
                {
                    MinLength = Math.Min(length, Constants.MaxManufacturedClassicSkiLength),
                    MaxLength = Math.Min(length, Constants.MaxManufacturedClassicSkiLength),
                };
            }
            return new SkiRecommendation
            {
                MinLength = Math.Min(length + 10, Constants.MaxManufacturedClassicSkiLength),
                MaxLength = Math.Min(length + 20, Constants.MaxManufacturedClassicSkiLength),
            };
        }
    }
}
