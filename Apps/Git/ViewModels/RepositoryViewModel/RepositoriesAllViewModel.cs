using System;
using System.Collections.Generic;
using System.Text;

namespace Git.ViewModels.RepositoryViewModel
{
    public class RepositoriesAllViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CreatedOn { get; set; }

        public string OwnerName { get; set; }

        public int CommitsCount { get; set; }
    }
}
