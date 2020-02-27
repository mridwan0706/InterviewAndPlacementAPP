using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewsController : BaseController<Interview, InterviewRepository>
    {
        private InterviewRepository interviewRepository;
        public InterviewsController(InterviewRepository interviewRepository) : base(interviewRepository)
        {
            this.interviewRepository = interviewRepository;
        }

        //[HttpPost]
        public IActionResult SendInvitation(Interview interview, string email)
        {
            var mail = interviewRepository.SendInvitation(interview, email);
            return Ok(mail);
        }

        //[HttpGet]
        public IActionResult GetEmployee(int id)
        {
            var employee = interviewRepository.GetEmployee(id);
            return Ok(employee);
        }
    }
}