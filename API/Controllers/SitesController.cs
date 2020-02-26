using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using API.Repositories.Interfaces;
using API.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly MySQLDatabase database;
        private readonly SiteRepository siteRepository;

        public SitesController(SiteRepository repository, MySQLDatabase database)
        {
            siteRepository = repository;
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //var sites = database.Connection.QueryAsync<Site>("Select * From Sites").Result.ToList();
            var sites = siteRepository.Get();
            return Ok(sites);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //var sites = database.Connection.QueryAsync<Site>("Select * From Sites Where Id = @Id", 
            //    new { Id = id }).Result.SingleOrDefault();
            var sites = siteRepository.Get(id);
            return Ok(sites);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Site site)
        {
            //var affectedRow = await database.Connection.ExecuteAsync("Insert Into Sites(Name, Address, Email, Phone, PIC) Values (@Name, @Address, @Email, @Phone, @PIC)", 
            //    new { Name = site.Name, Address = site.Address, Email = site.Email, Phone = site.Phone, PIC = site.PIC });
            var affectedRow = await siteRepository.Post(site);
            return Ok(affectedRow);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Site site)
        {
            //var affectedRow = await database.Connection.ExecuteAsync("Update Sites Set Name=@Name, Address=@Address, Email=@Email, Phone=@Phone, PIC=@PIC Where Id=@id", 
            //    new { id=id, Name = site.Name, Address = site.Address, Email = site.Email, Phone = site.Phone, PIC = site.PIC });
            var affectedRow = await siteRepository.Put(id, site);
            return Ok(affectedRow);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var affectedRow = await database.Connection.ExecuteAsync("Delete From Sites Where Id = @Id", new { Id = id });
            var affectedRow = await siteRepository.Delete(id);
            return Ok(affectedRow);
        }
    }
}