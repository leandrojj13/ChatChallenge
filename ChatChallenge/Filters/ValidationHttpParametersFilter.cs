using ChatChallenge.Bl.Validators.Generic;
using ChatChallenge.Controllers;
using ChatChallenge.Models;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatChallenge.Filters
{
    public class ValidationHttpParametersFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as IBaseController;
            if (controller != null)
            {
                var _validationFactory = controller._validationFactory;
                var parameters = context.ActionDescriptor.Parameters;
                foreach (var parameter in parameters)
                {
                    if (parameter.ParameterType.IsPrimitive) continue;
                    var contains = context.ActionArguments.ContainsKey(parameter.Name);
                    if (contains)
                    {
                        var parmeterValue = context.ActionArguments[parameter.Name];
                        var validator = _validationFactory.GetValidator(parameter.ParameterType);
                        if (validator != null)
                        {
                            var resultSet = ((IBaseValidator)validator).GetRuleSetName(parmeterValue);
                            var valueObject = parmeterValue ?? Activator.CreateInstance(parameter.ParameterType);
                            var validationResult = validator.Validate(new ValidationContext<object>(valueObject, new PropertyChain(), new RulesetValidatorSelector(resultSet)));

                            if (!validationResult.IsValid)
                            {
                                var result = controller.UnprocessableEntity(validationResult.Errors.Select(e => new ValidationFailedModel(e)));
                                context.Result = result;
                            }
                        }
                    }


                }
            }

            base.OnActionExecuting(context);
        }

    }
}
