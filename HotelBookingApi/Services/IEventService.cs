﻿using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event eventModel);
        Task<Event> UpdateEventAsync(int id, Event eventModel);
        Task<bool> DeleteEventAsync(int id);
    }
}
