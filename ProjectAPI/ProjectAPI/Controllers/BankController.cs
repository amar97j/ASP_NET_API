using Microsoft.AspNetCore.Authorization;
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
        public IActionResult GetAll(string filter = "", int pageNumber = 1, int pageSize = 10, string sortOrder = "asc")
        {
            IQueryable<BankBranch> bankBranches = _context.BankBranches;

            if (!string.IsNullOrEmpty(filter))
            {
                bankBranches = bankBranches.Where(b => b.LocationName.Contains(filter) || b.LocationURL.Contains(filter));
            }

            if (sortOrder.ToLower() == "desc")
            {
                bankBranches = bankBranches.OrderByDescending(b => b.LocationName);
            }
            else
            {
                bankBranches = bankBranches.OrderBy(b => b.LocationName);
            }

            var pagedBankBranches = bankBranches
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var result = pagedBankBranches.Select(b => new BankBranchResponce
            {
                BranchManager = b.BranchManager,
                LocationURL = b.LocationURL,
                LocationName = b.LocationName,
                EmployeeCount = b.EmployeeCount,
            }).ToList();

            int totalCount = bankBranches.Count();
            return Ok(new { result, totalCount });
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
        [Authorize]
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

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bank = _context.BankBranches.Find(id);
            _context.BankBranches.Remove(bank);
            _context.SaveChanges();

            return Ok();

        }

        [Authorize]
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
