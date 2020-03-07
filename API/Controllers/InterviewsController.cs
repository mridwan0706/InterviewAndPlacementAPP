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
        public InterviewsController(InterviewRepository interviewRepository, MySQLDatabase mySQL) : base(interviewRepository) {
            _mysql = mySQL;
        }

        [Route("getall")]
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var users = _mysql.Connection.QueryAsync<InterviewVM>("Call SP_GetAllInterview").Result.ToList();
            return Ok(users);
        }
        //with Join
        [Route("GetAllByInterviewId/{Id}")]
        [HttpGet("GetAllByInterviewId/{Id}")]
        public IActionResult GetAllByInterviewId(int Id)
        {
            var sql = "SP_GetAllByInterviewId";
            param.Add("InterviewId", Id);
            var interviews = _mysql.Connection.QueryAsync<InterviewVM>(sql, param, commandType: System.Data.CommandType.StoredProcedure).Result.ToList();
            return Ok(interviews);
        }

        [HttpGet("GetByUserId/{ParticipantId}")]
        public IActionResult GetById(string ParticipantId)        {
            var sql = "SP_GetAllByUserId";
            param.Add("Eid", ParticipantId);           
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

        [Route("UpdateStatus/{Id}")]
        [HttpPut("UpdateStatus/{Id}")]
        public async Task<ActionResult> UpdateStatus(int Id, InterviewVM interviewVM)
        {
            var sql = "SP_Update_StatusInterview";
            param.Add("InterviewId", Id);
            param.Add("IStatus", interviewVM.Status);
            var affectedRow = await _mysql.Connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
            return Ok(affectedRow);
        }





    }
}