namespace Cwiczenie3.Models
{
    public class DuplicateIndexException : Exception
    {
        public DuplicateIndexException() {
        }
        public DuplicateIndexException(string message) : base(message)
        {
        }
        public DuplicateIndexException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
