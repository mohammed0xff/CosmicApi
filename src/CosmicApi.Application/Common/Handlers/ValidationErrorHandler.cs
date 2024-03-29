﻿using MediatR;


namespace CosmicApi.Application.Common.Handlers
{

    public class ValidationErrorHandler : INotificationHandler<ValidationError>
    {
        private readonly IList<ValidationError> _errors;

        public ValidationErrorHandler()
        {
            _errors = new List<ValidationError>();
        }

        public IReadOnlyList<ValidationError> GetErrors() => _errors.ToList();

        public bool HasErrors => _errors.Count > 0;

        public Task Handle(ValidationError notification, CancellationToken cancellationToken)
        {
            _errors.Add(notification);

            return Task.CompletedTask;
        }
    }
}