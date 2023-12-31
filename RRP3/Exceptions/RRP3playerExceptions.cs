﻿namespace RRP3.Exceptions;

internal abstract class RRP3PlayerExceptions : RRP3Exceptions
{
    public RRP3PlayerExceptions(string message) : base(message) { }
}

internal class RRP3IndexOutOfBorderException : RRP3PlayerExceptions
{
    public RRP3IndexOutOfBorderException(string message = "Index Out Of Border Exception") : base(message) { }
}

internal class RRP3PlayerInvalidHitPointsException : RRP3PlayerExceptions
{
    public RRP3PlayerInvalidHitPointsException(string message = "HP shoul be at least 1.") : base(message) { }
}
