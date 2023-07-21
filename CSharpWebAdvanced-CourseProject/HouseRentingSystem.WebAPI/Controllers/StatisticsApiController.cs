namespace HouseRentingSystem.WebAPI.Controllers
{
    using HouseRentingSystem.Services.Data.Models.Statistics;
    using HouseRentingSystem.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IHouseService houseService;
        public StatisticsApiController(IHouseService _houseService)
        {
            this.houseService = _houseService;
        }

        [HttpGet]
        [Produces(contentType:"application/json")]
        [ProducesResponseType(200,Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatistics()
        {

            try
            {
                StatisticsServiceModel serviceModel =  await houseService.GetStatisticsAsync();

                return Ok(serviceModel);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
