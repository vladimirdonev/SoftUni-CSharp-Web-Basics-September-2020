using Git.Services;
using Git.ViewModels.RepositoryViewModel;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        [HttpGet]
        public HttpResponse All()
        {
            var repositories = this.repositoriesService.GetAll();
            return this.View(repositories);
        }

        [HttpGet]
        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateRepositoryViewModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (string.IsNullOrEmpty(input.Name) || string.IsNullOrWhiteSpace(input.Name) || input.Name.Length < 3 || input.Name.Length > 10)
            {
                return this.Error("Name Is Required");
            }
            if (string.IsNullOrEmpty(input.repositoryType) || string.IsNullOrWhiteSpace(input.repositoryType))
            {
                return this.Error("repositoryType Is Required");
            }

            var userid = this.GetUserId();
            this.repositoriesService.CreateRepository(input, userid);
            return this.Redirect("/Repositories/All");
        }
    }
}
