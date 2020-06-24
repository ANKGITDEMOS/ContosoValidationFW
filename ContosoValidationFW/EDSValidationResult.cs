using ContosoValidationFW.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContosoValidationFW
{
    public class EDSValidationResult
    {
        public EDSValidationResult(IValidateContext context)
        {
            Context = context;
        }

        public BlockingCollection<KeyValuePair<IValidationNode, long>> NodeExecutionTimeResult { get; } = new BlockingCollection<KeyValuePair<IValidationNode, long>>();

        public BlockingCollection<KeyValuePair<IValidationNode, bool>> NodeValidateResult { get; } = new BlockingCollection<KeyValuePair<IValidationNode, bool>>();

        public long TotalTimeExecution { get; set; }

        public IValidateContext Context { get; private set; }

        public bool AllValidationPassed
        {
            get
            {
                return !this.NodeValidateResult.Any(y => !y.Value);
            }
        }

        public List<IValidationNode> PassedValidations
        {
            get
            {
                return this.NodeValidateResult.Where(r => r.Value).Select(y => y.Key).ToList();
            }
        }

        public List<IValidationNode> FailedValidations
        {
            get
            {
                return this.NodeValidateResult.Where(r => !r.Value).Select(y => y.Key).ToList();
            }
        }

        public double ExecutionTimeSavedPercentage
        {
            get
            {
                return Math.Round(100 - (Convert.ToDouble(TotalTimeExecution) / Convert.ToDouble(NodeExecutionTimeResult.Sum(y => y.Value)) * 100), 2);
            }
        }

    }
}
