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
    public class TemplateService : ApplicationService, ITemplateService
    {
        readonly ITemplateRepository _templateRepository;
        readonly IProjectRepository _projectRepository;
        public TemplateService(
            IValidationService validationService,
            IUnitOfWork uow,
            ITemplateRepository templateRepository,
            IProjectRepository projectRepository) : base(validationService, uow)
        {
            _templateRepository = templateRepository;
            _projectRepository = projectRepository;
        }

        public async Task<string> Create(TemplateModel model)
        {
            try
            {
                var project = await _projectRepository.GetByIDAsync(model.ProjectID);
                if (project == null)
                {
                    _validationService.AddErrors("01", "Projeto nao encontrado para o ID informado.");
                    return string.Empty;
                }

                var template = new Template(model.Id, model.Description, model.MailTemplate, project, model.Subject, model.MailFrom);

                await _templateRepository.CreateAsync(template);
                await _uow.CommitAsync();

                return template.Id;
            }
            catch (DomainException dx)
            {
                _validationService.AddErrors("02", dx.Message);
            }

            return string.Empty;
        }

        public async Task Delete(string id)
        {
            var template = await _templateRepository.GetByIDAsync(id);
            if (template == null)
            {
                _validationService.AddErrors("01", "Template nao encontrado para o ID informado.");
                return;
            }

            _templateRepository.Remove(template);
            await _uow.CommitAsync();
        }

        public async Task<TemplateModel> GetByID(string id)
        {
            var template = await _templateRepository.GetByIDAsync(id);
            if (template == null)
            {
                return new TemplateModel();
            }

            return new TemplateModel
            {
                Id = template.Id,
                Description = template.Description,
                ProjectID = template.ProjectID,
                MailTemplate = template.MailTemplate
            };
        }

        public IList<TemplateModel> Search(Guid? projectID, string id, string description)
        {
            var templates = _templateRepository.ListByDescription("");

            var model = templates.Select(t => new TemplateModel{
                Id = t.Id,
                Description = t.Description,
                MailFrom = t.MailFrom,
                MailTemplate = t.MailTemplate,
                Subject = t.Subject
            });

            return model.ToList();
        }

        public void Update(TemplateModel model)
        {
            try
            {
                var template = _templateRepository.GetByID(model.Id);
                if (template == null)
                {
                    _validationService.AddErrors("01", "Template nao encontrado para o ID informado.");
                    return;
                }

                var project = _projectRepository.GetByID(model.ProjectID);
                if (project == null)
                {
                    _validationService.AddErrors("02", "Projeto nao encontrado para o ID informado.");
                    return;
                }

                template.SetDescription(model.Description);
                template.SetMailTemplate(model.MailTemplate);
                template.SetProject(project);
                template.SetMailFrom(model.MailFrom);
                template.SetSubject(model.Subject);

                _templateRepository.Update(template);
                _uow.Commit();
            }
            catch (DomainException dx)
            {
                _validationService.AddErrors("03", dx.Message);
            }
        }
    }
}