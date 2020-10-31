using Git.ViewModels.RepositoryViewModel;
using System;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        IEnumerable<RepositoriesAllViewModel> GetAll();

        void CreateRepository(CreateRepositoryViewModel input,string userId);

        string GetRepositoryName(string id);
    }
}
