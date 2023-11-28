using KristofferStrube.Blazor.Streams;

namespace KristofferStrube.Blazor.ServiceWorker;

public class BodyInit
{
    public readonly object value;

    public BodyInit(ReadableStreamProxy value)
    {
        this.value = value;
    }

    public static implicit operator BodyInit(ReadableStreamProxy value)
    {
        return new(value);
    }

    public static implicit operator string(BodyInit bi)
    {
        return bi.value switch
        {
            ReadableStreamProxy value => value.Id.ToString(),
            _ => throw new InvalidCastException("Constructed with wrong type.")
        };
    }
}

