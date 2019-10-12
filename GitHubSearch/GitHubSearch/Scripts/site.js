var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var GitHubSearch;
(function (GitHubSearch) {
    var search;
    $(document).ready(function () {
        search = new Search();
    });
    // move this out!!
    function messageBox(message) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                return [2 /*return*/];
            });
        });
    }
    GitHubSearch.messageBox = messageBox;
    var Search = /** @class */ (function () {
        function Search() {
            this._init();
        }
        Search.prototype._init = function () {
            this._element = $('#GitHubSearch');
            this._nameInput = $('#nameInput');
            this._searchButton = this._element.find('.search-button');
            this._resultContainer = this._element.find('.results-container');
            this._results = this._element.find('.results');
            this._ajax = new GitHubSearch.Ajax();
            this._setSearchEvents();
        };
        Search.prototype._setSearchEvents = function () {
            var _this = this;
            this._searchButton.click(function (e) {
                var name = _this._nameInput.val();
                if (_this._validateInput(name)) {
                    _this._searchName(name);
                }
            });
        };
        Search.prototype._validateInput = function (name) {
            return true;
        };
        Search.prototype._searchName = function (name) {
            return __awaiter(this, void 0, void 0, function () {
                var ajaxOptions, response;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            ajaxOptions = {
                                url: "Search/SearchName",
                                data: {
                                    name: name
                                }
                            };
                            return [4 /*yield*/, this._ajax.ajax(ajaxOptions)];
                        case 1:
                            response = _a.sent();
                            if (GitHubSearch.validationResultsAnyErrorOrInvalid(response.validationResults)) {
                                // notify user
                                // messageBox("message")
                            }
                            if (response.success) {
                                this._showResults(response.view);
                                // show results
                            }
                            return [2 /*return*/];
                    }
                });
            });
        };
        Search.prototype._showResults = function (view) {
            this._resultContainer.removeClass('hidden');
            this._results.empty();
            this._results.append(view);
        };
        return Search;
    }());
    GitHubSearch.Search = Search;
})(GitHubSearch || (GitHubSearch = {}));
//# sourceMappingURL=site.js.map