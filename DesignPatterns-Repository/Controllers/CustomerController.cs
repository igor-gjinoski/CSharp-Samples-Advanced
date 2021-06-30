using System.Data;
using System.Linq;
using DesignPatterns_Repository.Data;
using DesignPatterns_Repository.Repository.CustomerRepository;
using DesignPatterns_Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatterns_Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        public CustomerController(
            ICustomerRepository repository,
            IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        [Route("Index")]
        public IActionResult Index()
        {
            return Ok(_repository.Get().ToList());
        }


        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody]Customer Customer)
        {
            try
            {
                _repository.Insert(Customer);
                _unitOfWork.Complete();

                return Ok("Customer Created!");
            }
            catch (DataException)
            {
                return BadRequest("Unable to Create Customer object.");
            }
        }


        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                _unitOfWork.Complete();

                return Ok();
            }
            catch (DataException)
            {
                return BadRequest($"Unable to Delete Customer with id: {id}.");
            }
        }


        [HttpGet]
        [Route("GetById")]
        public IActionResult GetByID(int Id)
        {
            try
            {
                return Ok(_repository.GetByID(Id));
            }
            catch (DataException)
            {
                return BadRequest($"Unable to Find Customer with id: {Id}.");
            }
        }


        [HttpGet]
        [Route("FindAll")]
        public IActionResult FindAll(string name)
        {
            try
            {
                return Ok(_repository.FindByName(name));
            }
            catch (DataException)
            {
                return BadRequest($"Unable to Find Customers with name: {name}.");
            }
        }
    }
}
