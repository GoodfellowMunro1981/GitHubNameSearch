using System.Collections.Generic;
using System.Linq;

namespace GitHubSearch
{
    public static class ValidationResults
    {
        public static bool AnyErrorOrInvalid(IEnumerable<ValidationResult> validationResults)
        {
            return validationResults.Any(x => x.Level == ValidationLevel.Error || x.Level == ValidationLevel.Invalid);
        }
    }
}