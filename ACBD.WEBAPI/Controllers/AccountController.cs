using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Models.ViewModel;
//using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ACBD.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager
            ,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task <IActionResult> Register([FromBody]RegisterDTO registerDTO)
        {
            if(ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                    PasswordHash = registerDTO.Password,
                    Address = registerDTO.Address,
                };
                var x=await userManager.CreateAsync(user,registerDTO.Password);
                if(x.Succeeded)
                {
                    return Ok("Successful created");
                }
                foreach (var item in x.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                    
                }

            }
            return BadRequest(ModelState);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
               
                var user=await userManager.FindByNameAsync(loginDTO.UserName);
                if (user != null)
                {
                    var found=await userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (found == true)
                    {
                        var userRoles =await userManager.GetRolesAsync(user);
                        List<Claim> userclaims=new List<Claim>();
                        userclaims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));
                        userclaims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        foreach (var item in userRoles)
                        {
                            userclaims.Add(new Claim(ClaimTypes.Role, item));
                            
                        }
                        userclaims.Add(new Claim(JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString()));
                       // var securityKey = "@RRRRRRRRRRtybvcccb%888887511HHHHHjjjj8755&8";
                        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWt:SecretKey"]));
                        var signingCredentials = new SigningCredentials(signinKey,SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                            issuer: configuration["JWt:IssuerIP"],
                            audience: configuration["JWt:AudienceIP"],
                            expires: DateTime.UtcNow.AddHours(6),
                            claims:userclaims //to do payload for token
                            ,signingCredentials: signingCredentials


                            );
                        TokenDTO tokenDTO ;
                        return Ok(
                            tokenDTO = new TokenDTO
                            {
                                // to generaye token sa string
                                token1 = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                                Expire = DateTime.Now.AddHours(6)

                            }



                            );
                            

                    }
                }
                ModelState.AddModelError("username is invaild", "username or password is invaild");
            }
            return BadRequest(ModelState);
            
        }
    }
}
