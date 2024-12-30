using Microsoft.EntityFrameworkCore;
using ParkingService.Data;
using ParkingService.Models;
using ParkingService.Services;
using ParkingService.Repositories;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.Features;
using System.IO;
using System.Threading.Tasks;
using NetTopologySuite;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace ParkingService.Services
{
  public class ParkingSpotService : IParkingSpotService
{
    private readonly IParkingSpotRepository _repository;

    public ParkingSpotService(IParkingSpotRepository repository)
    {
        _repository = repository;
    }

     public async Task<string> GetParkingDataAsGeoJson(string shapefilePath)
    {
        
        var reader = new ShapefileReader(shapefilePath);
        var features = reader.ReadAll(); 
        
        var geoJsonWriter = new GeoJsonWriter();
        return geoJsonWriter.Write(features);
    }

   public string TransformToWGS84(string geoJson)
{
    var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory();
    var reader = new GeoJsonReader();
    var writer = new GeoJsonWriter();

    var sourceGeometry = reader.Read<GeometryCollection>(geoJson);

    var sourceCS = ProjectedCoordinateSystem.WGS84_UTM(33, true);
    var targetCS = GeographicCoordinateSystem.WGS84;

    var transform = new CoordinateTransformationFactory().CreateFromCoordinateSystems(sourceCS, targetCS);

    foreach (var geom in sourceGeometry.Geometries)
    {
        if (geom is Point point)
        {
            var newCoord = transform.MathTransform.Transform(new[] { point.X, point.Y });
            point.Coordinate.X = newCoord[0];
            point.Coordinate.Y = newCoord[1];
        }
        else if (geom is LineString line)
        {
            for (int i = 0; i < line.Coordinates.Length; i++)
            {
                var newCoord = transform.MathTransform.Transform(new[] { line.Coordinates[i].X, line.Coordinates[i].Y });
                line.Coordinates[i].X = newCoord[0];
                line.Coordinates[i].Y = newCoord[1];
            }
        }
        else if (geom is Polygon polygon)
        {
            var newShell = transform.MathTransform.Transform(new[] { polygon.Shell.Coordinates[0].X, polygon.Shell.Coordinates[0].Y });
            polygon.Shell.Coordinates[0].X = newShell[0];
            polygon.Shell.Coordinates[0].Y = newShell[1];

            foreach (var hole in polygon.Holes)
            {
                for (int i = 0; i < hole.Coordinates.Length; i++)
                {
                    var newCoord = transform.MathTransform.Transform(new[] { hole.Coordinates[i].X, hole.Coordinates[i].Y });
                    hole.Coordinates[i].X = newCoord[0];
                    hole.Coordinates[i].Y = newCoord[1];
                }
            }
        }
    }

    return writer.Write(sourceGeometry);
    }


    public async Task<IEnumerable<ParkingSpot>> GetAllParkingSpotsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ParkingSpot?> GetParkingSpotByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<ParkingSpot> AddParkingSpotAsync(ParkingSpot spot)
    {
        var existingSpot = (await _repository.GetAllAsync()).FirstOrDefault(s => s.id == spot.id);
        if (existingSpot != null) throw new Exception("Parking spot already exists.");

        return await _repository.AddAsync(spot);
    }

    public async Task<bool> DeleteParkingSpotAsync(int id)
    {
        var spot = await _repository.GetByIdAsync(id);
        if (spot == null) return false;

        if (!spot.is_available) throw new Exception("Cannot delete reserved parking spots.");

        return await _repository.DeleteAsync(id);
    }

    public async Task<ParkingSpot?> UpdateParkingSpotAsync(int id, ParkingSpot spot)
    {
        if (spot.id == null)  throw new Exception("Spot name cannot be empty.");

        return await _repository.UpdateAsync(spot);
    }

}

}