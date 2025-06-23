using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechNest.Domain.Interfaces;

namespace TechNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        public const string DefaultErrorMessage = "An unexpected error occurred while processing your request.";
        public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

    }
}
