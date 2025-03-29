namespace HashTable
{
    using System;

    class DuplicateKeyException : ArgumentException
    {
        public DuplicateKeyException(string message, string paramName)
            : base(message, paramName)
        {

        }
    }
}
