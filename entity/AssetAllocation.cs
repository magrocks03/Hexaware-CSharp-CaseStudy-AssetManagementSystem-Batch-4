using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.entity
{
    public class AssetAllocation
    {
        private int allocationId;
        private int assetId;
        private int employeeId;
        private DateTime allocationDate;
        private DateTime? returnDate;

        public AssetAllocation() { }

        public AssetAllocation(int allocationId, int assetId, int employeeId, DateTime allocationDate, DateTime? returnDate)
        {
            this.allocationId = allocationId;
            this.assetId = assetId;
            this.employeeId = employeeId;
            this.allocationDate = allocationDate;
            this.returnDate = returnDate;
        }

        public int GetAllocationId() { return allocationId; }
        public void SetAllocationId(int value) { allocationId = value; }

        public int GetAssetId() { return assetId; }
        public void SetAssetId(int value) { assetId = value; }

        public int GetEmployeeId() { return employeeId; }
        public void SetEmployeeId(int value) { employeeId = value; }

        public DateTime GetAllocationDate() { return allocationDate; }
        public void SetAllocationDate(DateTime value) { allocationDate = value; }

        public DateTime? GetReturnDate() { return returnDate; }
        public void SetReturnDate(DateTime? value) { returnDate = value; }

        public override string ToString()
        {
            return $"AllocationId: {allocationId}, AssetId: {assetId}, EmployeeId: {employeeId}, Allocation: {allocationDate.ToShortDateString()}, Return: {(returnDate.HasValue ? returnDate.Value.ToShortDateString() : "Pending")}";
        }
    }
}

