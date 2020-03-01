﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository>
    {
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository) { }
    }
}