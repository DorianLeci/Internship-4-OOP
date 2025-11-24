namespace Internship_4_OOP.Domain.Common.Model;

public class GetByIdRequest
{
    public int Id { get; init; }

    public GetByIdRequest(int id)
    {
        Id = id;
    }
}

public class GetByIdRequest<T>
{
    public T Id { get; init; }

    public GetByIdRequest(T id)
    {
        Id = id;
    }
}