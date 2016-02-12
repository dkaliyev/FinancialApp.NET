using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using NCalc;

namespace FinancialThing.Utilities
{
    public class RatioEvaluator
    {
        private IDatabaseRepository<Ratio, Guid> _ratioRepo;
        private Dictionary<string, Expression> _expressions;

        public RatioEvaluator(IDatabaseRepository<Ratio, Guid> ratioRepo)
        {
            _ratioRepo = ratioRepo;
            _expressions = new Dictionary<string, Expression>();
        }


        public float Evaluate(IEnumerable<Data> data, Ratio ratio)
        {
            //if (!_expressions.ContainsKey(ratio.Code))
            //{
                var exp = new Expression(ratio.Formula);
                exp.EvaluateParameter += delegate(string name, ParameterArgs args)
                {
                    var value = data.First(d => d.Dictionary.Code == name).Values.ToList();
                    args.Result = value;
                };

                exp.EvaluateFunction += delegate(string name, FunctionArgs args)
                {
                    if (name == "Get")
                    {
                        var flag = (int)args.Parameters[1].Evaluate();
                        if (flag == 0)
                        {
                            var vals = args.Parameters[0].Evaluate() as List<Value>;
                            var val = vals.OrderBy(v => v.Year).Last().DataValue;
                            if (val == "--")
                            {
                                throw new InvalidOperationException();
                            }
                            args.Result = Convert.ToSingle(val);
                        }
                        else
                        {
                            var vals = args.Parameters[0].Evaluate() as List<Value>;
                            args.Result = Convert.ToSingle(vals.Average(v =>v.DataValue == "--" ? 0 : Convert.ToSingle(v.DataValue)));
                        }
                    }
                };
                //_expressions.Add(ratio.Code, exp);
            //}
            
            //var e = _expressions[ratio.Code];

                return Convert.ToSingle(exp.Evaluate());
        }
    }
}