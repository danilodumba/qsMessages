using FluentValidation;
using qs.Messages.ApplicationServices.Command;

namespace qs.Messages.ApplicationServices.Validations
{
    public class SendMailCommandValidation : AbstractValidator<SendMailCommand>
    {
        public SendMailCommandValidation()
        {
            RuleFor(x => x.To)
                .EmailAddress().WithMessage("Informe um e-mail valido.")
                .NotEmpty().WithMessage("Informe um e-mail Para.");
            
            RuleFor(x => x.From)
                .EmailAddress().WithMessage("Informe um e-mail valido.")
                .NotEmpty().WithMessage("Informe um e-mail De.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Informe um assunto.");
            
            RuleFor(x => x.ProjectApiKey)
                .NotNull().WithMessage("Informe um projeto.");
        }
    }
}