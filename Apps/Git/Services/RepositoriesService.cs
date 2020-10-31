using Git.Data;
using Git.ViewModels.RepositoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateRepository(CreateRepositoryViewModel input,string userId)
        {
            bool Public = false;
            if(input.repositoryType == "Public")
            {
                Public = true;
            }

            var Repository = new Repository
            {
                Name = input.Name,
                CreatedOn = DateTime.UtcNow,
                IsPublic = Public,
                OwnerId = userId,
            };
            this.db.Repositories.Add(Repository);
            this.db.SaveChanges();
        }

        public IEnumerable<RepositoriesAllViewModel> GetAll()
        {
            var Repositories = this.db.Repositories.Where(x => x.IsPublic == true).Select(x => new RepositoriesAllViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy H:mm:ss"),
                OwnerName = x.Owner.Username,
                CommitsCount = x.Commits.Count(),                
            }).ToList();

            return Repositories;
        }

        public string GetRepositoryName(string id)
        {
            var Repositoryname = this.db.Repositories.FirstOrDefault(x => x.Id == id);

            return Repositoryname.Name;
        }
    }
}
