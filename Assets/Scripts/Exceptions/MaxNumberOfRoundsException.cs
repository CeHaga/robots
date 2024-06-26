using System;

[Serializable]
public class MaxNumberOfRoundsException : Exception {
    public MaxNumberOfRoundsException() : base() { }
    public MaxNumberOfRoundsException(string message) : base(message) { }
    public MaxNumberOfRoundsException(string message, Exception inner) : base(message, inner) { }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client.
    protected MaxNumberOfRoundsException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
