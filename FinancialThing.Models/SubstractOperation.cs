﻿namespace FinancialThing.Models
{
    public class SubstractOperation: Operation
    {
        private Operation _first;
        private Operation _second;

        public SubstractOperation(Operation first, Operation second)
        {
            _first = first;
            _second = second;
        }

        public override Operation Execute()
        {
            var result = new Operation();
            result.Value = _first.Value - _second.Value;
            return result;
        }
    }
}