namespace RRP3.Exceptions;

public abstract class RRP3Exceptions : Exception
{
    public RRP3Exceptions(string message) : base(message) { }
}

internal class RRP3InvalidEnumException : RRP3Exceptions
{
    public RRP3InvalidEnumException(string message = "Enum is wrong") : base(message) { }
}
