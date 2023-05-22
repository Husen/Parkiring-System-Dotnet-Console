using System;
using System.Collections.Generic;

namespace ParkingSystem.Core
{
    public class ParkingLot
    {
        private int totalSlots;
        private int availableSlots;
        private List<ParkingSlot> slots;

        public ParkingLot(int totalSlots)
        {
            this.totalSlots = totalSlots;
            this.availableSlots = totalSlots;
            this.slots = new List<ParkingSlot>();

            for (int i = 0; i < totalSlots; i++)
            {
                slots.Add(new ParkingSlot(i + 1));
            }
        }

        public bool IsFull()
        {
            return availableSlots == 0;
        }

        public int GetTotalSlots()
        {
            return totalSlots;
        }

        public int GetAvailableSlots()
        {
            return availableSlots;
        }

        public void ParkVehicle(Vehicle vehicle)
        {
            if (IsFull())
            {
                Console.WriteLine("Sorry, parking lot is full");
                return;
            }

            ParkingSlot slot = FindAvailableSlot();
            slot.ParkVehicle(vehicle);
            availableSlots--;

            Console.WriteLine($"Allocated slot number: {slot.SlotNumber}");
        }

        public void Leave(int slotNumber)
        {
            if (slotNumber < 1 || slotNumber > totalSlots)
            {
                Console.WriteLine("Invalid slot number");
                return;
            }

            ParkingSlot slot = slots[slotNumber - 1];
            slot.ClearSlot();
            availableSlots++;

            Console.WriteLine($"Slot number {slotNumber} is free");
        }

        public void GetParkingLotStatus()
        {
            Console.WriteLine("Slot\tNo.\tType\tRegistration No\tColour");
            foreach (ParkingSlot slot in slots)
            {
                if (slot.IsOccupied())
                {
                    Vehicle vehicle = slot.GetParkedVehicle();
                    Console.WriteLine($"{slot.SlotNumber}\t{vehicle.RegistrationNumber}\t{vehicle.Type}\t{vehicle.Color}");
                }
            }
        }

        public int GetVehicleCountByType(string type)
        {
            int count = 0;
            foreach (ParkingSlot slot in slots)
            {
                if (slot.IsOccupied() && slot.GetParkedVehicle().Type == type)
                {
                    count++;
                }
            }
            return count;
        }

        public int GetVehicleCountByNumberType(string numberType)
        {
            int count = 0;
            foreach (ParkingSlot slot in slots)
            {
                if (slot.IsOccupied())
                {
                    string lastCharacter = slot.GetParkedVehicle().RegistrationNumber.Substring(slot.GetParkedVehicle().RegistrationNumber.Length - 1);

                    if ((numberType == "Odd" && int.Parse(lastCharacter) % 2 != 0) ||
                        (numberType == "Even" && int.Parse(lastCharacter) % 2 == 0))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int GetVehicleCountByColor(string color)
        {
            int count = 0;
            foreach (ParkingSlot slot in slots)
            {
                if (slot.IsOccupied() && slot.GetParkedVehicle().Color == color)
                {
                    count++;
                }
            }
            return count;
        }

        public Vehicle GetVehicleBySlotNumber(int slotNumber)
        {
            if (slotNumber < 1 || slotNumber > totalSlots)
            {
                return null;
            }

            ParkingSlot slot = slots[slotNumber - 1];
            return slot.GetParkedVehicle();
        }

        private ParkingSlot FindAvailableSlot()
        {
            foreach (ParkingSlot slot in slots)
            {
                if (!slot.IsOccupied())
                {
                    return slot;
                }
            }
            return null;
        }
    }

    public class ParkingSlot
    {
        public int SlotNumber { get; }
        private Vehicle parkedVehicle;

        public ParkingSlot(int slotNumber)
        {
            SlotNumber = slotNumber;
            parkedVehicle = null;
        }

        public bool IsOccupied()
        {
            return parkedVehicle != null;
        }

        public Vehicle GetParkedVehicle()
        {
            return parkedVehicle;
        }

        public void ParkVehicle(Vehicle vehicle)
        {
            parkedVehicle = vehicle;
        }

        public void ClearSlot()
        {
            parkedVehicle = null;
        }
    }

    public abstract class Vehicle
    {
        public string RegistrationNumber { get; }
        public string Color { get; }
        public string Type { get; }

        public Vehicle(string registrationNumber, string color, string type)
        {
            RegistrationNumber = registrationNumber;
            Color = color;
            Type = type;
        }
    }

    public class Car : Vehicle
    {
        public Car(string registrationNumber, string color)
            : base(registrationNumber, color, "Mobil")
        {
        }
    }

    public class Motorcycle : Vehicle
    {
        public Motorcycle(string registrationNumber, string color)
            : base(registrationNumber, color, "Motor")
        {
        }
    }
}



