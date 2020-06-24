
using ContosoValidationFW.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoValidationFW
{

    public class ValidateContext : IValidateContext
    {
        public BlockingCollection<ValidationResult> ResultCollection { get; } = new BlockingCollection<ValidationResult>();
        public Dictionary<string, object> PropertyBag { get; } = new Dictionary<string, object>();
    
    }
}
