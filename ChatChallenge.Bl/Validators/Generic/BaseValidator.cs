using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Bl.Validators.Generic
{
    public abstract class BaseValidator<T> : AbstractValidator<T>, IBaseValidator
    {

        /// <summary>
        /// This method is responsible for deciding which RuleSet will be executed, based on an evaluation of the submitted object
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual string[] GetRuleSetName(object dto)
        {
            return new string[] { "default" };
        }
    }
}
