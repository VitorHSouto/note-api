using note_api.Domain.Entities;
using note_api.Domain.Repositories;
using note_api.DTOs.Auth;
using note_api.DTOs.Core;

namespace note_api.Domain.Services
{
    public class UserService
    {
        private UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> ListAll()
        {
            var entities = await _userRepository.ListAll();
            return entities.Select(entity => ToDTO(entity)).ToList();
        }

        public async Task<UserDTO> GetById(Guid id)
        {
            var entity = await _userRepository.GetById(id);
            if (entity == null)
                throw new Exception("Usuário não encontrado.");

            return ToDTO(entity);
        }

        public async Task<UserDTO> GetByLogin(string email, string password)
        {
            var hashPassword = TokenService.HashPassword(password);
            var entity = await _userRepository.GetByLogin(email, hashPassword);
            if (entity == null)
                throw new Exception("E-mail ou senha inválida.");

            return ToDTO(entity);
        }

        public async Task<UserDTO> GetByLogin(Guid id, string token)
        {
            var entity = await _userRepository.GetById(id);
            if (entity == null)
                throw new Exception("Usuário não encontrado.");

            entity.Token = token;
            await _userRepository.Update(entity);

            return ToDTO(entity);
        }

        public async Task<UserDTO> Save(CreateUserRequestDTO req)
        {
            var entity = new UserEntity();
            entity.Name = req.Name;
            entity.Email = req.Email;
            entity.Password = TokenService.HashPassword(req.Password);
            entity.Profile = RoleEnum.USER;
            entity.Token = TokenService.GenerateToken(entity.Email, entity.Profile);

            await _userRepository.Save(entity);
            return ToDTO(entity);
        }

        private UserDTO ToDTO(UserEntity entity)
        {
            var dto = new UserDTO(entity);
            dto.Name = entity.Name;
            dto.Email = entity.Email;
            dto.Profile = entity.Profile;
            dto.Token = entity.Token;
            return dto;
        }
    }
}
