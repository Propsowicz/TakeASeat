using Microsoft.AspNetCore.Mvc;
using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.TicketService
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> CreateRangeOfTicketRecords(PaymentTransaction paymentTransaction);        
        Task SendTicketsViaEmail(List<Ticket> listOfTickets, UserDataToSendEmailDTO userData);
    }
}
