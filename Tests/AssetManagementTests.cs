using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Data.SqlClient;
using AssetManagementSystem.dao;
using AssetManagementSystem.entity;
using AssetManagementSystem.exception;
using AssetManagementSystem.util;

namespace AssetManagementSystem.Tests
{
    [TestFixture]
    public class AssetManagementTests
    {
        private IAssetManagementService service;

        [SetUp]
        public void Setup()
        {
            service = new AssetManagementServiceImpl();
        }

        // 🔧 Cleanup helper: removes linked records before deleting asset
        private void CleanupAsset(int assetId)
        {
            using (SqlConnection conn = new SqlConnection(DBPropertyUtil.GetConnectionString("db.properties")))
            {
                conn.Open();

                var deleteMaintenance = new SqlCommand("DELETE FROM maintenance_records WHERE asset_id = @id", conn);
                deleteMaintenance.Parameters.AddWithValue("@id", assetId);
                deleteMaintenance.ExecuteNonQuery();

                var deleteReservations = new SqlCommand("DELETE FROM reservations WHERE asset_id = @id", conn);
                deleteReservations.Parameters.AddWithValue("@id", assetId);
                deleteReservations.ExecuteNonQuery();

                var deleteAllocations = new SqlCommand("DELETE FROM asset_allocations WHERE asset_id = @id", conn);
                deleteAllocations.Parameters.AddWithValue("@id", assetId);
                deleteAllocations.ExecuteNonQuery();

                var deleteAsset = new SqlCommand("DELETE FROM assets WHERE asset_id = @id", conn);
                deleteAsset.Parameters.AddWithValue("@id", assetId);
                deleteAsset.ExecuteNonQuery();

                conn.Close();
            }
        }

        [Test]
        public void Test_AddAsset_Success()
        {
            Asset asset = new Asset(999, "Test Monitor", "Electronics", "TST-MNTR-999",
                DateTime.Now.Date, "Test Room", "Available", 999); // Uses test employee ID 999

            bool result = service.AddAsset(asset);

            Assert.IsTrue(result, "Asset should be added successfully.");

            CleanupAsset(999);
        }

        [Test]
        public void Test_PerformMaintenance_Success()
        {
            Asset asset = new Asset(998, "Test Keyboard", "Electronics", "TST-KBRD-998",
                DateTime.Now.AddYears(-1), "Lab", "Available", 999);
            service.AddAsset(asset);

            bool result = service.PerformMaintenance(998, DateTime.Now.ToString("yyyy-MM-dd"), "Test maintenance", 1000);

            Assert.IsTrue(result, "Maintenance should be added.");

            CleanupAsset(998);
        }

        [Test]
        public void Test_ReserveAsset_Success()
        {
            Asset asset = new Asset(997, "Test Mouse", "Electronics", "TST-MS-997",
                DateTime.Now, "IT", "Available", 999);
            service.AddAsset(asset);

            bool result = service.ReserveAsset(997, 999, DateTime.Now.ToString("yyyy-MM-dd"),
                                               DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"),
                                               DateTime.Now.AddDays(3).ToString("yyyy-MM-dd"));

            Assert.IsTrue(result, "Asset should be reserved.");

            CleanupAsset(997);
        }

        [Test]
        public void Test_AssetNotFoundException_OnDelete()
        {
            int invalidAssetId = -1;

            var ex = Assert.Throws<AssetNotFoundException>(() => service.DeleteAsset(invalidAssetId));
            Assert.That(ex.Message, Does.Contain("not found"));
        }

        [Test]
        public void Test_AssetNotMaintainException_WhenLastMaintained2YearsAgo()
        {
            Asset asset = new Asset(
                996,
                "Old Printer",
                "Electronics",
                "OLD-PRN-996",
                DateTime.Now.AddYears(-4),  
                "Archive",
                "Available",
                999);

            service.AddAsset(asset);

            service.PerformMaintenance(
                996,
                DateTime.Now.AddYears(-3).ToString("yyyy-MM-dd"),
                "Old repair",
                1500);

            var ex = Assert.Throws<AssetNotMaintainException>(() =>
                service.PerformMaintenance(
                    996,
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    "Blocked repair",
                    999));

            Assert.That(ex.Message, Does.Contain("not maintained in last 2 years"));

            CleanupAsset(996);
        }

    }
}
