using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oop_laba3.Data;
using oop_laba3.Models;

namespace oop_laba3.Controllers
{
    public class ParkingCarsController : Controller
    {
        private readonly AppDbContext _context;

        public ParkingCarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ParkingCars
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ParkingCars.Include(p => p.Car).Include(p => p.Parking);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ParkingCars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingCar = await _context.ParkingCars
                .Include(p => p.Car)
                .Include(p => p.Parking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingCar == null)
            {
                return NotFound();
            }

            return View(parkingCar);
        }

        // GET: ParkingCars/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            ViewData["ParkingId"] = new SelectList(_context.Parkings, "Id", "Id");
            return View();
        }

        // POST: ParkingCars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,ParkingId")] ParkingCar parkingCar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", parkingCar.CarId);
            ViewData["ParkingId"] = new SelectList(_context.Parkings, "Id", "Id", parkingCar.ParkingId);
            return View(parkingCar);
        }

        // GET: ParkingCars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingCar = await _context.ParkingCars.FindAsync(id);
            if (parkingCar == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", parkingCar.CarId);
            ViewData["ParkingId"] = new SelectList(_context.Parkings, "Id", "Id", parkingCar.ParkingId);
            return View(parkingCar);
        }

        // POST: ParkingCars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,ParkingId")] ParkingCar parkingCar)
        {
            if (id != parkingCar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingCarExists(parkingCar.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", parkingCar.CarId);
            ViewData["ParkingId"] = new SelectList(_context.Parkings, "Id", "Id", parkingCar.ParkingId);
            return View(parkingCar);
        }

        // GET: ParkingCars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingCar = await _context.ParkingCars
                .Include(p => p.Car)
                .Include(p => p.Parking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingCar == null)
            {
                return NotFound();
            }

            return View(parkingCar);
        }

        // POST: ParkingCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkingCar = await _context.ParkingCars.FindAsync(id);
            if (parkingCar != null)
            {
                _context.ParkingCars.Remove(parkingCar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingCarExists(int id)
        {
            return _context.ParkingCars.Any(e => e.Id == id);
        }
    }
}
