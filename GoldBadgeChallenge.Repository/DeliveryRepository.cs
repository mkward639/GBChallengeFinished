public class DeliveryRepository
{
    private readonly List<Delivery> _deliveryDb = new List<Delivery>();

    public bool AddDelivery(Delivery delivery)
    {
        if (delivery.ItemQuantity <= 0)
        {
            return false;
        }
        else
        {
            _deliveryDb.Add(delivery);
            System.Console.WriteLine("Delivery Successful");
            return true;
        }
    }

    public List<Delivery> GetDeliveries()
    {
        return _deliveryDb;
    }

    public Delivery GetDeliveryByCustomerId(int customerId)
    {
        return _deliveryDb.SingleOrDefault(d => d.CustomerId == customerId);
    }

    public List<Delivery> GetEnRouteDeliveries()
    {
        return _deliveryDb.Where(d => d.DeliveryStatus == DeliveryStatus.EnRoute).ToList();
    }

    public List<Delivery> GetCompletedDeliveries()
    {
        return _deliveryDb.Where(d => d.DeliveryStatus == DeliveryStatus.Complete).ToList();
    }

    public Delivery GetDeliveryById(int id)
    {
        return _deliveryDb.SingleOrDefault(d => d.Id == id);
    }

    public bool UpdateStatus(int deliveryId, DeliveryStatus currentStatus)
    {
        Delivery deliveryInDb = GetDeliveryById(deliveryId);
        if (deliveryInDb != null)
        {
            deliveryInDb.DeliveryStatus = currentStatus;
            return true;
        }
        return false;
    }

    public bool DeleteDelivery(int customerId)
    {
        var deliveryToRemove = GetDeliveryByCustomerId(customerId);
        if (deliveryToRemove != null)
        {
            _deliveryDb.Remove(deliveryToRemove);
            return true;
        }
        return false;
    }
}
