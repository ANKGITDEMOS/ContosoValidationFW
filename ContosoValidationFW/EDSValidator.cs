
using ContosoValidationFW.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContosoValidationFW
{
    public class EDSValidator
    {
        ILogger _logger;

        public EDSValidator(ILogger logger)
        {
            _logger = logger;
        }

 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateJsonConfig">
        /// </param>
        public EDSValidationResult ValidateNodes(string validateJsonConfig)
        {
            try
            {
                IValidateContext context = new ValidateContext();
                EDSValidationResult edsValidationResult = new EDSValidationResult(context);
                //This method will validate tree and see if all nodes are present in domain
                _logger.LogInformation(string.Format("Starting Validation Nodes of nodes with config: /n/n {0}",validateJsonConfig));
                BlockingCollection<IValidationNode> nodes = ValidateAndLoad(validateJsonConfig);
                _logger.LogInformation(string.Format("Fetched {0} nodes for validation.",nodes.Count));
                
                //Fetch from table storage all codes or cache.
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //Read all nodes which Parent Validation code as null, because we can execute them in parallel
                _logger.LogInformation(string.Format("Executing {0} parent nodes for validation.", nodes.Count(y=> string.IsNullOrEmpty(y.ParentValidationCode))));
                Parallel.ForEach(nodes.Where(m => string.IsNullOrEmpty(m.ParentValidationCode)).ToList(), vn =>
                {
                    _logger.LogInformation(string.Format("Start Validating {0} node.",vn.ValidationCode ));
                    Stopwatch swNode = new Stopwatch();
                    swNode.Start();
                    if (vn.Validate(context))
                    { //Only if I am passed
                        edsValidationResult.NodeValidateResult.Add(new KeyValuePair<IValidationNode, bool>(vn, true)) ;
                        edsValidationResult.NodeExecutionTimeResult.Add(new KeyValuePair<IValidationNode, long>(vn,swNode.ElapsedMilliseconds));
                        _logger.LogInformation(string.Format("Passed Validation of {0} node.", vn.ValidationCode));
                        ValidateChilds(nodes, vn, context,edsValidationResult);
                    }
                    else
                    {
                        edsValidationResult.NodeValidateResult.Add(new KeyValuePair<IValidationNode, bool>(vn, false));
                        edsValidationResult.NodeExecutionTimeResult.Add(new KeyValuePair<IValidationNode, long>(vn, swNode.ElapsedMilliseconds));
                        _logger.LogInformation(string.Format("Failed Validation of {0} node.", vn.ValidationCode));
                    }
                    _logger.LogInformation(string.Format("Completed Validation of {0} node and its child.", vn.ValidationCode));
                });
                long timetaken = sw.ElapsedMilliseconds;
                _logger.LogInformation(string.Format("Completed Validation of {0} node and its child in {1} milliseconds.", nodes.Count, timetaken.ToString()));
                edsValidationResult.NodeExecutionTimeResult.CompleteAdding();
                edsValidationResult.TotalTimeExecution = timetaken;
                //sw.Stop();
                //sw = new Stopwatch();
                //sw.Start();
                //context = new ValidateContext();
                ////Read all nodes which Parent Validation code as null, because we can execute them in parallel
                //foreach (var n in nodes)
                //{
                //    n.Validate(context);
                //}
                //string ss = sw.ElapsedMilliseconds.ToString();
                //Console.WriteLine("Total time : " + sw.ElapsedMilliseconds);
                return edsValidationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Validation of Nodes failed. Please contact administrator.");
                //Log the error
                throw new Exception("Failed to validate nodes.", ex);
            }
            
        }


        private static BlockingCollection<IValidationNode> ValidateAndLoad(string validateJsonConfig)
        {

            BlockingCollection<IValidationNode> nodes = new BlockingCollection<IValidationNode>();
            //string json = File.ReadAllText(@"ValidateJSon.json");
            dynamic arr = JsonConvert.DeserializeObject(validateJsonConfig);

            foreach (dynamic item in arr)
            {
                Assembly a = Assembly.Load(item.assembly.ToString());
                Type p = a.GetType(item.code.ToString());
                var obj = Activator.CreateInstance(p);
                if (obj is IValidationNode)
                {
                    ((IValidationNode)obj).ParentValidationCode = item.parentcode;
                    nodes.Add((IValidationNode)obj);
                }
            }
            return nodes;
        }


        private  void ValidateChilds(BlockingCollection<IValidationNode> nodes, IValidationNode parentNode, IValidateContext context,EDSValidationResult edsValidationResult)
        {
            //Now look for our child and validate them.
            Parallel.ForEach(nodes.Where(c => c.ParentValidationCode == parentNode.ValidationCode).ToList(), c =>
            {
                Stopwatch swNode = new Stopwatch();
                swNode.Start();
                c.ParentValidationNode = parentNode;
                _logger.LogInformation(string.Format("Validation of {0} node with parentnode {1} started.", c.ValidationCode,c.ParentValidationCode));
                if (c.Validate(context))
                {
                    edsValidationResult.NodeValidateResult.Add(new KeyValuePair<IValidationNode, bool>(c, true));
                    edsValidationResult.NodeExecutionTimeResult.Add(new KeyValuePair<IValidationNode, long>(c, swNode.ElapsedMilliseconds));
                    _logger.LogInformation(string.Format("Validation of {0} node with parentnode {1} passed.", c.ValidationCode, c.ParentValidationCode));
                    ValidateChilds(nodes, c, context,edsValidationResult);
                }
                else
                {
                    edsValidationResult.NodeValidateResult.Add(new KeyValuePair<IValidationNode, bool>(c, false));
                    edsValidationResult.NodeExecutionTimeResult.Add(new KeyValuePair<IValidationNode, long>(c, swNode.ElapsedMilliseconds));
                    _logger.LogInformation(string.Format("Validation of {0} node with parentnode {1} failed.", c.ValidationCode, c.ParentValidationCode));
                }
                _logger.LogInformation(string.Format("Validation of {0} node with parentnode {1} completed.", c.ValidationCode, c.ParentValidationCode));
            });
        }

    }
}
