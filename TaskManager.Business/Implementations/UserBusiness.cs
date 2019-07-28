using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Interfaces;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Model;
using TaskManager.Repository.Context;
using TaskManager.Repository.Interfaces;

namespace TaskManager.Business.Implementations
{
    public class UserBusiness : IBusiness<UserViewModel>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserBusiness(IRepository<User> userRepository,
                                IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task Add(UserViewModel model)
        {
            var userEntity = _mapper.Map<User>(model);
            await _userRepository.Add(userEntity);
        }

        public async Task Delete(int id)
        {
            var userEntity = await GetUser(id);
            await _userRepository.Delete(userEntity);
        }

        public async Task<UserViewModel> Get(int id)
        {
            var userEntity = await GetUser(id);
            var userViewModel = _mapper.Map<UserViewModel>(userEntity);

            return userViewModel;
        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            var userEntities = await _userRepository.GetAll();
            var users = new List<UserViewModel>();
            foreach (var userEntity in userEntities)
            {
                var userViewModel = _mapper.Map<UserViewModel>(userEntity);
                users.Add(userViewModel);
            }

            return users;
        }

        public async Task Update(UserViewModel model)
        {
            var userEntity = _mapper.Map<User>(model);
            var userToBeUpdated = await GetUser(model.Id);
            if (userToBeUpdated != null)
            {
                await _userRepository.Update(userToBeUpdated, userEntity);
            }
        }

        private async Task<User> GetUser(int id)
        {
            var userEntity = await _userRepository.Get(id);
            if (userEntity == null)
            {
                throw new TaskManagerException(ErrorCodes.UserNotFoundResponse, "User not found");
            }

            return userEntity;
        }
    }
}
