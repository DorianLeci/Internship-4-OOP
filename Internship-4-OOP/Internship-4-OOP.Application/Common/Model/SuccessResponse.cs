namespace Internship_4_OOP.Application.Common.Model;

public class SuccessResponse(bool isSuccess)
{
    public bool IsSuccess { get; set; } = isSuccess;
}