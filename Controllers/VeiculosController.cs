using Consumo_veiculos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Consumo_veiculos.ViewModels;
using AutoMapper;
using Consumo_veiculos.Repositories;

namespace Consumo_veiculos.Controllers
{
    [Authorize]
    public class VeiculosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public VeiculosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Padrão de uam requisição http (async etc)
        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.Veiculos.ToListAsync();
            var model = _mapper.Map<List<VeiculoViewModel>>(vehicles);
            return View(model);
        }
        //[HttpGet] é o padrão se não escrevo nada
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(VeiculoViewModel veiculo)
        {
            if (ModelState.IsValid)
            {
                var veiculoToCreate = new Veiculo();

                _mapper.Map(veiculo, veiculoToCreate);
                _context.Veiculos.Add(veiculoToCreate);
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
        public async Task<IActionResult> Edit(int? Id, VeiculoViewModel veiculo)
        {
            if (Id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var veiculoToUpdate = await _context.Veiculos.FindAsync(Id);
                _mapper.Map(veiculo, veiculoToUpdate);

                _context.Veiculos.Update(veiculoToUpdate);
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
