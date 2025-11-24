using Internship_4_OOP.Application.Common.Model;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Application.Common.Users.User;

public class CreateUserRequest
{
    public int Id{get; set;}
    public string Name{get; set;}
    public string Username{get; set;}
    public string Email{get; set;}
    public string AddressStreet{get; set;}
    public string AddressCity{get; set;}
    public decimal GeoLatitude{get; set;}
    public decimal GeoLongitude{get; set;}
    public string? Website;
    private string _password = Guid.NewGuid().ToString();
    public bool IsActive = true;
}
internal class CreateUserRequestHandler : RequestHandler<CreateUserRequest,SuccessPostResponse>
{
    private readonly IUserUnitOfWork _unitOfWork;
    protected override async Task<Result<SuccessPostResponse>> HandleRequest(CreateUserRequest request,
        Result<SuccessPostResponse> result)
    {
        var user=new Domain.Entities.Users.User(request.Id,request.Name,request.Username,request.Email,request.AddressStreet,request.AddressCity,request.GeoLatitude,request.GeoLongitude,
            request.Website);
        var validationResult = await user.Create(_unitOfWork.Repository);
        result.SetValidationResult(validationResult.ValidationResult);

        if (result.HasErrors)
            return result;

        await _unitOfWork.SaveAsync();

        result.SetResult(new SuccessPostResponse(user.Id));
        return result;
    }  
    protected override Task<bool> IsAuthorized()
    {
        return Task.FromResult(true);
    }
}