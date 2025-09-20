namespace CQRS_Project.CQRS.Queries.ReservationQueries
{
	public class GetReservationByIdQuery
	{
		public int ReservationId { get; set; }

		public GetReservationByIdQuery(int reservationId)
		{
			ReservationId = reservationId;
		}
	}
}
