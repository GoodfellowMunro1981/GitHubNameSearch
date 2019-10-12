using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GitHubSearch.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchName(string name)
        {
            // validate name

            var validationResults = new List<ValidationResult>();
            var success = true;
            var view = default(string);

            // api call to GitHub Search
            // handle results

            var viewModel = new ResultsModel
            {
                Username = name,
                Avatar = "https://avatars0.githubusercontent.com/u/53115751?s=460&v=4",
                Location = "Newcastle upon Tyne",
                Repos = new List<GitHubRepo> {new GitHubRepo
                {
                    Name = "Test repo",
                    StarGazerCount = 5,

                }}
            };

            view = RazorViewToString.RenderRazorViewToString(this, "~/Views/Results/_results.cshtml", viewModel);

            return Json(new
            {
                success,
                view,
                validationResults
            });
        }
    }
}