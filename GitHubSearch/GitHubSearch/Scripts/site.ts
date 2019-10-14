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

    export class Search {

        private _element: JQuery;
        private _nameInput: JQuery;
        private _searchButton: JQuery;
        private _resultContainer: JQuery;
        private _results: JQuery;
        private _ajax: Ajax;
        private _dialog: JQuery;

        constructor() {
            this._init();
        }

        private _init(): void {
            this._element = $('#GitHubSearch');
            this._nameInput = $('#nameInput');
            this._dialog = $("#dialog");

            this._searchButton = this._element.find('.search-button');
            this._resultContainer = this._element.find('.results-container');
            this._results = this._element.find('.results');
            this._ajax = new Ajax();
            this._setSearchEvents();

            // requires adding JQuery UI
            this._dialog.dialog({
                autoOpen: false, modal: true, show: "blind", hide: "blind"
            });
        }

        private _messageBox(message: string): void {
            // requires adding JQuery UI
            this._dialog.find('.dialog-message').text(message);
            this._dialog.dialog("open"); 
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

            if (name == null) {
                this._messageBox("Name cannot be empty");
                return false;
            }

            if (name.length == 0) {
                this._messageBox("Name cannot be empty");
                return false;
            }

            // validate using REGEX
            // Type: RegExp (/^[a-z\d](?:[a-z\d]|-(?=[a-z\d])){0,38}$/i)

            // only alphanumeric characters or hyphens

            // cannot have multiple consecutive hyphens

            // name can't start or end with a hypen

            // Maximum is 39 characters
            if (name.length > 38) {
                this._messageBox("Name must be less tha 39 characters");
                return false;
            }

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
                // notify user foreach validationResult in response.validationResults
                // messageBox("message")
            }

            if (response.success) {
                this._showResults(response.view)
            }
        }

        private _showResults(view: string): void {
            this._resultContainer.removeClass('hidden');
            this._results.empty();
            this._results.append(view);
        }
    }
}