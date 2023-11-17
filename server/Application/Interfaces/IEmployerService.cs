using JobPortal.Application.ViewModels.AuthM;
using JobPortal.Application.ViewModels.ResponseM;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Application.Interfaces
{
    public interface IEmployerService
    {
        public ResponseViewModel addEmployer(Employer photo);
        public ResponseViewModel deleteEmployer(Guid id,string authToken);
        public ResponseViewModel getEmployer(Guid id);
    }
}
