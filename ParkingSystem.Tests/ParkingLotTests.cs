using ParkingSystem.Core;
using Xunit;

namespace ParkingSystem.Tests
{
    public class ParkingLotTests
    {
        [Fact]
        public void CreateParkingLot_ValidCapacity_Success()
        {
            ParkingLot parkingLot = new ParkingLot();
            int capacity = 6;

            parkingLot.CreateParkingLot(capacity);

            Assert.Equal(capacity, parkingLot.GetTotalSlots());
        }

        [Fact]
        public void ParkVehicle_ValidVehicle_Success()
        {
            ParkingLot parkingLot = new ParkingLot();
            parkingLot.CreateParkingLot(6);
            string registrationNumber = "B-1234-XYZ";
            string color = "Putih";
            string vehicleType = "Mobil";

            int allocatedSlotNumber = parkingLot.ParkVehicle(registrationNumber, color, vehicleType);

            Assert.NotEqual(-1, allocatedSlotNumber);
        }

        [Fact]
        public void LeaveSlot_ValidSlotNumber_Success()
        {
            ParkingLot parkingLot = new ParkingLot();
            parkingLot.CreateParkingLot(6);
            string registrationNumber = "B-1234-XYZ";
            string color = "Putih";
            string vehicleType = "Mobil";
            int allocatedSlotNumber = parkingLot.ParkVehicle(registrationNumber, color, vehicleType);

            bool isSlotFreed = parkingLot.LeaveSlot(allocatedSlotNumber);

            Assert.True(isSlotFreed);
        }

        [Fact]
        public void GetVehicleCountByNumberType_ValidNumberType_Success()
        {
            ParkingLot parkingLot = new ParkingLot();
            parkingLot.CreateParkingLot(6);
            string registrationNumber1 = "B-1234-XYZ";
            string color1 = "Putih";
            string vehicleType1 = "Mobil";
            string registrationNumber2 = "B-9999-XYZ";
            string color2 = "Putih";
            string vehicleType2 = "Motor";
            parkingLot.ParkVehicle(registrationNumber1, color1, vehicleType1);
            parkingLot.ParkVehicle(registrationNumber2, color2, vehicleType2);

            int countByType = parkingLot.GetVehicleCountByNumberType("Even");

            Assert.Equal(1, countByType);
        }

        // Add more test cases for other functionalities

    }
}
