using System;
using System.Threading;
using System.Threading.Tasks;
using qs.Messages.ApplicationServices.Command;
using qs.Messages.Domains.Entities;
using qs.Messages.Domains.Repositories;
using qs.Messages.Domains.Services;
using qsLibPack.Domain.Exceptions;
using qsLibPack.Domain.ValueObjects;
using qsLibPack.Mediator;
using qsLibPack.Repositories.Interfaces;
using qsLibPack.Validations.Interface;

namespace qs.Messages.ApplicationServices.CommandHandlers
{
    public class CreateMailCommandHandler : CommandHandler<SendMailCommand, Guid>
    {
        readonly IProjectRepository _projectRepository;
        readonly IEmailRepository _emailRepository;
        readonly ITemplateRepository _templateRepository;
        readonly IEmailService _emailService;

        public CreateMailCommandHandler(
            IValidationService validationService,
            IUnitOfWork uow,
            IProjectRepository projectRepository,
            IEmailRepository emailRepository,
            ITemplateRepository templateRepository,
            IEmailService emailService) : base(validationService, uow)
        {
            _projectRepository = projectRepository;
            _emailRepository = emailRepository;
            _templateRepository = templateRepository;
            _emailService = emailService;
        }

        public override async Task<Guid> Handle(SendMailCommand request, CancellationToken cancellationToken)
        {
            if (!this.CommandIsValid(request))
            {
                return Guid.Empty;
            }

            try
            {
                string body = request.Body;
                var project = _projectRepository.GetByApiKey(request.ProjectApiKey);
                if (project == null)
                {
                    _validationService.AddErrors("01", "Projeto nao encontrado para a APIKEY informado");
                    return Guid.Empty;
                }

                if (!string.IsNullOrWhiteSpace(request.TemplateID))
                {
                    var template = await _templateRepository.GetByIDAsync(request.TemplateID);
                    if (template != null)
                    {
                        body = template.MailTemplate;

                        foreach (var key in request.KeyValues)
                        {
                            body = body.Replace(key.Key, key.Value);
                        }
                    }
                }

                var email = new Email(new EmailVO(request.To), new EmailVO(request.From), body, project, request.Subject);
                await _emailRepository.CreateAsync(email);
                await _uow.CommitAsync();

                await this.SendMail(email);
            }
            catch (DomainException dx)
            {
                _validationService.AddErrors("02", dx.Message);
            }

            return Guid.Empty;
        }

        private async Task SendMail(Email email)
        {
            try
            {
                await _emailService.Send(email);
                email.SetStatusSent();
            }
            catch (Exception ex)
            {
                email.SetStatusError(ex.Message);
            }

            _emailRepository.Update(email);
            await _uow.CommitAsync();
        }
    }
}