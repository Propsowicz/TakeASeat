using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using Syncfusion.Drawing;
using MimeKit;
using TakeASeat.ProgramConfigurations.DTO;
using TakeASeat.Services.PaymentService;
using System.Net.Mail;
using TakeASeat.Models;

namespace TakeASeat.Services.TicketService
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DatabaseContext _context;
        
        public TicketRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> CreateRangeOfTicketRecords(PaymentTransaction paymentTransaction)
        {
            List<Ticket> listOfTicketsToCreate = new List<Ticket>();
            foreach(var seatReservation in paymentTransaction.SeatReservations)
            {
                var seatsQuery = _context.Seats
                                .AsNoTracking()
                                .Where(s => s.ReservationId == seatReservation.Id)
                                .Include(s => s.Show)
                                    .ThenInclude(s => s.Event)
                                .ToList();
                foreach(var seat in seatsQuery)
                {
                    listOfTicketsToCreate.Add(new Ticket()
                    {
                        TickedCode = generateTicketCode(),
                        Row = seat.Row,
                        Position= seat.Position,
                        Price= seat.Price,
                        ShowDescription = seat.Show.Description,
                        ShowDate = seat.Show.Date,
                        EventName = seat.Show.Event.Name,
                        ShowId= seat.ShowId,
                        PaymentTransactionId = paymentTransaction.Id
                    });
                }
            }
            await _context.Ticket.AddRangeAsync(listOfTicketsToCreate);
            await _context.SaveChangesAsync();
            return listOfTicketsToCreate;
        }
        private int generateTicketCode()
        {
            Random randomObj = new Random();
            return randomObj.Next(1000000, 9999999);
        }
        public async Task SendTicketsViaEmail(List<Ticket> listOfTickets, UserDataToSendEmailDTO userData)
        {            
            await SendEmailWithAttachment(listOfTickets, userData);
        }        
        private async Task SendEmailWithAttachment(List<Ticket> listOfTickets, UserDataToSendEmailDTO userData)
        {
            var emailServiceProviderPassword = _context.ProtectedKeys.AsNoTracking().FirstOrDefault(k => k.Key == "EMAIL_PASSWORD");
            var emailServiceProviderAddress = _context.ProtectedKeys.AsNoTracking().FirstOrDefault(k => k.Key == "EMAIL_FROM");

            if (emailServiceProviderPassword == null || emailServiceProviderAddress == null)
            {
                throw new CantAccessDataException("Can't access Email Service Provider Data.");
            }          
            var emailMessage = await createEmail(listOfTickets, userData, emailServiceProviderAddress);
            await sendEmail(emailServiceProviderPassword, emailServiceProviderAddress, emailMessage);            
        }
        private async Task sendEmail(ProtectedKeys emailServiceProviderPassword, ProtectedKeys emailServiceProviderAddress, MimeMessage emailMessage)
        {
            var client = new MailKit.Net.Smtp.SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(emailServiceProviderAddress.Value, emailServiceProviderPassword.Value);
            client.Send(emailMessage);
            client.Disconnect(true);
            client.Dispose();
        }
        private async Task<MimeMessage> createEmail(List<Ticket> listOfTickets, UserDataToSendEmailDTO userData, ProtectedKeys emailServiceProviderAddress)
        {
            // mess to clean up!!
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("TakeASeat", emailServiceProviderAddress.Value));
            emailMessage.To.Add(new MailboxAddress(userData.UserName, userData.Email));
            emailMessage.Subject = "Your Tickets are finally here!";

            var bodyBuilder = new BodyBuilder { HtmlBody = $"<h2>Hello {userData.UserName}!</h2><p>Thank You for buying Tickets in our application. Enjoy!</p>" +
                                                                                                                    $"<p>Greetings,</p><p>TakeASeat Team</p>" };
            foreach (Ticket ticket in listOfTickets)
            {
                bodyBuilder.Attachments.Add("Ticket.pdf", await CreatePdfTicket(ticket));
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        public async Task<MemoryStream> CreatePdfTicket(Ticket ticket)
        {           
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            PdfFont fontBig = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);
            PdfFont fontMid = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            PdfFont fontSmall = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);
            PdfBrush black = PdfBrushes.Black;

            graphics.DrawString("Automatically Generated Ticket", fontSmall, black, new PointF(0, 0));
            graphics.DrawString($"Ticket for {ticket.EventName} : {ticket.ShowDescription}", fontBig, black, new PointF(0, 15));
            graphics.DrawString($"Date: {ticket.ShowDate}", fontBig, black, new PointF(0, 50));

            graphics.DrawString($"Row:", fontSmall, black, new PointF(0, 100));
            graphics.DrawString($"{ticket.Row}", fontMid, black, new PointF(0, 120));

            graphics.DrawString($"Position:", fontSmall, black, new PointF(200, 100));
            graphics.DrawString($"{ticket.Position}", fontMid, black, new PointF(200, 120));

            graphics.DrawString($"Price:", fontSmall, black, new PointF(400, 100));
            graphics.DrawString($"{ticket.Price}$", fontMid, black, new PointF(400, 120));

            graphics.DrawString($"Ticket Code:", fontSmall, black, new PointF(0, 150));
            graphics.DrawString($"{ticket.TickedCode}", fontMid, black, new PointF(0, 170));

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            return stream;
        }
    }       
}
