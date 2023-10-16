using System;
namespace MVM.Estoque.Api.Exceptions;

public class DomainException : Exception
{
    public string? Key { get; private set; }

    public DomainException(string key, string message) : base(message)
    {
        Key = key;
    }

    public DomainException(string message) : base(message)
    {
        Key = null;
    }
}

