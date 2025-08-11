// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

// class for order
using System.Collections.Specialized;

public class Order
{ 
    public int OrderID { get; set; }
    public string PaymentType { get; set; }
    public float Bill { get; set; }
    
}


// Order Repository Interface
public interface IOrderRepository
{
    Order GetOrder(int orderId);

}
// Different types of repositories can be implementing the above interface IOrderRepository
public class SQL_DB : IOrderRepository 
{
    public Order GetOrder(int id)
    {
        // gets order from the SQL DB and then returns it
        Order order = null;
        // Store the order in the above variable order to return it
        return order;
    }
}





// Process Handlers
public interface IPaymentHandler
{
    void validateCredentials();
    void ProcessPayment(Order order);
}

public class CreditCardPaymentHandler : IPaymentHandler
{
    public void validateCredentials()
    {
        // logic for validating whether the card details are valid or not
    }
    public void ProcessPayment(Order order)
    {
        // code logic for the credit card payment
    }
}

public class PayPalPaymentHandler : IPaymentHandler
{
    public void validateCredentials()
    {
        // logic for validating whether the email and password of PayPal account are correct or not.
    }
    public void ProcessPayment(Order order)
    {
        // code logic for processing PayPal payment
    }
}


// Resolver

public interface IPaymentHandlerResolver
{
    IPaymentHandler Resolve(string paymentType);
}

public class PaymentHandlerResolver : IPaymentHandlerResolver
{
    // string stores the payment type in the dictionary 
    // IPayment handler can be a Credit card, PayPal etc. depending on the string (paymentType)
    private readonly Dictionary<string, IPaymentHandler> _handlers;
    public PaymentHandlerResolver(Dictionary<string, IPaymentHandler> handlers)
    {
        _handlers = handlers;
    }
    public IPaymentHandler Resolve(string paymentType)
    {
        return _handlers[paymentType];
    }
}


// Logger Interface

public interface ILogger
{
    void Log(string message);
}

// Logger class implementing the Logger interface
public class Logger:ILogger
{ 
    private readonly string _filePath = "";
    public void Log(string message)
    {
        Console.WriteLine(message);
        File.AppendAllText(_filePath, message + '\n');
    }
}



// Order Processor

public class OrderProcessor
{
    private readonly IOrderRepository _orderRepo;
    private readonly IPaymentHandlerResolver _resolver;
    private readonly ILogger _logger;

    public OrderProcessor(IOrderRepository orderRepo, IPaymentHandlerResolver resolver, ILogger logger)
    {
        _orderRepo = orderRepo;
        _resolver = resolver;
        _logger = logger;
    }

    public void ProcessOrder(int orderId)
    {
        Order order = _orderRepo.GetOrder(orderId);
        var handler = _resolver.Resolve(order.PaymentType);
        handler.ProcessPayment(order);
        _logger.Log("Order processed");
    }
}
