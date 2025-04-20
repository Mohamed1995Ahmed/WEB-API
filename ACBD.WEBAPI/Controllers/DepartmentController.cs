using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Data;
using Models.Models;
using Models.Mpdels;
using Models.ViewModel;

namespace ACBD.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        AppDBcontext context;
        public DepartmentController(AppDBcontext appDBcontext)
        {
            context = appDBcontext; 
        }
        [HttpGet]
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var depts = context.Departments
                .Include(d => d.Employees)
                .ToList();

            var deptDtos = depts.Select(dept => new DeptDto
            {
                Id = dept.Id,
                Name = dept.Name,
                Names = dept.Employees.Select(e => e.Name).ToList()
            }).ToList();

            return Ok(deptDtos);// there is two soluation in rhe end controller 
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dept = context.Departments
                              .Include(d => d.Employees)
                              .FirstOrDefault(d => d.Id == id);

            if (dept == null)
            {
                return NotFound(new ApiResponse
                {
                    IsSuccess = false,
                    Data = "Invalid department ID",
                    Message = "Department not found"
                });
            }

            var result = new
            {
                Id = dept.Id,
                Name = dept.Name,
                Names = dept.Employees.Select(e => e.Name).ToList()
            };

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Data = result,
                Message = "Department retrieved successfully"
            });
        }
        [HttpPost]
        public IActionResult Add(Department dept)
        {
            context.Departments.Add(dept);
            context.SaveChanges();
            return CreatedAtAction("GetById", new {id=dept.Id} ,dept);
        }
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var emp = context.Departments.FirstOrDefault(x => x.Name == name);
            context.SaveChanges();
            return Ok(emp);
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id,EditDto dept)
        {
            Department d =context.Departments.FirstOrDefault(x => x.Id == id);
           if(d != null)
            {
                d.Name = dept.name;
                
                context.Update(d);
                context.SaveChanges();
                return NoContent();
            }
            return NotFound();

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           if(ModelState.IsValid)
            {
                var d = context.Departments.FirstOrDefault(b => b.Id == id);
                if (d != null)
                {
                    context.Departments.Remove(d);
                    context.SaveChanges();
                    return NoContent();
                }
            }
            return NotFound();


        }

    }

}



//[HttpGet]
//public IActionResult GetAll()
//{
//    var emps = context.Departments.Include(x => x.Employees).ToList();

//    var allEmployees = new List<DeptDto>();

//    foreach (var dept in emps)
//    {
//        var d = new DeptDto
//        {
//            Id = dept.Id,
//            Name = dept.Name,
//            Names = new List<string>() // ✅ Initialize the list
//        };

//        foreach (var item in dept.Employees)
//        {
//            d.Names.Add(item.Name); // ✅ Safely add to the list
//        }

//        allEmployees.Add(d);
//    }

//    return Ok(allEmployees);
//}
//public IActionResult GetAll()
//{
//    var emps = context.Departments.Include(x => x.Employees).ToList();

//    var allEmployees = new List<DeptDto>();

//    foreach (var dept in emps)
//    {
//        var d = new DeptDto();
//        d.Id = dept.Id;
//        d.Name = dept.Name;
//        d.Names = new List<string>();
//        foreach (var item in dept.Employees)
//        {

//            d.Names.Add(item.Name);


//        }
//        allEmployees.Add(d);
//    }

//    return Ok(allEmployees);
//}

