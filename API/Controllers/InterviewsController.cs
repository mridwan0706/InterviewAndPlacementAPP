using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Databases;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using API.ViewModels;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class InterviewsController : BaseController<Interview, InterviewRepository>
    {
        private MySQLDatabase _mysql;
        DynamicParameters param = new DynamicParameters();
        public InterviewsController(InterviewRepository interviewRepository, MySQLDatabase mySQL): base(interviewRepository){
            _mysql = mySQL;
        }

        [Route("{getall}")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _mysql.Connection.QueryAsync<InterviewVM>("Call SP_GetAllInterview").Result.ToList();
            return Ok(users);
        }

       
        [HttpGet("GetInterviewById/{id}")]
        public IActionResult GetById(int id)
        {
            var sql = "SP_GetInterviewById";
            param.Add("EmployeeId", id);
            var users = _mysql.Connection.QueryAsync<InterviewVM>(sql, param, commandType: System.Data.CommandType.StoredProcedure).Result.ToList();
            return Ok(users);
        }
        

         [HttpGet("GetInterviewByStatus/{Status}")]
        public IActionResult GetByStatus(string Status)
        {
            var sql = "SP_GetInterviewByStatus";
            param.Add("IStatus", Status);
            var users = _mysql.Connection.QueryAsync<InterviewVM>(sql, param, commandType: System.Data.CommandType.StoredProcedure).Result.ToList();
            return Ok(users);
        }

        [HttpPut("Status/{id}")]
        public async Task<ActionResult> UpdateStatus(int id, InterviewVM interviewVM)
        {
            var sql = "SP_Update_StatusInterview";
            param.Add("EId", id);
            param.Add("IStatus", interviewVM.Status);
            var affectedRow = await _mysql.Connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
            return Ok(affectedRow);
        }





    }
}