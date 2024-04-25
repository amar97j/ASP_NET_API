using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;
using WebApplication2.Models;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly BankContext _context;

        public BankController(BankContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<BankBranchResponce> GetAll()
        {
            return _context.BankBranches.Select(b => new BankBranchResponce
            {
                BranchManager = b.BranchManager,
                LocationURL = b.LocationURL,
                EmployeeCount = b.EmployeeCount,
                LocationName = b.LocationName,
            }).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<BankBranchResponce> Details(int id)
        {
            var bank = _context.BankBranches.Find(id);
            if (bank == null)
            {
                return NotFound();
            }
            return new BankBranchResponce
            {
                BranchManager = bank.BranchManager,
                LocationURL = bank.LocationURL,
                EmployeeCount = bank.EmployeeCount,
                LocationName = bank.LocationName,

            };

        }

        [HttpPatch("{id}")]
        public IActionResult Edit(int id, AddBankRequest req)
        {
            var bank = _context.BankBranches.Find(id);

            bank.LocationName = req.LocationName;
            bank.LocationURL = req.LocationURL;
            bank.BranchManager = req.BranchManager;
            bank.EmployeeCount = req.EmployeeCount;
            
           
            _context.SaveChanges();

            return Created(nameof(Details), new { Id = bank.Id });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bank = _context.BankBranches.Find(id);
            _context.BankBranches.Remove(bank);
            _context.SaveChanges();

            return Ok();

        }

        [HttpPost]
        public IActionResult Add(AddBankRequest req) 
        {
            var newBank = new BankBranch()
            {
                LocationName = req.LocationName,
                LocationURL = req.LocationURL,
                BranchManager = req.BranchManager,
                EmployeeCount = req.EmployeeCount,  
           };
            _context.BankBranches.Add(newBank);
            _context.SaveChanges();

            return Created(nameof(Details),new {Id = newBank.Id});

                }
    }
}
