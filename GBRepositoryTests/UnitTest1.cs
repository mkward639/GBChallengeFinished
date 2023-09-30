using System;
using Xunit;

public class DeliveryRepositoryTests
{
    private readonly DeliveryRepository _deliveryRepository;

    public DeliveryRepositoryTests()
    {
        _deliveryRepository = new DeliveryRepository();
        SeedDeliveries();
    }

    private void SeedDeliveries()
    {
        Delivery deliveryA = new Delivery(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 111111, 50, DeliveryStatus.EnRoute, 1);
        Delivery deliveryB = new Delivery(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 222222, 51, DeliveryStatus.EnRoute, 2);
        Delivery deliveryC = new Delivery(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 333333, 52, DeliveryStatus.Scheduled, 3);
        Delivery deliveryD = new Delivery(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 444444, 53, DeliveryStatus.Complete, 4);
        Delivery deliveryE = new Delivery(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 555555, 54, DeliveryStatus.Scheduled, 5);
        Delivery deliveryF = new Delivery(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 666666, 55, DeliveryStatus.EnRoute, 6);

        _deliveryRepository.AddDelivery(deliveryA);
        _deliveryRepository.AddDelivery(deliveryB);
        _deliveryRepository.AddDelivery(deliveryC);
        _deliveryRepository.AddDelivery(deliveryD);
        _deliveryRepository.AddDelivery(deliveryE);
        _deliveryRepository.AddDelivery(deliveryF);
    }

    [Fact]
    public void AddDelivery_WithValidDelivery_ShouldReturnTrue()
    {
        var delivery = new Delivery(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 12345, 5, DeliveryStatus.Scheduled, 1);
        var result = _deliveryRepository.AddDelivery(delivery);
        Assert.True(result);
    }

    [Fact]
    public void AddDelivery_WithInvalidQuantity_ShouldReturnFalse()
    {
        var delivery = new Delivery(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 12345, 0, DeliveryStatus.Scheduled, 1);
        var result = _deliveryRepository.AddDelivery(delivery);
        Assert.False(result);
    }

    [Fact]
    public void UpdateStatus_WithValidDeliveryId_ShouldUpdateStatus()
    {
        var deliveryId = 1;
        var newStatus = DeliveryStatus.EnRoute;
        var result = _deliveryRepository.UpdateStatus(deliveryId, newStatus);
        Assert.True(result);
        var updatedDelivery = _deliveryRepository.GetDeliveryById(deliveryId);
        Assert.Equal(newStatus, updatedDelivery.DeliveryStatus);
    }

    [Fact]
    public void GetEnRouteDeliveries_ShouldReturnEnRouteDeliveries()
    {
        var enRouteDeliveries = _deliveryRepository.GetEnRouteDeliveries();
        Assert.All(enRouteDeliveries, d => Assert.Equal(DeliveryStatus.EnRoute, d.DeliveryStatus));
    }

    [Fact]
    public void GetDeliveryByCustomerId_WithValidId_ShouldReturnCorrectDelivery()
    {
        var customerId = 1;
        var retrievedDelivery = _deliveryRepository.GetDeliveryByCustomerId(customerId);
        Assert.NotNull(retrievedDelivery);
        Assert.Equal(customerId, retrievedDelivery.CustomerId);
    }
}
