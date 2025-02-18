using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeditationApp.Data;
using MeditationApp.Models;
using AutoMapper;
using MeditationApp.Dtos;


[Route("api/[controller]")]
[ApiController]
public class MeditationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public MeditationController(IMapper mapper, ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSessions()
    {
        var sessions = await _context.MeditationSessions.ToListAsync();

        // Convert list of MeditationSession -> List of CreateSessionDto
        var sessionDtos = _mapper.Map<List<CreateSessionDto>>(sessions);

        return Ok(sessionDtos);
    }


    [HttpPost]
    public async Task<IActionResult> AddSession([FromBody] CreateSessionDto sessionDto)
    {
        if (sessionDto == null)
        {
            return BadRequest("Invalid session data.");
        }

        var session = _mapper.Map<MeditationSession>(sessionDto);
        _context.MeditationSessions.Add(session);
        await _context.SaveChangesAsync();

        // Return a DTO instead of full entity
        var createdSessionDto = _mapper.Map<CreateSessionDto>(session);

        return CreatedAtAction(nameof(GetSession), new { id = session.Id }, createdSessionDto);
    }

    [HttpGet("{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> GetSession(int id)
    {
        var session = await _context.MeditationSessions.FindAsync(id);
        if (session == null) return NotFound();

        var sessionDto = _mapper.Map<CreateSessionDto>(session);
        return Ok(sessionDto);
    }
}
