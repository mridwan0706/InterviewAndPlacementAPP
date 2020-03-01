using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Databases;
using API.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private readonly AppDB _con;
        //DynamicParameters param = new DynamicParameters();
        //public UsersController(AppDB con)
        //{
        //    _con = con;
           
        //}
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var users = _con.database.QueryAsync<UserVM>("Call SP_Retrieve_Users").Result.ToList();
        //    return Ok(users);
        //}

        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var sql = "GetUserById";            
        //    param.Add("userId", id);
        //    var data =  _con.database.QueryAsync<UserVM>(sql, param, commandType: CommandType.StoredProcedure).Result.SingleOrDefault();
        //    return Ok(data);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Insert(UserVM userVM)
        //{
        //    var sql = "SP_Insert_User";            
        //    param.Add("userName", userVM.UserName);
        //    var data = await _con.database.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
        //    return Ok(data);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, UserVM userVM)
        //{
        //    var sql = "SP_Update_User";
        //    param.Add("userId", id);
        //    param.Add("userName", userVM.UserName);
        //    var affectedRow = await _con.database.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
        //    return Ok(affectedRow);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var sql = "SP_Delete_User";
        //    param.Add("userId", id);
        //    var affectedRow = await _con.database.ExecuteAsync(sql,param,commandType: CommandType.StoredProcedure);
        //    return Ok(affectedRow);
        //}

    }
}