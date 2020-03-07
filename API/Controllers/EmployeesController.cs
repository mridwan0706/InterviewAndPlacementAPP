using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Databases;
using API.Models;
using API.Repositories;
using API.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository>
    {
        private MySQLDatabase _mysql;
        DynamicParameters param = new DynamicParameters();
        public EmployeesController(EmployeeRepository employeeRepository, MySQLDatabase mySQL) : base(employeeRepository) {
           _mysql = mySQL;
        }

        [Route("GetById/{ParticipantId}")]
        [HttpGet("GetById/{ParticipantId}")]
        public IActionResult GetById(string ParticipantId)
        {
            var sql = "SP_GetByParticipantId";
            param.Add("Participant_Id", ParticipantId);
            var interviews = _mysql.Connection.QueryAsync<EmployeeVM>(sql, param, commandType: System.Data.CommandType.StoredProcedure).Result.ToList();
            return Ok(interviews);
        }
    }
}