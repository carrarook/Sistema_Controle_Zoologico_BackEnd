using Microsoft.AspNetCore.Mvc;
using Sistema_Controle_Zoologico.Data;
using Sistema_Controle_Zoologico.Models.DTOs;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email &&
                                     u.Senha == loginDto.Senha); 

        if (usuario == null)
            return Unauthorized("Credenciais inválidas");

      
        HttpContext.Session.SetInt32("UserId", usuario.Id);
        HttpContext.Session.SetString("UserName", usuario.Nome);

        return Ok(new { usuario.Nome, usuario.Email });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        if (await _context.Usuarios.AnyAsync(u => u.Email == registerDto.Email))
            return BadRequest("Email já cadastrado");

        var usuario = new Usuario
        {
            Nome = registerDto.Nome,
            Email = registerDto.Email,
            Senha = registerDto.Senha 
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Ok(new { usuario.Nome, usuario.Email });
    }
}