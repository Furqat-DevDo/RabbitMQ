using CoreApp.Entities;
using CoreApp.Entities.Enums;
using CoreApp.Models;

namespace CoreApp.Mappers;

public class TicketMapper : ITicketMapper
{
    public BookingModel ToModel(Ticket entity)
    {
        var model = new BookingModel
        {
            Id = entity.Id,
            Status = entity.Status.ToString(),
            BookingDateTime = entity.BookingDateTime,
            CancelledDateTime = entity.CancelledDateTime,
            FlightDateTime = entity.FlightDateTime,
            From = entity.From,
            To = entity.To,
            Price = entity.Price,
            OwnerInfo = new OwnerInfo(entity.OwnerFio,entity.OwnerAge,entity.OwnerAdrress)
        };
        
        return model;
    }

    public Ticket ToEntity(BookingCreateModel createModel)
    {
        var ticket = new Ticket
        {
            From = createModel.From,
            To = createModel.To,
            OwnerAdrress = createModel.OwnerInfo.Adrress,
            OwnerAge = createModel.OwnerInfo.Age,
            BookingDateTime = DateTime.UtcNow,
            FlightDateTime = createModel.FlightDateTime,
            OwnerFio = createModel.OwnerInfo.Fullname,
            Price = createModel.Price,
            Status = ETicketStatus.Created
        };
        
        return ticket;
    }
}