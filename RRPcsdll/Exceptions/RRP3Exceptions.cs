namespace RRPcsdll.Exceptions;

internal abstract class RRP3Exceptions(string message) : Exception(message) { }

internal class RRP3GameOverException(string message = "Game is over.") : RRP3Exceptions(message) { }