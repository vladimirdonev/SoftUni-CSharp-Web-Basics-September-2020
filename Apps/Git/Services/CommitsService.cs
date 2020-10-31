using Git.ViewModels.Commits;
using Git.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateCommit(string userid, CreateCommitViewModel input)
        {
            var Commit = new Commit
            {
                CreatorId = userid,
                CreatedOn = DateTime.UtcNow,
                Description = input.Description,
                RepositoryId = input.Id,
            };
            this.db.Commits.Add(Commit);
            this.db.SaveChanges();
        }

        public void Delete(string commitid)
        {
            var commit = this.db.Commits.Find(commitid);
            this.db.Commits.Remove(commit);
            this.db.SaveChanges();
        }

        public IEnumerable<CommitsAllViewModel> GetAll(string userid)
        {
            var commits = this.db.Commits.Where(x => x.CreatorId == userid).Select(x => new CommitsAllViewModel
            {
                Id = x.Id,
                CreatedOn = x.CreatedOn.ToString("yyyy/MM/dd H:mm"),
                Description = x.Description,
                RepositoryName = x.Repository.Name,
            }).ToList();
            return commits;
        }
    }
}
