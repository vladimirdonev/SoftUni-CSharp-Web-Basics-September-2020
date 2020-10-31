using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }

        [HttpGet]
        public HttpResponse Create(CreateCommitViewModel viewModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View(viewModel);
        }
        [HttpPost("/Commits/Create")]
        public HttpResponse DoCreate(CreateCommitViewModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if(string.IsNullOrEmpty(input.Description) || string.IsNullOrWhiteSpace(input.Description) ||input.Description.Length < 5)
            {
                return this.Error("Description Is Required");
            }
            var userid = this.GetUserId();
            this.commitsService.CreateCommit(userid, input);
            return this.Redirect("/Repositories/All");
        }

        [HttpGet]
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var userid = this.GetUserId();
            var commits = this.commitsService.GetAll(userid);
            return this.View(commits);
        }

        [HttpGet]
        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            this.commitsService.Delete(id);
            return this.Redirect("/Commits/All");
        }
    }
}
