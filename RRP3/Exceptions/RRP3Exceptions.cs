namespace RRP3.Exceptions;

public abstract class RRP3Exceptions : Exception
{
    public RRP3Exceptions(string message = "Un-corrected exception.") : base(message) { }
}
