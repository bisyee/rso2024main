using Microsoft.AspNetCore.Mvc;
using ParkingService.Models;
using ParkingService.Services;
using System.Text.Json;
namespace ParkingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingSpotController : ControllerBase
    {
        private readonly IParkingSpotService _parkingService;

        public ParkingSpotController(IParkingSpotService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var spots = await _parkingService.GetAllParkingSpotsAsync();
            return Ok(spots);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var spot = await _parkingService.GetParkingSpotByIdAsync(id);
            if (spot == null) return NotFound();
            return Ok(spot);
        }
        [HttpGet("parking")]
        public async Task<IActionResult> GetParkingData()
        {
            var geoJsonData = await _parkingService.GetParkingDataAsGeoJson(@"C:\Users\User\Desktop\BIseraFRI\Magisterij\rso\rso-project2024\backend\ParkingService\Data\Dataset\MOL_Parkirisca_reprojected.shp");
            var jsonResult = JsonSerializer.Serialize(geoJsonData); 
            return new JsonResult(jsonResult);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ParkingSpot spot)
        {
            var newSpot = await _parkingService.AddParkingSpotAsync(spot);
            return CreatedAtAction(nameof(GetById), new { id = newSpot.id }, newSpot);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ParkingSpot spot)
        {
            var updatedSpot = await _parkingService.UpdateParkingSpotAsync(id, spot);
            if (updatedSpot == null) return NotFound();
            return Ok(updatedSpot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _parkingService.DeleteParkingSpotAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
