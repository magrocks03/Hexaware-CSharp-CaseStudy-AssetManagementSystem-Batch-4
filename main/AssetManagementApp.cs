using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManagementSystem.dao;
using AssetManagementSystem.entity;
using AssetManagementSystem.exception;

namespace AssetManagementSystem.main
{
    class AssetManagementApp
    {
        static void Main(string[] args)
        {
            IAssetManagementService service = new AssetManagementServiceImpl();

            while (true)
            {
                Console.WriteLine("\n====== Asset Management Menu ======");
                Console.WriteLine("1. Add Asset");
                Console.WriteLine("2. Update Asset");
                Console.WriteLine("3. Delete Asset");
                Console.WriteLine("4. Allocate Asset");
                Console.WriteLine("5. Deallocate Asset");
                Console.WriteLine("6. Perform Maintenance");
                Console.WriteLine("7. Reserve Asset");
                Console.WriteLine("8. Withdraw Reservation");
                Console.WriteLine("9. Exit");
                Console.Write("Enter your choice: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Asset asset = new Asset();
                            Console.Write("Enter Asset ID: ");
                            asset.SetAssetId(int.Parse(Console.ReadLine()));
                            Console.Write("Enter Asset Name: ");
                            asset.SetName(Console.ReadLine());
                            Console.Write("Enter Asset Type: ");
                            asset.SetType(Console.ReadLine());
                            Console.Write("Enter Serial Number: ");
                            asset.SetSerialNumber(Console.ReadLine());
                            Console.Write("Enter Purchase Date (yyyy-MM-dd): ");
                            asset.SetPurchaseDate(DateTime.Parse(Console.ReadLine()));
                            Console.Write("Enter Location: ");
                            asset.SetLocation(Console.ReadLine());
                            Console.Write("Enter Status: ");
                            asset.SetStatus(Console.ReadLine());
                            Console.Write("Enter Owner Employee ID: ");
                            asset.SetOwnerId(int.Parse(Console.ReadLine()));

                            bool added = service.AddAsset(asset);
                            Console.WriteLine(added ? "✅ Asset added successfully." : "❌ Failed to add asset.");
                            break;

                        case 2:
                            Asset updatedAsset = new Asset();
                            Console.Write("Enter Asset ID to Update: ");
                            updatedAsset.SetAssetId(int.Parse(Console.ReadLine()));
                            Console.Write("Enter New Name: ");
                            updatedAsset.SetName(Console.ReadLine());
                            Console.Write("Enter New Type: ");
                            updatedAsset.SetType(Console.ReadLine());
                            Console.Write("Enter New Serial Number: ");
                            updatedAsset.SetSerialNumber(Console.ReadLine());
                            Console.Write("Enter New Purchase Date (yyyy-MM-dd): ");
                            updatedAsset.SetPurchaseDate(DateTime.Parse(Console.ReadLine()));
                            Console.Write("Enter New Location: ");
                            updatedAsset.SetLocation(Console.ReadLine());
                            Console.Write("Enter New Status: ");
                            updatedAsset.SetStatus(Console.ReadLine());
                            Console.Write("Enter New Owner ID: ");
                            updatedAsset.SetOwnerId(int.Parse(Console.ReadLine()));

                            bool updated = service.UpdateAsset(updatedAsset);
                            Console.WriteLine(updated ? "✅ Asset updated." : "❌ Update failed.");
                            break;

                        case 3:
                            Console.Write("Enter Asset ID to Delete: ");
                            int delId = int.Parse(Console.ReadLine());
                            bool deleted = service.DeleteAsset(delId);
                            Console.WriteLine(deleted ? "✅ Asset deleted." : "❌ Delete failed.");
                            break;

                        case 4:
                            Console.Write("Enter Asset ID: ");
                            int aid = int.Parse(Console.ReadLine());
                            Console.Write("Enter Employee ID: ");
                            int eid = int.Parse(Console.ReadLine());
                            Console.Write("Enter Allocation Date (yyyy-MM-dd): ");
                            string adate = Console.ReadLine();
                            Console.WriteLine(service.AllocateAsset(aid, eid, adate)
                                ? "✅ Asset allocated." : "❌ Allocation failed.");
                            break;

                        case 5:
                            Console.Write("Enter Asset ID: ");
                            int daid = int.Parse(Console.ReadLine());
                            Console.Write("Enter Employee ID: ");
                            int deid = int.Parse(Console.ReadLine());
                            Console.Write("Enter Return Date (yyyy-MM-dd): ");
                            string rdate = Console.ReadLine();
                            Console.WriteLine(service.DeallocateAsset(daid, deid, rdate)
                                ? "✅ Asset deallocated." : "❌ Deallocation failed.");
                            break;

                        case 6:
                            Console.Write("Enter Asset ID for Maintenance: ");
                            int mid = int.Parse(Console.ReadLine());
                            Console.Write("Enter Maintenance Date (yyyy-MM-dd): ");
                            string mdate = Console.ReadLine();
                            Console.Write("Enter Description: ");
                            string desc = Console.ReadLine();
                            Console.Write("Enter Cost: ");
                            double cost = double.Parse(Console.ReadLine());

                            bool maintained = service.PerformMaintenance(mid, mdate, desc, cost);
                            Console.WriteLine(maintained ? "✅ Maintenance recorded." : "❌ Maintenance failed.");
                            break;

                        case 7:
                            Console.Write("Enter Asset ID to Reserve: ");
                            int rsid = int.Parse(Console.ReadLine());
                            Console.Write("Enter Employee ID: ");
                            int reseid = int.Parse(Console.ReadLine());
                            Console.Write("Enter Reservation Date (yyyy-MM-dd): ");
                            string resdate = Console.ReadLine();
                            Console.Write("Enter Start Date (yyyy-MM-dd): ");
                            string sdate = Console.ReadLine();
                            Console.Write("Enter End Date (yyyy-MM-dd): ");
                            string edate = Console.ReadLine();
                            Console.WriteLine(service.ReserveAsset(rsid, reseid, resdate, sdate, edate)
                                ? "✅ Reservation successful." : "❌ Reservation failed.");
                            break;

                        case 8:
                            Console.Write("Enter Reservation ID to Withdraw: ");
                            int rid = int.Parse(Console.ReadLine());
                            Console.WriteLine(service.WithdrawReservation(rid)
                                ? "✅ Reservation withdrawn." : "❌ Withdraw failed.");
                            break;

                        case 9:
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (AssetNotFoundException ex)
                {
                    Console.WriteLine("❌ Asset Error: " + ex.Message);
                }
                catch (AssetNotMaintainException ex)
                {
                    Console.WriteLine("⚠️ Maintenance Error: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("⚠️ Invalid input format. Please try again.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ An unexpected error occurred: " + ex.Message);
                }
            }
        }
    }
}

