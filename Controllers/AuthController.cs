using Backend1_2.Data;
using Backend1_2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend1_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
            private readonly SqlContext _context;
            private readonly IAuthService authService;

        public AuthController(SqlContext context, IAuthService authService)
        {
            _context = context;
            this.authService = authService;
        }
    }
}
