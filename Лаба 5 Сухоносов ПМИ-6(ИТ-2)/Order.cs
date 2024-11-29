public class Order
{
    public int OrderCode { get; set; }
    public int ServiceCode { get; set; }
    public int PerformerCode { get; set; }
    public decimal Cost { get; set; }

    public Order(int orderCode, int serviceCode, int performerCode, decimal cost)
    {
        OrderCode = orderCode;
        ServiceCode = serviceCode;
        PerformerCode = performerCode;
        Cost = cost;
    }

    public override string ToString()
    {
        return $"Код заказа: {OrderCode}, Код услуги: {ServiceCode}, Код исполнителя: {PerformerCode}, Стоимость: {Cost}";
    }
}
