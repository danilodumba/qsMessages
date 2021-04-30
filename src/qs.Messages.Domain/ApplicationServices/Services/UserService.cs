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
    public class UserService : ApplicationService, IUserService
    {
        readonly IUserRepository _userRepository;
        public UserService(
            IValidationService validationService,
            IUnitOfWork uow,
            IUserRepository userRepository) : base(validationService, uow)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Create(UserModel model)
        {
            try
            {
                var user = new User(model.Name, model.Password, model.UserName, model.Admin);
                await _userRepository.CreateAsync(user);
                await _uow.CommitAsync();
                return user.Id;
            }
            catch (DomainException dx)
            {
                _validationService.AddErrors("01", dx.Message);
            }

            return Guid.Empty;
        }

        public async Task Delete(Guid id)
        {
            var user = await _userRepository.GetByIDAsync(id);
            if (user == null)
            {
                _validationService.AddErrors("01", "Usuario nao encontrado para o ID informado.");
                return;
            }

            _userRepository.Remove(user);
            await _uow.CommitAsync();
        }

        public async Task<UserModel> GetByID(Guid id)
        {
            var user = await _userRepository.GetByIDAsync(id);
            if (user == null)
            {
                return new UserModel();
            }

            return new UserModel
            {
                Name = user.Name,
                UserName = user.UserName,
                Id = user.Id,
                Admin = user.Admin
            };
        }

        public IList<UserModel> ListByName(string name)
        {
            var users = _userRepository.ListByName(name);
            return users.Select(u => new UserModel{
                Name = u.Name,
                Id = u.Id,
                Admin = u.Admin,
                UserName = u.UserName
            }).ToList();
        }

        public void Update(UserModel model)
        {
            try
            {
                var user = _userRepository.GetByID(model.Id);
                if (user == null)
                {
                    _validationService.AddErrors("01", "Usuario nao encontrado para o ID informado.");
                    return;
                }

                _userRepository.Update(user);
                _uow.Commit();
            }
            catch (DomainException dx)
            {
                _validationService.AddErrors("02", dx.Message);
            }
        }
    }
}