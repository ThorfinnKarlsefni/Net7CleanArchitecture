using System;
using AutoMapper;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.WebApi.Controllers
{
    [Route("[action]")]
    [ApiController]
    [Authorize]
    public class ConsigneeController : Controller
    {
        private readonly IConsigneeRepository _consigneeRepository;
        private readonly IMapper _mapper;
        public ConsigneeController(IConsigneeRepository consigneeRepository, IMapper mapper)
        {
            _consigneeRepository = consigneeRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> ConsigneeList(string phone)
        {
            var consignees = _mapper.Map<List<ConsigneeDto>>(await _consigneeRepository.GetConsigneeList(phone));
            return Ok(consignees);
        }
    }
}

