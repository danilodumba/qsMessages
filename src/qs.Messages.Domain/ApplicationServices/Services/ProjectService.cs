using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qs.Messages.ApplicationServices.Models;
using qs.Messages.ApplicationServices.Services.Interfaces;
using qs.Messages.Domains.Entities;
using qs.Messages.Domains.Repositories;
using qsLibPack.Application;
using qsLibPack.Domain.Exceptions;
using qsLibPack.Repositories.Interfaces;
using qsLibPack.Validations.Interface;

namespace qs.Messages.ApplicationServices.Services
{
    public class ProjectService : ApplicationService, IProjectService
    {
        readonly IProjectRepository _projectRepository;

        public ProjectService(
            IValidationService validationService,
            IUnitOfWork uow,
            IProjectRepository projectRepository) : base(validationService, uow)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Guid> Create(ProjectModel model)
        {
            try
            {
                var project = new Project(model.Name);
                await _projectRepository.CreateAsync(project);
                await _uow.CommitAsync();

                return project.Id;
            }
            catch (DomainException dx)
            {
                _validationService.AddErrors("01", dx.Message);
            }

            return Guid.Empty;
        }

        public async Task Delete(Guid id)
        {
            var project = await _projectRepository.GetByIDAsync(id);
            if (project == null)
            {
                _validationService.AddErrors("01", "Projeto nao encontrado para o ID informado.");
                return;
            }

            _projectRepository.Remove(project);
            await _uow.CommitAsync();
        }

        public async Task<ProjectModel> GetByID(Guid id)
        {
            var project = await _projectRepository.GetByIDAsync(id);
            if (project == null)
            {
                return new ProjectModel();
            }

            return new ProjectModel
            {
                Id = project.Id,
                Name = project.Name,
                ApiKey = project.ApiKey
            };
        }

        public IList<ProjectModel> ListByName(string name)
        {
            var projects = _projectRepository.ListByName(name);
            return projects.Select(p => new ProjectModel{
                Name = p.Name,
                Id = p.Id,
                ApiKey = p.ApiKey
            }).ToList();
        }

        public void Update(ProjectModel model)
        {
            try
            {
                var project = _projectRepository.GetByID(model.Id);
                if (project == null)
                {
                    _validationService.AddErrors("01", "Projeto nao encontrado para o ID informado.");
                    return;
                }

                project.SetName(model.Name);

                _projectRepository.Update(project);
                _uow.Commit();
            }
            catch (DomainException dx)
            {
                _validationService.AddErrors("02", dx.Message);
            }
        }
    }
}