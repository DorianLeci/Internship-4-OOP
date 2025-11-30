namespace Internship_4_OOP.Domain.Common.Exceptions;

public class UnauthenticatedException:Exception
{
    public UnauthenticatedException() : base(){}
    public UnauthenticatedException(string message) : base(message){}    
}