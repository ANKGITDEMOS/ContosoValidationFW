using System.Collections.Concurrent;
using System.Collections.Generic;


namespace ContosoValidationFW.Interfaces
{
    public interface IValidateContext
    {
        BlockingCollection<ValidationResult> ResultCollection { get; }

        Dictionary<string, object> PropertyBag { get; }
    }
}