using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.entity
{
    public class Reservation
    {
        private int reservationId;
        private int assetId;
        private int employeeId;
        private DateTime reservationDate;
        private DateTime startDate;
        private DateTime endDate;
        private string status;

        public Reservation() { }

        public Reservation(int reservationId, int assetId, int employeeId, DateTime reservationDate, DateTime startDate, DateTime endDate, string status)
        {
            this.reservationId = reservationId;
            this.assetId = assetId;
            this.employeeId = employeeId;
            this.reservationDate = reservationDate;
            this.startDate = startDate;
            this.endDate = endDate;
            this.status = status;
        }

        public int GetReservationId() { return reservationId; }
        public void SetReservationId(int value) { reservationId = value; }

        public int GetAssetId() { return assetId; }
        public void SetAssetId(int value) { assetId = value; }

        public int GetEmployeeId() { return employeeId; }
        public void SetEmployeeId(int value) { employeeId = value; }

        public DateTime GetReservationDate() { return reservationDate; }
        public void SetReservationDate(DateTime value) { reservationDate = value; }

        public DateTime GetStartDate() { return startDate; }
        public void SetStartDate(DateTime value) { startDate = value; }

        public DateTime GetEndDate() { return endDate; }
        public void SetEndDate(DateTime value) { endDate = value; }

        public string GetStatus() { return status; }
        public void SetStatus(string value) { status = value; }

        public override string ToString()
        {
            return $"ReservationId: {reservationId}, AssetId: {assetId}, EmployeeId: {employeeId}, Status: {status}, From: {startDate.ToShortDateString()} To: {endDate.ToShortDateString()}";
        }
    }
}
