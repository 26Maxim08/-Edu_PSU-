
/// <summary>
/// Класс, представляющий заказ
/// </summary>
public class Order
{
    public int OrderCode { get; }
    public int ServiceCode { get; }
    public int PerformerCode { get; }
    public decimal Cost { get; set; }

    public Order(int orderCode, int serviceCode, int performerCode, decimal cost)
    {
        OrderCode = orderCode;
        ServiceCode = serviceCode;
        PerformerCode = performerCode;
        Cost = cost;
    }

    public override string ToString() =>
        $"Код заказа: {OrderCode}, Код услуги: {ServiceCode}, Код исполнителя: {PerformerCode}, Стоимость: {Cost}";
}