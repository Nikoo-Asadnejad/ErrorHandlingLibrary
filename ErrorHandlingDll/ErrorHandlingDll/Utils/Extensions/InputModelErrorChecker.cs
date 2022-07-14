using ErrorHandlingDll.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ErrorHandlingDll.Extensions;
public static class ModelErrorCheckerExtensions
{
  public static List<FieldErrorsModel> GetModelErrors(this ModelStateDictionary modelState)
  {
    List<FieldErrorsModel> fieldErrors = new();

    var errors = modelState.Where(x => x.Value.Errors.Any())
      .Select(x => new { key = x.Key, errors = x.Value.Errors });

    foreach (var error in errors)
    {
      var fieldKey = error.key;
      fieldErrors.AddRange(error.errors
                         .Select(error => new FieldErrorsModel(fieldKey, error.ErrorMessage)).ToList());
    }

    return fieldErrors;
  }
}


