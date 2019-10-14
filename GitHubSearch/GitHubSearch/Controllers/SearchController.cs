using System.Web.Mvc;

namespace GitHubSearch.Controllers
{
    public class SearchController : Controller
    {
        #region Public Methods
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchName(string name)
        {
            var validationResults = new ValidationResultList();
            var success = false;
            var view = default(string);

            if(ValidateName(name, validationResults))
            {
                var viewModel = GitHubService.SearchByName(name, validationResults);

                if (!ValidationResults.AnyErrorOrInvalid(validationResults))
                {
                    success = true;
                    view = RazorViewToString.RenderRazorViewToString(this, "~/Views/Results/_results.cshtml", viewModel);
                }
            }

            return Json(new
            {
                success,
                view,
                validationResults
            });
        }
        #endregion

        #region Private Helpers
        private bool ValidateName(string name, IValidationResultList validationResults)
        {
            if (string.IsNullOrEmpty(name))
            {
                validationResults.Add(new ValidationResult
                {
                    Level = ValidationLevel.Invalid,
                    Message = "Name must be valid"
                });

                return false;
            }

            return true;
        }
        #endregion
    }
}