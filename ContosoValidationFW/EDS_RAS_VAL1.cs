
using ContosoValidationFW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoValidationFW
{
    public class EDS_RAS_VAL1 : IValidationNode
    {
        public EDS_RAS_VAL1()
        {
            this.ValidationCode = this.ToString();
        }

        public string ValidationCode { get; set; }
        public bool ValidateParent { get; set; }
        public string ParentValidationCode { get; set; }
        public IValidationNode ParentValidationNode { get; set; }

        public bool Validate(IValidateContext context)
        {
            Thread.Sleep(1000);
            var result = new ValidationResult(context, this);
            result.ValidationMessages.Add(string.Format("{0} executed at {1} with parent as {2}", this.ValidationCode, DateTime.Now.ToLongDateString() + ":" + DateTime.Now.ToLongTimeString(), this.ParentValidationCode));
            return 1 == 1;
        }
    }


    public class EDS_RAS_VAL2 : IValidationNode
    {
        public EDS_RAS_VAL2()
        {
            this.ValidationCode = this.ToString();
        }

        public string ValidationCode { get; set; }
        public bool ValidateParent { get; set; }
        public string ParentValidationCode { get; set; }
        public IValidationNode ParentValidationNode { get; set; }

        public bool Validate(IValidateContext context)
        {
            Thread.Sleep(1000);

            var result = new ValidationResult(context, this);
            result.ValidationMessages.Add(string.Format("{0} executed at {1} with parent as {2}", this.ValidationCode, DateTime.Now.ToLongDateString() + ":" + DateTime.Now.ToLongTimeString(), this.ParentValidationCode));
            return 1 == 0;
        }
    }

    public class EDS_RAS_VAL3 : IValidationNode
    {
        public EDS_RAS_VAL3()
        {
            this.ValidationCode = this.ToString();
        }

        public string ValidationCode { get; set; }
        public bool ValidateParent { get; set; }
        public string ParentValidationCode { get; set; }
        public IValidationNode ParentValidationNode { get; set; }

        public bool Validate(IValidateContext context)
        {
            Thread.Sleep(1000);

            var result = new ValidationResult(context, this);
            result.ValidationMessages.Add(string.Format("{0} executed at {1} with parent as {2}", this.ValidationCode, DateTime.Now.ToLongDateString() + ":" + DateTime.Now.ToLongTimeString(), this.ParentValidationCode));
            return 1 == 1;
        }
    }


    public class EDS_RAS_VAL4 : IValidationNode
    {
        public EDS_RAS_VAL4()
        {
            this.ValidationCode = this.ToString();
        }

        public string ValidationCode { get; set; }
        public bool ValidateParent { get; set; }
        public string ParentValidationCode { get; set; }
        public IValidationNode ParentValidationNode { get; set; }

        public bool Validate(IValidateContext context)
        {
            Thread.Sleep(1000);

            var result = new ValidationResult(context, this);
            result.ValidationMessages.Add(string.Format("{0} executed at {1} with parent as {2}", this.ValidationCode, DateTime.Now.ToLongDateString() + ":" + DateTime.Now.ToLongTimeString(), this.ParentValidationCode));
            return 1 == 1;
        }
    }

    public class EDS_RAS_VAL5 : IValidationNode
    {
        public EDS_RAS_VAL5()
        {
            this.ValidationCode = this.ToString();
        }

        public string ValidationCode { get; set; }
        public bool ValidateParent { get; set; }
        public string ParentValidationCode { get; set; }
        public IValidationNode ParentValidationNode { get; set; }

        public bool Validate(IValidateContext context)
        {
            Thread.Sleep(1000);

            var result = new ValidationResult(context, this);
            result.ValidationMessages.Add(string.Format("{0} executed at {1} with parent as {2}", this.ValidationCode, DateTime.Now.ToLongDateString() + ":" + DateTime.Now.ToLongTimeString(), this.ParentValidationCode));
            return 1 == 1;
        }
    }


}
