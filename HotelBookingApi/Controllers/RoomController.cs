using AutoMapper;
using HotelBookingApi.DTOs;
using HotelBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{

    [Route("api/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var rooms = await _roomService.GetAllRooms(cancellationToken);
            var roomDtos = _mapper.Map<RoomDto>(rooms);
            return Ok(roomDtos);
        }
    }
}
