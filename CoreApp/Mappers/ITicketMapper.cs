using CoreApp.Entities;
using CoreApp.Models;

namespace CoreApp.Mappers;

public interface ITicketMapper
{
    public BookingModel ToModel(Ticket entity);
    public Ticket ToEntity(BookingCreateModel createModel);
}