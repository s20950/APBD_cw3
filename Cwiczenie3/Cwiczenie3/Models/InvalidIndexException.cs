﻿namespace Cwiczenie3.Models
{
    public class InvalidIndexException : Exception
    {
        public InvalidIndexException()
        {
        }
        public InvalidIndexException(string message) : base(message)
        {
        }
        public InvalidIndexException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
