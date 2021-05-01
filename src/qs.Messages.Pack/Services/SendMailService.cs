using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using qs.Messages.Pack.Events;
using qs.Messages.Pack.Services.Interfaces;
using qsLibPack.Domain.ValueObjects;
using Rebus.Bus;

namespace qs.Messages.Pack.Services
{
    public class SendMailService : ISendMailService
    {
        readonly IBus _bus;
        readonly MessageSettings _settings;

        public SendMailService(IBus bus, IOptions<MessageSettings> settings)
        {
            _bus = bus;
            _settings = settings.Value;
        }

        public async Task Send(
            EmailVO to,
            string templateID,
            params KeyValuePair<string, string>[] values)
        {
            var mailEvent = new SendMailEvent
            {
                To = to.ToString(),
                TemplateID = templateID,
                ProjectApiKey = Guid.Parse(_settings.ProjectApiKey)
            };

            foreach (var value in values)
            {
                mailEvent.KeyValues.Add(value);
            }

            await _bus.Publish(mailEvent);
        }
    }
}