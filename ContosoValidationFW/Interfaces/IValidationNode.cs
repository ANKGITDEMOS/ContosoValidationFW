using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;


namespace ContosoValidationFW.Interfaces
{
    
 
    public interface IValidationNode
    {
        string ValidationCode { get; set; } //EDS_RAS_VAL1 - RAS.Validation.EDS_RAS_VAL1 

        bool ValidateParent { get; set; }

        string ParentValidationCode { get; set; }

        bool Validate(IValidateContext context);

        IValidationNode ParentValidationNode { get; set; }

    }

    
}