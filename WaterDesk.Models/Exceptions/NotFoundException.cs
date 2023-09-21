﻿namespace WaterDesk.Models.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Not Found")
        {
        }
        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}