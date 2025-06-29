using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.entity
{
    public class Asset
    {
        private int assetId;
        private string name;
        private string type;
        private string serialNumber;
        private DateTime purchaseDate;
        private string location;
        private string status;
        private int ownerId;

        public Asset() { }

        public Asset(int assetId, string name, string type, string serialNumber, DateTime purchaseDate, string location, string status, int ownerId)
        {
            this.assetId = assetId;
            this.name = name;
            this.type = type;
            this.serialNumber = serialNumber;
            this.purchaseDate = purchaseDate;
            this.location = location;
            this.status = status;
            this.ownerId = ownerId;
        }

        public int GetAssetId() { return assetId; }
        public void SetAssetId(int value) { assetId = value; }

        public string GetName() { return name; }
        public void SetName(string value) { name = value; }

        public string GetType() { return type; }
        public void SetType(string value) { type = value; }

        public string GetSerialNumber() { return serialNumber; }
        public void SetSerialNumber(string value) { serialNumber = value; }

        public DateTime GetPurchaseDate() { return purchaseDate; }
        public void SetPurchaseDate(DateTime value) { purchaseDate = value; }

        public string GetLocation() { return location; }
        public void SetLocation(string value) { location = value; }

        public string GetStatus() { return status; }
        public void SetStatus(string value) { status = value; }

        public int GetOwnerId() { return ownerId; }
        public void SetOwnerId(int value) { ownerId = value; }

        public override string ToString()
        {
            return $"AssetId: {assetId}, Name: {name}, Type: {type}, SerialNo: {serialNumber}, Location: {location}, Status: {status}, OwnerId: {ownerId}";
        }
    }
}
