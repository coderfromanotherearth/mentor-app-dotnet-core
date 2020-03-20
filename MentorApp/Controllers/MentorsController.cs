using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentorApp.Models;
using MySql.Data.MySqlClient;

namespace MentorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorsController : ControllerBase
    {
        private readonly MentorContext _context;

        public MentorsController(MentorContext context)
        {
            _context = context;
        }

        // GET: api/Mentors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mentor>>> GetMentors()
        {
            MentorContext context = HttpContext.RequestServices.GetService(typeof(MentorContext)) as MentorContext;
            List<Mentor> mentorsDetails = context.GetAllMentors();
            if(mentorsDetails == null || mentorsDetails.Count == 0)
            {
                return NoContent();
            }
            return Ok(mentorsDetails);
        }

        // GET: api/Mentors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mentor>> GetMentor(int id)
        {
            MentorContext context = HttpContext.RequestServices.GetService(typeof(MentorContext)) as MentorContext;
            Mentor mentorDetails = context.GetMentorById(id);
            if (mentorDetails == null)
            {
                return NoContent();
            }
            return Ok(mentorDetails);
        }

        // POST: api/Mentors
        [HttpPost]
        public async Task<ActionResult<bool>> PostMentor(Mentor mentor)
        {
            MentorContext context = HttpContext.RequestServices.GetService(typeof(MentorContext)) as MentorContext;
            return Ok(context.AddMentor(mentor));
        }

        // DELETE: api/Mentors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Mentor>> DeleteMentor(int id)
        {
            MentorContext context = HttpContext.RequestServices.GetService(typeof(MentorContext)) as MentorContext;
            return Ok(context.DeleteMentorById(id));
        }
    }
}
