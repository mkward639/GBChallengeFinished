using static System.Console;

public class ProgramUI
{
    private readonly DeliveryRepository _devRepo = new DeliveryRepository();

    public void Run()
    {
        SeedDeliveries();
        RunApplication();
    }

    private void RunApplication()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Clear();

            WriteLine("|Action Menu|\n" +
                      "~~~~~~~~~~~~~\n" +
            "1. Update Delivery Status\n" +
            "2. Enroute Deliveries\n" +
            "3. Completed Deliveries\n" +
            "4. Deliveries\n" +
            "5. Add Delivery\n" +
            "6. Delivery using Customer ID\n" +
            "7. Delete Delivery\n" +
            "00. Exit");

            var userInput = int.Parse(ReadLine()!);
            switch (userInput)
            {
                case 1:
                    UpdateDeliveryStatus();
                    break;
                case 2:
                    GetAllEnrouteDeliveries();
                    break;
                case 3:
                    GetAllCompletedDeliveries();
                    break;
                case 4:
                    GetAllDeliveries();
                    break;
                case 5:
                    AddDelivery();
                    break;
                case 6:
                    GetDeliveryByCustomerId();
                    break;
                case 7:
                    DeleteDelivery();
                    break;
                case 00:
                    isRunning = Quit();
                    break;
                default:
                    WriteLine("Invalid Selection");
                    break;
            }
        }
    }

    private void SeedDeliveries()
    {
        Clear();

        Delivery deliveryA = new Delivery(new DateOnly(2023, 09, 26), new DateOnly(2023, 09, 27), 111111, 50, DeliveryStatus.EnRoute, 1);
        Delivery deliveryB = new Delivery(new DateOnly(2023, 09, 27), new DateOnly(2023, 09, 28), 222222, 51, DeliveryStatus.EnRoute, 2);
        Delivery deliveryC = new Delivery(new DateOnly(2023, 09, 21), new DateOnly(2023, 09, 22), 333333, 52, DeliveryStatus.Scheduled, 3);
        Delivery deliveryD = new Delivery(new DateOnly(2023, 07, 17), new DateOnly(2023, 07, 20), 444444, 53, DeliveryStatus.Complete, 4);
        Delivery deliveryF = new Delivery(new DateOnly(2023, 08, 18), new DateOnly(2023, 08, 21), 666666, 54, DeliveryStatus.Complete, 5);
        Delivery deliveryE = new Delivery(new DateOnly(2023, 07, 01), new DateOnly(2023, 07, 02), 555555, 55, DeliveryStatus.Cancelled, 6);

        _devRepo.AddDelivery(deliveryA);
        _devRepo.AddDelivery(deliveryB);
        _devRepo.AddDelivery(deliveryC);
        _devRepo.AddDelivery(deliveryD);
        _devRepo.AddDelivery(deliveryE);
        _devRepo.AddDelivery(deliveryF);
    }

    private void AddDelivery()
    {
        Clear();
        WriteLine("Add New Delivery");
        WriteLine("Enter Order Date (mm/dd/yyyy):");
        if (DateOnly.TryParse(ReadLine(), out DateOnly orderDate))
        {
            WriteLine("Enter Delivery Date (mm/dd/yyyy):");
            if (DateOnly.TryParse(ReadLine(), out DateOnly deliveryDate))
            {
                WriteLine("Enter Item Number:");
                if (int.TryParse(ReadLine(), out int itemNumber))
                {
                    WriteLine("Enter Item Quantity:");
                    if (int.TryParse(ReadLine(), out int itemQuantity))
                    {
                        WriteLine("Enter Customer ID:");
                        if (int.TryParse(ReadLine(), out int customerId))
                        {
                            WriteLine("Enter Status:\n" +
                                      "1. Scheduled\n" +
                                      "2. EnRoute\n" +
                                      "3. Complete\n" +
                                      "4. Cancelled");
                            if (int.TryParse(ReadLine(), out int statusInput) &&
                                Enum.IsDefined(typeof(DeliveryStatus), statusInput))
                            {
                                var deliveryStatus = (DeliveryStatus)statusInput;
                                var newDelivery = new Delivery(orderDate, deliveryDate, itemNumber, itemQuantity, deliveryStatus, customerId);
                                if (_devRepo.AddDelivery(newDelivery))
                                {
                                    WriteLine("Delivery added");
                                }
                                else
                                {
                                    WriteLine("Delivery addition failed");
                                }
                            }
                            else
                            {
                                WriteLine("Invalid status");
                            }
                        }
                        else
                        {
                            WriteLine("Invalid customer Id");
                        }
                    }
                    else
                    {
                        WriteLine("Invalid item quantity");
                    }
                }
                else
                {
                    WriteLine("Invalid item number");
                }
            }
            else
            {
                WriteLine("Invalid delivery date");
            }
        }
        else
        {
            WriteLine("Invalid order date");
        }
        PressAnyKeyToContinue();
    }

    private void DeleteDelivery()
    {
        Clear();
        WriteLine("Delete Delivery");
        WriteLine("Enter ID");
        var userInput = int.Parse(ReadLine()!);
        bool isDeleted = _devRepo.DeleteDelivery(userInput);
        if (isDeleted)
        {
            WriteLine("Delivery Deleted");
        }
        else
        {
            WriteLine("Deletion Failed");
        }
        PressAnyKeyToContinue();
    }

    private void UpdateDeliveryStatus()
    {
        Clear();

        WriteLine("Update Status");
        WriteLine("Enter ID");
        int userInputDeliveryId = int.Parse(ReadLine()!);
        Delivery deliveryInDb = _devRepo.GetDeliveryById(userInputDeliveryId);
        if (deliveryInDb != null)
        {
            WriteLine("Enter Status:\n" +
                    "1. Scheduled\n" +
                    "2. EnRoute\n" +
                    "3. Complete\n" +
                    "4. Cancelled");
            if (int.TryParse(ReadLine(), out int userInputStatusId))
            {
                DeliveryStatus status;
                switch (userInputStatusId)
                {
                    case 1:
                        status = DeliveryStatus.Scheduled;
                        break;
                    case 2:
                        status = DeliveryStatus.EnRoute;
                        break;
                    case 3:
                        status = DeliveryStatus.Complete;
                        break;
                    case 4:
                        status = DeliveryStatus.Cancelled;
                        break;
                    default:
                        WriteLine("Invalid Selection");
                        PressAnyKeyToContinue();
                        return;
                }

                deliveryInDb.DeliveryStatus = status;
                _devRepo.UpdateStatus(deliveryInDb.Id, status);
                WriteLine($"Delivery updated!");
            }
            else
            {
                WriteLine("Invalid Id");
            }
        }
        else
        {
            WriteLine($"NOT FOUND");
        }
        PressAnyKeyToContinue();
    }

    private void GetDeliveryByCustomerId()
    {
        Clear();
        WriteLine("Get Delivery By Id");
        WriteLine("Enter Customer Id");
        var userInput = int.Parse(ReadLine()!);
        Delivery delivery = _devRepo.GetDeliveryByCustomerId(userInput);
        WriteLine(
                               $"====================================================================================\n" +
                               $"| Delivery ID: {delivery.ItemNumber} | Customer ID: {delivery.CustomerId}\n" +
                               $"| Order Date: {delivery.OrderDate}   | Delivery Date: {delivery.DeliveryDate}\n" +
                               $"| Quantity: {delivery.ItemQuantity}  | Delivery Status: {delivery.DeliveryStatus}\n" +
                               $"===================================================================================="
            );
        PressAnyKeyToContinue();
    }

    private void GetAllCompletedDeliveries()
    {
        Clear();
        WriteLine("All Completed Deliveries");
        List<Delivery> completedDeliveries = _devRepo.GetCompletedDeliveries();
        foreach (var delivery in completedDeliveries)
        {
            WriteLine(
                               $"====================================================================================\n" +
                               $"| Delivery ID: {delivery.ItemNumber} | Customer ID: {delivery.CustomerId}\n" +
                               $"| Order Date: {delivery.OrderDate}   | Delivery Date: {delivery.DeliveryDate}\n" +
                               $"| Quantity: {delivery.ItemQuantity}  | Delivery Status: {delivery.DeliveryStatus}\n" +
                               $"===================================================================================="
            );
        }
        PressAnyKeyToContinue();
    }

    private void GetAllEnrouteDeliveries()
    {
        Clear();
        WriteLine("All EnRoute Deliveries");
        List<Delivery> enrouteDeliveries = _devRepo.GetEnRouteDeliveries();
        foreach (var delivery in enrouteDeliveries)
        {
            WriteLine(
                               $"====================================================================================\n" +
                               $"| Delivery ID: {delivery.ItemNumber} | Customer ID: {delivery.CustomerId}\n" +
                               $"| Order Date: {delivery.OrderDate}   | Delivery Date: {delivery.DeliveryDate}\n" +
                               $"| Quantity: {delivery.ItemQuantity}  | Delivery Status: {delivery.DeliveryStatus}\n" +
                               $"===================================================================================="
            );
        }
        PressAnyKeyToContinue();
    }

    private void GetAllDeliveries()
    {
        Clear();
        WriteLine("All Deliveries");

        List<Delivery> deliveries = _devRepo.GetDeliveries();
        foreach (var delivery in deliveries)
        {
            WriteLine(
                               $"====================================================================================\n" +
                               $"| Delivery ID: {delivery.ItemNumber} | Customer ID: {delivery.CustomerId}\n" +
                               $"| Order Date: {delivery.OrderDate}   | Delivery Date: {delivery.DeliveryDate}\n" +
                               $"| Quantity: {delivery.ItemQuantity}  | Delivery Status: {delivery.DeliveryStatus}\n" +
                               $"===================================================================================="
            );
        }
        PressAnyKeyToContinue();
    }

    private bool Quit()
    {
        Clear();
        WriteLine("Have a good day!");
        ReadKey();
        return false;
    }

    private void PressAnyKeyToContinue()
    {
        WriteLine("Press any key to continue");
        ReadKey();
    }
}
