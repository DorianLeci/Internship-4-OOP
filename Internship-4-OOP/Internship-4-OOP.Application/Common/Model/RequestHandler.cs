using Internship_4_OOP.Application.Common.Users.User;

namespace Internship_4_OOP.Application.Common.Model;

public abstract class RequestHandler<TRequest,TResult> where TRequest:class where TResult:class
{
    public Guid RequestId { get; set; }

    public async Task<Result<TResult>> ProcessAuthorizedRequestAsync(TRequest request)
    {
        var result = new Result<TResult>();

        if (!await IsAuthorized())
        {
            result.IsAuthorized = false;
            return result;
        }

        await HandleRequest(request, result);
        
        //ako postoji podatak spremamo ga u cache
    }

    protected abstract Task<Result<TResult>> HandleRequest(TRequest request, Result<TResult> result);
    protected abstract Task<bool> IsAuthorized();
    
}