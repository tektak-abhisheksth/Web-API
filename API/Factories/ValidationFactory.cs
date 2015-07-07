using Autofac;
using FluentValidation;
using System;

namespace API.Factories
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext _context;

        public ValidatorFactory(IComponentContext container)
        {
            _context = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _context.IsRegistered(validatorType) ? _context.Resolve(validatorType) as IValidator : null;
        }
    }
}