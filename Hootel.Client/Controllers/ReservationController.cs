﻿using Hootel.Client.DTO;
using Hootel.Client.Services.Interfaces;
using Hootel.Client.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hootel.Client.Controllers;

public class ReservationController : Controller
{
    private readonly IRoomService _roomService;
    private readonly IHotelService _hotelService;
    
    public ReservationController(IRoomService roomService, IHotelService hotelService)
    {
        _roomService = roomService;
        _hotelService = hotelService;
    }

    [HttpPost("reserva")]
    public async Task<IActionResult> Reservation(string rId)
    {
        var room = await _roomService.GetRoom(int.Parse(rId));
        var hotel = await _hotelService.GetHotel(room.HotelId);
        return View(new HotelRoomViewModel()
        {
            Room = room,
            Hotel = hotel
        });
    }


    [HttpPost("reservado")]
    public async Task<IActionResult> Reserved(ReservationDTO dto)
    {
        var room = await _roomService.GetRoom(int.Parse(dto.RoomId));
        var availableRooms = await _roomService.GetAvailableRooms(dto.CheckIn, dto.CheckOut);
        
        ViewBag.Available = false;
        if(availableRooms.Contains(room))
        {   
            ViewBag.Available = true;
            
        }
        
        return View();
    }
}