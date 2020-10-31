using Git.ViewModels.Commits;
using System.Collections.Generic;

namespace Git.Services
{
    public interface ICommitsService
    {

        IEnumerable<CommitsAllViewModel> GetAll(string userid);

        void CreateCommit(string userid,CreateCommitViewModel input);

        void Delete(string commitid);

    }
}
