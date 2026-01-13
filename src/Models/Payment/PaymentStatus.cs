namespace E_Commerce.Models.Payments;

public enum PaymentStatus
{
    Pending,    // Payment initiated, not finished
    Succeeded,  // Money received
    Failed,     // Payment attempt failed
    Refunded    // Money returned
}

// Pending → Succeeded
//    ↓
//  Failed

// Succeeded → Refunded

