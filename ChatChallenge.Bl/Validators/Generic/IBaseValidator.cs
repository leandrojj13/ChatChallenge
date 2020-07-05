using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Bl.Validators.Generic
{
    public interface IBaseValidator : IValidator
    {
        /// <summary>
        /// This method is responsible for deciding which RuleSet will be executed, based on an evaluation of the submitted object
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        string[] GetRuleSetName(object dto);
    }
}
