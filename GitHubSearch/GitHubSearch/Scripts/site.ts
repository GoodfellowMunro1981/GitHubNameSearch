namespace GitHubSearch {

    var search: Search;

    $(document).ready(function () {
        search = new Search();
    });

    interface SearchResponse {
        success: boolean,
        validationResults: ValidationResult[],
        view: string,
    }

    // move this out!!
    export async function messageBox(message: string): Promise<void> {
        
        // use JQuery dialog
    } 

    export class Search {

        private _element: JQuery;
        private _nameInput: JQuery;
        private _searchButton: JQuery;
        private _resultContainer: JQuery;
        private _results: JQuery;
        private _ajax: Ajax;

        constructor() {
            this._init();
        }

        private _init(): void {
            this._element = $('#GitHubSearch');
            this._nameInput = $('#nameInput');
            this._searchButton = this._element.find('.search-button');
            this._resultContainer = this._element.find('.results-container');
            this._results = this._element.find('.results');
            this._ajax = new Ajax();
            this._setSearchEvents();
        }

        private _setSearchEvents(): void {

            this._searchButton.click((e: JQueryEventObject) => {

                var name = this._nameInput.val();

                if (this._validateInput(name)) {
                    this._searchName(name);
                }
            });
        }

        private _validateInput(name: string): boolean {

            return true;
        }

        private async _searchName(name: string): Promise<void> {

            let ajaxOptions: AjaxOptions = {
                url: "Search/SearchName",
                data: {
                    name: name
                }
            };

            let response: SearchResponse = await this._ajax.ajax(ajaxOptions);

            if (validationResultsAnyErrorOrInvalid(response.validationResults)) {
                // notify user

                // messageBox("message")
            }

            if (response.success) {

                this._showResults(response.view)

                // show results
            }
        }

        private _showResults(view: string): void {
            this._resultContainer.removeClass('hidden');
            this._results.empty();
            this._results.append(view);
        }
    }
}