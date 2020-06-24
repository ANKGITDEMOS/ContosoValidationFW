
using ContosoValidationFW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoValidationFW
{
    public class ValidationResult
    {
        private IValidationNode _node;

        public ValidationResult(IValidateContext context, IValidationNode validateNode)
        {
            _node = validateNode;
            context.ResultCollection.Add(this);
            this.ValidationCode = validateNode.ValidationCode;
            this.ParentValidationCode = validateNode.ParentValidationCode;
        }

        public IValidationNode Node { get { return _node; } }

        /// <summary>
        /// Validation Code for current Validation.
        /// </summary>
        public string ValidationCode { get; private set; }

        public List<string> ValidationMessages { get; } = new List<string>();

        /// <summary>
        /// Parent Validation Code - If parent is passed then only child will be executed.
        /// </summary>
        public string ParentValidationCode { get; private set; }

        
    }
}
