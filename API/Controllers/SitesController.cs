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
    public class SitesController : BaseController<Site, SiteRepository>
    {
        public SitesController(SiteRepository siteRepository) : base(siteRepository) { }
    }
}