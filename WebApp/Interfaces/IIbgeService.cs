using devops_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace devops_project.Interfaces
{
    public interface IIbgeService
    {
        public Task<List<NameInfo>> GetNameFrequencyAsync(string name);
    }
}
