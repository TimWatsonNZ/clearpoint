using System;

namespace TodoList.Api.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string message) : base(message)
        {
            
        }
    }
}
