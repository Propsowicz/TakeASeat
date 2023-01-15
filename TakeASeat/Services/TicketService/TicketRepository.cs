using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using Syncfusion.Drawing;
using MimeKit;

namespace TakeASeat.Services.TicketService
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DatabaseContext _context;
        
        public TicketRepository(DatabaseContext context)
        {
            _context = context;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////// CLEAN THIS!!!

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
        public async Task SendTicketsViaEmail(List<Ticket> listOfTickets)
        {


            //var emailSender = new EmailSender("tomasiktomasz00@gmail.com", emailPassword.Value, emailAddress.Value);
            //await emailSender.SendEmailWithAttachment(listOfTickets);
            await SendEmailWithAttachment(listOfTickets, "tomasiktomasz00@gmail.com");

        }


        private async Task<MimeMessage> CreateEmailMessage(List<Ticket> listOfTickets, string emailTo, string emailFrom)
        {
            // mess to clean up!!
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("TakeASeat", emailFrom));
            emailMessage.To.Add(new MailboxAddress(emailTo, emailTo));
            emailMessage.Subject = "Tickets";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = "Hello! \n Here are yours tickets! \n\n Greetings!" };  

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", "Hello") };
            foreach (Ticket ticket in listOfTickets)
            {
                bodyBuilder.Attachments.Add("Ticket.pdf", await CreatePdfTicket(ticket));
            }
           
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }
        public async Task SendEmailWithAttachment(List<Ticket> listOfTickets, string emailTo)
        {
            var emailPassword = _context.ProtectedKeys.FirstOrDefault(k => k.Key == "EMAIL_PASSWORD");
            var emailAddress = _context.ProtectedKeys.FirstOrDefault(k => k.Key == "EMAIL_FROM");   // names of variables are bad :<

            var emailMessage = await CreateEmailMessage(listOfTickets, emailTo, emailAddress.Value);
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailAddress.Value, emailPassword.Value);
                    client.Send(emailMessage);
                }
                catch
                {
                    Console.WriteLine("something went wrong with sending email..");
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
        public async Task<MemoryStream> CreatePdfTicket(Ticket ticket)
        {
            

            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            PdfFont fontBig = new PdfStandardFont(PdfFontFamily.TimesRoman, 20);
            PdfFont fontMid = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            PdfFont fontSmall = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);
            PdfBrush black = PdfBrushes.Black;

            graphics.DrawString("Automatically Generated Ticket", fontSmall, black, new PointF(0, 0));
            graphics.DrawString($"Ticket for : {ticket.EventName} : {ticket.ShowDescription}", fontBig, black, new PointF(0, 16));
            graphics.DrawString($"{ticket.ShowDate}", fontBig, black, new PointF(600, 16));

            graphics.DrawString($"Row:", fontSmall, black, new PointF(0, 50));
            graphics.DrawString($"{ticket.Row}", fontMid, black, new PointF(0, 65));

            graphics.DrawString($"Position:", fontSmall, black, new PointF(300, 50));
            graphics.DrawString($"{ticket.Position}", fontMid, black, new PointF(300, 65));

            graphics.DrawString($"Price:", fontSmall, black, new PointF(600, 50));
            graphics.DrawString($"{ticket.Price}$", fontMid, black, new PointF(600, 65));

            graphics.DrawString($"Ticket Code:", fontSmall, black, new PointF(0, 80));
            graphics.DrawString($"{ticket.TickedCode}", fontMid, black, new PointF(0, 95));

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            return stream;
        }
    }

        
    
}
