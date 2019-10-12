var GitHubSearch;
(function (GitHubSearch) {
    var ValidationLevel;
    (function (ValidationLevel) {
        ValidationLevel[ValidationLevel["Warning"] = 0] = "Warning";
        ValidationLevel[ValidationLevel["Error"] = 1] = "Error";
        ValidationLevel[ValidationLevel["Invalid"] = 2] = "Invalid";
    })(ValidationLevel = GitHubSearch.ValidationLevel || (GitHubSearch.ValidationLevel = {}));
    function validationResultsAnyErrorOrInvalid(validationResults) {
        if (validationResults.length > 0) {
            for (var _i = 0, validationResults_1 = validationResults; _i < validationResults_1.length; _i++) {
                var validationResult = validationResults_1[_i];
                if (validationResult.level == ValidationLevel.Error || validationResult.level == ValidationLevel.Invalid) {
                    return true;
                }
            }
        }
        return false;
    }
    GitHubSearch.validationResultsAnyErrorOrInvalid = validationResultsAnyErrorOrInvalid;
})(GitHubSearch || (GitHubSearch = {}));
//# sourceMappingURL=validationResult.js.map