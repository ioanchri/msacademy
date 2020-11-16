using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCF.Services
{
    public interface IProjectService
    {
        ProjectService CreateProject(ProjectService projectService);
        ProjectService UpdateProject(ProjectService projectSer, int id);
        bool DeleteProject(int id);
    }
}
