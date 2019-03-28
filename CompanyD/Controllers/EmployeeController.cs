using CompanyD.Models.Requests;
using CompanyD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompanyD.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        [HttpGet, Route("echo")]
        public IHttpActionResult EchoTest()
        {
            return Ok("echo success");
        }

        EmployeeService svc = new EmployeeService();

        [HttpGet, Route()]
        public IHttpActionResult GetAll()
        {
            var status = svc.AllEmployees();
            if (status == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(status); 
            }
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult GetId(int id)
        {
            var status = svc.EmployeeById(id);
            if (status == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(status);
            }
        }

        [HttpPost, Route()]
        public IHttpActionResult Add(EmployeeAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return Ok("Employee " + svc.AddEmployee(model) + " has been added");
            }
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(EmployeeEditRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return Ok("Employee " + svc.EditEmployee(model) + " has been updated");
            }
        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Remove(int id)
        {
            var status = svc.RemoveEmployee(id);
            if (status == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok("Employee " + status + " has been removed");
            }
        }
    }
}
