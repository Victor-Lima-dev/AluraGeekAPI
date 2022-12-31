using AluraGeekAPI.Context;
using AluraGeekAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluraGeekAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //criar usando o identity
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;

        //construtor
        public UsuariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        //metodo para registrar um usuario
        //POST /usuarios/Registrar
        [HttpPost("Registrar")]
        public async Task<ActionResult> Registrar(Usuario usuario)
        {
            //criar um identityUser

            var user = new IdentityUser
            {
                UserName = usuario.Nome                     
            };           
            //criar o usuario
            var result = await _userManager.CreateAsync(user, usuario.Senha);
            //verificar se o usuario foi criado
            if (result.Succeeded)
            {
                //salvar no banco de dados
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return Ok("Usuario Criado");
            }
            else
            {
                return BadRequest(result.Errors);
            }            
        }

        //metodo para listar todos os usuarios
        //GET /usuarios/Listar
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Usuario>>> Listar()
        {
            var listaUsuarios = await _context.UserLogins.ToListAsync();
            return Ok(listaUsuarios);
        }



    }
}
