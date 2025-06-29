using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AssetManagementSystem.entity;
using AssetManagementSystem.util;
using AssetManagementSystem.exception;

namespace AssetManagementSystem.dao
{
    public class AssetManagementServiceImpl : IAssetManagementService
    {
        private readonly string propFile = "db.properties";

        private SqlConnection GetConnection()
        {
            return DBConnUtil.GetConnection(propFile);
        }

        /* -------------------------------------------------
           a. Add Asset
        -------------------------------------------------*/
        public bool AddAsset(Asset asset)
        {
            using (SqlConnection conn = GetConnection())
            {
                string query = @"INSERT INTO assets
                                (asset_id, name, type, serial_number, purchase_date,
                                 location, status, owner_id)
                                 VALUES
                                (@id, @name, @type, @serial, @pdate,
                                 @location, @status, @owner)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", asset.GetAssetId());
                cmd.Parameters.AddWithValue("@name", asset.GetName());
                cmd.Parameters.AddWithValue("@type", asset.GetType());
                cmd.Parameters.AddWithValue("@serial", asset.GetSerialNumber());
                cmd.Parameters.AddWithValue("@pdate", asset.GetPurchaseDate());
                cmd.Parameters.AddWithValue("@location", asset.GetLocation());
                cmd.Parameters.AddWithValue("@status", asset.GetStatus());
                cmd.Parameters.AddWithValue("@owner", asset.GetOwnerId());

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /* -------------------------------------------------
           b. Update Asset
        -------------------------------------------------*/
        public bool UpdateAsset(Asset asset)
        {
            using (SqlConnection conn = GetConnection())
            {
                string query = @"UPDATE assets SET
                                 name          = @name,
                                 type          = @type,
                                 serial_number = @serial,
                                 purchase_date = @pdate,
                                 location      = @location,
                                 status        = @status,
                                 owner_id      = @owner
                                 WHERE asset_id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", asset.GetAssetId());
                cmd.Parameters.AddWithValue("@name", asset.GetName());
                cmd.Parameters.AddWithValue("@type", asset.GetType());
                cmd.Parameters.AddWithValue("@serial", asset.GetSerialNumber());
                cmd.Parameters.AddWithValue("@pdate", asset.GetPurchaseDate());
                cmd.Parameters.AddWithValue("@location", asset.GetLocation());
                cmd.Parameters.AddWithValue("@status", asset.GetStatus());
                cmd.Parameters.AddWithValue("@owner", asset.GetOwnerId());

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /* -------------------------------------------------
           c. Delete Asset  (throws AssetNotFoundException)
        -------------------------------------------------*/
        public bool DeleteAsset(int assetId)
        {
            using (SqlConnection conn = GetConnection())
            {
                // Verify existence
                string chk = "SELECT COUNT(*) FROM assets WHERE asset_id=@id";
                SqlCommand chkCmd = new SqlCommand(chk, conn);
                chkCmd.Parameters.AddWithValue("@id", assetId);
                if ((int)chkCmd.ExecuteScalar() == 0)
                    throw new AssetNotFoundException($"Asset ID {assetId} not found.");

                // Delete
                string del = "DELETE FROM assets WHERE asset_id=@id";
                SqlCommand delCmd = new SqlCommand(del, conn);
                delCmd.Parameters.AddWithValue("@id", assetId);
                return delCmd.ExecuteNonQuery() > 0;
            }
        }

        /* -------------------------------------------------
           d. Allocate Asset
        -------------------------------------------------*/
        public bool AllocateAsset(int assetId, int employeeId, string allocationDate)
        {
            using (SqlConnection conn = GetConnection())
            {
                string query = @"INSERT INTO asset_allocations
                                (asset_id, employee_id, allocation_date)
                                VALUES (@aid, @eid, @adate)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@aid", assetId);
                cmd.Parameters.AddWithValue("@eid", employeeId);
                cmd.Parameters.AddWithValue("@adate", DateTime.Parse(allocationDate));

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /* -------------------------------------------------
           e. Deallocate Asset
        -------------------------------------------------*/
        public bool DeallocateAsset(int assetId, int employeeId, string returnDate)
        {
            using (SqlConnection conn = GetConnection())
            {
                string query = @"UPDATE asset_allocations
                                 SET return_date = @rdate
                                 WHERE asset_id  = @aid
                                   AND employee_id = @eid
                                   AND return_date IS NULL";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@aid", assetId);
                cmd.Parameters.AddWithValue("@eid", employeeId);
                cmd.Parameters.AddWithValue("@rdate", DateTime.Parse(returnDate));

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /* -------------------------------------------------
           f. Perform Maintenance
              - throws AssetNotFoundException
              - throws AssetNotMaintainException (>2 years)
        -------------------------------------------------*/
        public bool PerformMaintenance(int assetId, string maintenanceDate, string description, double cost)
        {
            using (SqlConnection conn = GetConnection())
            {
                // Check asset existence
                string chk = "SELECT COUNT(*) FROM assets WHERE asset_id=@aid";
                SqlCommand chkCmd = new SqlCommand(chk, conn);
                chkCmd.Parameters.AddWithValue("@aid", assetId);
                if ((int)chkCmd.ExecuteScalar() == 0)
                    throw new AssetNotFoundException($"Asset ID {assetId} not found.");

                // Fetch last maintenance
                string lastQ = "SELECT MAX(maintenance_date) FROM maintenance_records WHERE asset_id=@aid";
                SqlCommand lastCmd = new SqlCommand(lastQ, conn);
                lastCmd.Parameters.AddWithValue("@aid", assetId);
                object lastVal = lastCmd.ExecuteScalar();

                if (lastVal != DBNull.Value && lastVal != null)
                {
                    DateTime lastDate = Convert.ToDateTime(lastVal);
                    if ((DateTime.Now - lastDate).TotalDays > 730)  // >2 years
                        throw new AssetNotMaintainException($"Asset ID {assetId} not maintained in last 2 years.");
                }

                // Insert maintenance record
                string ins = @"INSERT INTO maintenance_records
                               (asset_id, maintenance_date, description, cost)
                               VALUES (@aid, @mdate, @desc, @cost)";
                SqlCommand insCmd = new SqlCommand(ins, conn);
                insCmd.Parameters.AddWithValue("@aid", assetId);
                insCmd.Parameters.AddWithValue("@mdate", DateTime.Parse(maintenanceDate));
                insCmd.Parameters.AddWithValue("@desc", description);
                insCmd.Parameters.AddWithValue("@cost", cost);

                return insCmd.ExecuteNonQuery() > 0;
            }
        }

        /* -------------------------------------------------
           g. Reserve Asset
        -------------------------------------------------*/
        public bool ReserveAsset(int assetId, int employeeId, string reservationDate,
                                 string startDate, string endDate)
        {
            using (SqlConnection conn = GetConnection())
            {
                string query = @"INSERT INTO reservations
                                (asset_id, employee_id, reservation_date,
                                 start_date, end_date, status)
                                 VALUES
                                (@aid, @eid, @rdate, @sdate, @edate, 'Pending')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@aid", assetId);
                cmd.Parameters.AddWithValue("@eid", employeeId);
                cmd.Parameters.AddWithValue("@rdate", DateTime.Parse(reservationDate));
                cmd.Parameters.AddWithValue("@sdate", DateTime.Parse(startDate));
                cmd.Parameters.AddWithValue("@edate", DateTime.Parse(endDate));

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /* -------------------------------------------------
           h. Withdraw Reservation
        -------------------------------------------------*/
        public bool WithdrawReservation(int reservationId)
        {
            using (SqlConnection conn = GetConnection())
            {
                string query = "DELETE FROM reservations WHERE reservation_id = @rid";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@rid", reservationId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

