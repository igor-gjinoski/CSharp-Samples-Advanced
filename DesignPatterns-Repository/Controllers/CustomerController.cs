using System.Data;
using System.Linq;
using DesignPatterns_Repository.Data;
using DesignPatterns_Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatterns_Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        [Route("Index")]
        public IActionResult Index()
        {
            return Ok(_unitOfWork.Repository.Get().ToList());
        }


        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody]Customer Customer)
        {
            try
            {
                _unitOfWork.Repository.Insert(Customer);
                _unitOfWork.Commit();
                return Ok("Customer Created!");
            }
            catch (DataException)
            {
                return BadRequest("Unable to Create Customer object.");
            }
        }


        [HttpGet]
        [Route("GetById")]
        public IActionResult GetByID(int Id)
        {
            try
            {
                return Ok(_unitOfWork.Repository.GetByID(Id));
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
                return Ok(_unitOfWork.Repository.FindByName(name));
            }
            catch (DataException)
            {
                return BadRequest($"Unable to Find Customers with name: {name}.");
            }
        }


        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                _unitOfWork.Repository.Delete(id);
                return Ok();
            }
            catch (DataException)
            {
                return BadRequest($"Unable to Delete Customer with id: {id}.");
            }
        }


        [HttpPost]
        [Route("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.Repository.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}
