namespace E_Commerce.Models.Orders;

public enum OrderStatus
{
    Pending,    // Created, not paid yet
    Paid,       // Payment completed
    Shipped,    // Sent to customer
    Completed,  // Delivered & closed
    Cancelled   // Cancelled by user/admin
}

// Pending → Paid → Shipped → Completed
//    ↓
// Cancelled
