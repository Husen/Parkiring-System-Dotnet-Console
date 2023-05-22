using System;
using ParkingSystem.Core;

namespace ParkingSystem.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ParkingLot parkingLot = null;

            while (true)
            {
                Console.WriteLine("Enter command:");
                string command = Console.ReadLine();

                if (command == "exit")
                    break;

                string[] commandParts = command.Split(' ');
                string action = commandParts[0];

                switch (action)
                {
                    case "create_parking_lot":
                        int totalSlots = int.Parse(commandParts[1]);
                        parkingLot = new ParkingLot(totalSlots);
                        Console.WriteLine($"Created a parking lot with {totalSlots} slots");
                        break;

                    case "park":
                        string registrationNumber = commandParts[1];
                        string color = commandParts[2];
                        string type = commandParts[3];

                        if (parkingLot == null)
                        {
                            Console.WriteLine("Parking lot is not created yet");
                            break;
                        }

                        if (type == "Mobil")
                        {
                            Car car = new Car(registrationNumber, color);
                            parkingLot.ParkVehicle(car);
                        }
                        else if (type == "Motor")
                        {
                            Motorcycle motorcycle = new Motorcycle(registrationNumber, color);
                            parkingLot.ParkVehicle(motorcycle);
                        }
                        else
                        {
                            Console.WriteLine("Invalid vehicle type");
                        }
                        break;

                    case "leave":
                        int slotNumber = int.Parse(commandParts[1]);

                        if (parkingLot == null)
                        {
                            Console.WriteLine("Parking lot is not created yet");
                            break;
                        }

                        parkingLot.Leave(slotNumber);
                        break;

                    case "status":
                        if (parkingLot == null)
                        {
                            Console.WriteLine("Parking lot is not created yet");
                            break;
                        }

                        parkingLot.GetParkingLotStatus();
                        break;

                    case "type_of_vehicles":
                        string vehicleType = commandParts[1];

                        if (parkingLot == null)
                        {
                            Console.WriteLine("Parking lot is not created yet");
                            break;
                        }

                        int countByType = parkingLot.GetVehicleCountByType(vehicleType);
                        Console.WriteLine(countByType);
                        break;

                    case "registration_numbers_for_vehicles_with_odd_plate":
                        if (parkingLot == null)
                        {
                            Console.WriteLine("Parking lot is not created yet");
                            break;
                        }

                        int countByOddPlate = parkingLot.GetVehicleCountByNumberType("Odd");
                        string[] oddPlateNumbers = new string[countByOddPlate];
                        int index = 0;

                        for (int i = 1; i <= parkingLot.GetTotalSlots(); i++)
                        {
                            Vehicle vehicle = parkingLot.GetVehicleBySlotNumber(i);
                            if (vehicle != null)
                            {
                                string lastCharacter = vehicle.RegistrationNumber.Substring(vehicle.RegistrationNumber.Length - 1);
                                if (int.Parse(lastCharacter) % 2 != 0)
                                {
                                    oddPlateNumbers[index] = vehicle.RegistrationNumber;
                                    index++;
                                }
                            }
                        }

                        Console.WriteLine(string.Join(", ", oddPlateNumbers));
                        break;

                    case "registration_numbers_for_vehicles_with_even_plate":
                        if (parkingLot == null)
                        {
                            Console.WriteLine("Parking lot is not created yet");
                            break;
                        }

                        int countByEvenPlate = parkingLot.GetVehicleCountByNumberType("Even");
                        string[] evenPlateNumbers = new string[countByEvenPlate];
                        index = 0;

                        for (int i = 1; i <= parkingLot.GetTotalSlots(); i++)
                        {
                            Vehicle vehicle = parkingLot.GetVehicleBySlotNumber(i);
                            if (vehicle != null)
                            {
                                string lastCharacter = vehicle.RegistrationNumber.Substring(vehicle.RegistrationNumber.Length - 1);
                                if (int.Parse(lastCharacter) % 2 == 0)
                                {
                                    evenPlateNumbers[index] = vehicle.RegistrationNumber;
                                    index++;
                                }
                            }
                        }

                        Console.WriteLine(string.Join(", ", evenPlateNumbers));
                        break;

                    case "registration_numbers_for_vehicles_with_colour":
                        string vehicleColor = commandParts[1];

                        if (parkingLot == null)
                        {
                            Console.WriteLine("Parking lot is not created yet");
                            break;
                        }

                        int countByColor = parkingLot.GetVehicleCountByColor(vehicleColor);
                        string[] colorPlateNumbers = new string[countByColor];
                        index = 0;

                        for (int i = 1; i <= parkingLot.GetTotalSlots(); i++)
                        {
                            Vehicle vehicle = parkingLot.GetVehicleBySlotNumber(i);
                            if (vehicle != null && vehicle.Color == vehicleColor)
                            {
                                colorPlateNumbers[index] = vehicle.RegistrationNumber;
                                index++;
                            }
                        }

                        Console.WriteLine(string.Join(", ", colorPlateNumbers));
                        break;

                    case "slot_numbers_for_vehicles_with_colour":
                        string vehicleColor1 = commandParts[1];

                        if (parkingLot == null)
                        {
                            Console.WriteLine("Parking lot is not created yet");
                            break;
                        }

                        int countByColor1 = parkingLot.GetVehicleCountByColor(vehicleColor1);
                        int[] colorSlotNumbers = new int[countByColor1];
                        index = 0;

                        for (int i = 1; i <= parkingLot.GetTotalSlots(); i++)
                        {
                            Vehicle vehicle = parkingLot.GetVehicleBySlotNumber(i);
                            if (vehicle != null && vehicle.Color == vehicleColor1)
                            {
                                colorSlotNumbers[index] = i;
                                index++;
                            }
                        }

                        Console.WriteLine(string.Join(", ", colorSlotNumbers));
                        break;

                    case "slot_number_for_registration_number":
                        string registrationNumber1 = commandParts[1];

                        if (parkingLot == null)
                        {
                            Console.WriteLine("Parking lot is not created yet");
                            break;
                        }

                        int slotNumberForRegistration = -1;

                        for (int i = 1; i <= parkingLot.GetTotalSlots(); i++)
                        {
                            Vehicle vehicle = parkingLot.GetVehicleBySlotNumber(i);
                            if (vehicle != null && vehicle.RegistrationNumber == registrationNumber1)
                            {
                                slotNumberForRegistration = i;
                                break;
                            }
                        }

                        if (slotNumberForRegistration == -1)
                        {
                            Console.WriteLine("Not found");
                        }
                        else
                        {
                            Console.WriteLine(slotNumberForRegistration);
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }
    }
}
