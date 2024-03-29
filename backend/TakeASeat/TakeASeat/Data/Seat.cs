﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class Seat
    {
        public int Id { get; set; }
        public char Row { get; set; }
        public int Position { get; set; }
        public double Price { get; set; }
        public string SeatColor { get; set; }

        [ForeignKey(nameof(SeatReservation))]
        public int? ReservationId { get; set; }
        public SeatReservation? SeatReservation { get; set; }

        [ForeignKey(nameof(Show))]
        public int ShowId { get; set; }
        public Show Show { get; set; }

    }
}
