using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.entity
{
    public class MaintenanceRecord
    {
        private int maintenanceId;
        private int assetId;
        private DateTime maintenanceDate;
        private string description;
        private decimal cost;

        public MaintenanceRecord() { }

        public MaintenanceRecord(int maintenanceId, int assetId, DateTime maintenanceDate, string description, decimal cost)
        {
            this.maintenanceId = maintenanceId;
            this.assetId = assetId;
            this.maintenanceDate = maintenanceDate;
            this.description = description;
            this.cost = cost;
        }

        public int GetMaintenanceId() { return maintenanceId; }
        public void SetMaintenanceId(int value) { maintenanceId = value; }

        public int GetAssetId() { return assetId; }
        public void SetAssetId(int value) { assetId = value; }

        public DateTime GetMaintenanceDate() { return maintenanceDate; }
        public void SetMaintenanceDate(DateTime value) { maintenanceDate = value; }

        public string GetDescription() { return description; }
        public void SetDescription(string value) { description = value; }

        public decimal GetCost() { return cost; }
        public void SetCost(decimal value) { cost = value; }

        public override string ToString()
        {
            return $"MaintenanceId: {maintenanceId}, AssetId: {assetId}, Date: {maintenanceDate.ToShortDateString()}, Desc: {description}, Cost: ₹{cost}";
        }
    }
}
