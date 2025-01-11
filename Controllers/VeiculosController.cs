using Consumo_veiculos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consumo_veiculos.Controllers
{
    [Authorize]
    public class VeiculosController : Controller
    {
        private readonly AppDbContext _context;
        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        //Padrão de uam requisição http (async etc)
        public async Task<IActionResult> Index()
        {
            var dados = await _context.Veiculos.ToListAsync();

            return View(dados);
        }
        //[HttpGet] é o padrão se não escrevo nada
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Veiculos.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }
        
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            
            var dados = await _context.Veiculos.FindAsync(Id);
            if (dados == null)
            {
                return BadRequest();
            }
            return View(dados);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? Id, Veiculo veiculo)
        {
            if (Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Veiculos.Update(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            } else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // ou logue esses erros para análise
                }
            }
            return View();
        }
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var dados = await _context.Veiculos.FindAsync(Id);
            if (dados == null)
            {
                return BadRequest();
            }
            return View(dados);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var dados = await _context.Veiculos.FindAsync(Id);
            if (dados == null)
            {
                return BadRequest();
            }
            return View(dados);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var dados = await _context.Veiculos.FindAsync(Id);
            if (dados == null)
            {
                return NotFound();
            }

            _context.Veiculos.Remove(dados);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index"); ;
        }
        public async Task<IActionResult> Relatorio(int? id)
        {
            if (id == null)
                return NotFound();

            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
                return NotFound();

            var consumos = await _context.Consumos
                .Where(c => c.VeiculoId == id)
                .OrderByDescending(c => c.Data)
                .ToListAsync();

            decimal total = consumos.Sum(c => c.Valor);
            ViewBag.Veiculo = veiculo;
            ViewBag.Total = total;
                 
            return View(consumos);
        }
    }
}
