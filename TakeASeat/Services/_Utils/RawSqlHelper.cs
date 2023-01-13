using System.Collections.Generic;
using TakeASeat.Data;

namespace TakeASeat.Services._Utils
{
    public class RawSqlHelper
    {
        public static string WHERE_ReservationId_is_Id (List<int> list)
        {
            string WhereConditions = string.Empty;
            for (var i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    WhereConditions += $"ReservationId = {list[i]}";
                }
                else
                {
                    WhereConditions += $"ReservationId = {list[i]} OR ";
                }
            }

            return WhereConditions;
        }

        public static string WHERE_Id_is_SeatId(IEnumerable<Seat> enuList)
        {
            var listedSeats = enuList.ToList();

            string WhereConditions = string.Empty;
            for (var i = 0; i < listedSeats.Count; i++)
            {
                if (i == listedSeats.Count - 1)
                {
                    WhereConditions += $"Id = {listedSeats[i].Id}";
                }
                else
                {
                    WhereConditions += $"Id = {listedSeats[i].Id} OR ";
                }
            }

            return WhereConditions;
        }
        public static string WHERE_Id_is_Id(List<int> list)
        {
            string WhereConditions = string.Empty;
            for (var i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    WhereConditions += $"Id = {list[i]}";
                }
                else
                {
                    WhereConditions += $"Id = {list[i]} OR ";
                }
            }

            return WhereConditions;
        }
    }
}
