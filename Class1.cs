using System;

public class Class1
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
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


}
