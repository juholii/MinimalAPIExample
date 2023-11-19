using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MinimalAPIExample", Version = "v1" });
});

builder.Services.AddDbContext<CarContext>(options =>
    options.UseSqlite("Data Source=cars.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MinimalAPIExample v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}

public class ElectricCar : Car
{
    public int BatteryCapacity { get; set; }
}

public class CarContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<ElectricCar> ElectricCars { get; set; }

    public CarContext(DbContextOptions<CarContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ElectricCar>().HasData(
            new
            {
                Id = 1,
                Brand = "BMW",
                Model = "i4",
                Year = 2022,
                BatteryCapacity = 81
            },
            new
            {
                Id = 2,
                Brand = "Audi",
                Model = "e-tron",
                Year = 2020,
                BatteryCapacity = 71
            },
            new
            {
                Id = 3,
                Brand = "Mercedes-Benz",
                Model = "EQE",
                Year = 2022,
                BatteryCapacity = 100
            }
        );
    }
}

[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly CarContext _context;

    public CarsController(CarContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ElectricCar car)
    {
        _context.ElectricCars.Add(car);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
    }

    private object GetById()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IEnumerable<ElectricCar>> Get()
    {
        return await _context.ElectricCars.ToListAsync();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ElectricCar car)
    {
        if (id != car.Id)
        {
            return BadRequest();
        }

        _context.Entry(car).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var car = await _context.ElectricCars.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }

        _context.ElectricCars.Remove(car);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}



