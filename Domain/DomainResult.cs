namespace Domain;

/// <summary>
/// Domain result reason
/// </summary>
public enum DomainResultReason
{
    Unknown = 0,
    Invalid = 1,
    Valid = 2,
    ValidationFailed = 3
}

/// <summary>
/// Domain Result, used for domain crud action in a DDD context
/// </summary>
/// <param name="reason">Reason for the domain result</param>
/// <param name="message">Message for detailed messaging</param>
public sealed class DomainResult(DomainResultReason reason, string message)
{
    public DomainResultReason Reason { get; private set; } = reason;
    public string Message { get; private set; } = message;

    /// <summary>
    /// Indication whether the domain result valid
    /// </summary>
    public bool IsValid => Reason == DomainResultReason.Valid && string.IsNullOrEmpty(Message);
        
    /// <summary>
    /// Indication whether the domain result is invalid
    /// </summary>
    public bool IsInvalid => !IsValid;
}