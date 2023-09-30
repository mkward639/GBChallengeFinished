public class Delivery
{
    public Delivery(DateOnly orderDate, DateOnly deliveryDate, int itemNumber, int itemQuantity, DeliveryStatus deliveryStatus, int customerId)
    {
        OrderDate = orderDate;
        DeliveryDate = deliveryDate;
        ItemNumber = itemNumber;
        ItemQuantity = itemQuantity;
        DeliveryStatus = deliveryStatus;
        CustomerId = customerId;
        Id = customerId;
    }

    public DateOnly OrderDate { get; set; }
    public DateOnly DeliveryDate { get; set; }
    public int ItemNumber { get; set; }
    public int ItemQuantity { get; set; }
    public int CustomerId { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public int Id { get; set; }
}
