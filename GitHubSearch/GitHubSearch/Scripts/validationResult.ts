namespace GitHubSearch {

    export interface ValidationResult {
        level: ValidationLevel,
        message: string,
    }

    export enum ValidationLevel {
        Warning,
        Error,
        Invalid
    }

    export function validationResultsAnyErrorOrInvalid(validationResults: ValidationResult[]): boolean {

        if (validationResults.length > 0) {

            for (var validationResult of validationResults) {

                if (validationResult.level == ValidationLevel.Error || validationResult.level == ValidationLevel.Invalid) {
                    return true;
                }
            }
        }

        return false;
    }
}