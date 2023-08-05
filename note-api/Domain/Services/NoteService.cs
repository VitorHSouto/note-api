using note_api.Domain.Repositories;
using note_api.DTOs.Core;
using note_api.Domain.Entities;
using static Dapper.SqlMapper;

namespace note_api.Domain.Services
{
    public class NoteService
    {
        private NoteRepository _noteRepository;
        public NoteService(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<List<NoteDTO>> ListAll()
        {
            var entities = await _noteRepository.ListAll();
            return entities.Select(entity => ToDTO(entity)).ToList();
        }

        public async Task<NoteDTO> GetById(Guid id)
        {
            var entity = await _noteRepository.GetById(id);
            return ToDTO(entity);
        }

        public async Task<NoteDTO> Save(CreateNoteRequestDTO req)
        {
            ValidateCreateNoteRequestDTO(req);

            var entity = new NoteEntity();
            entity.Title = req.Title;
            entity.Content = req.Content;

            await _noteRepository.Save(entity);
            return ToDTO(entity);
        }

        public async Task<NoteDTO> Update(Guid id, CreateNoteRequestDTO req)
        {
            var entity = await _noteRepository.GetById(id);
            if (entity == null)
                throw new Exception("Não foi possível encontrar essa anotação.");

            ValidateCreateNoteRequestDTO(req);

            entity.Title = req.Title;
            entity.Content = req.Content;

            await _noteRepository.Update(entity);
            return ToDTO(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _noteRepository.Delete(id);
        }

        private void ValidateCreateNoteRequestDTO(CreateNoteRequestDTO req)
        {
            if (req == null)
                throw new Exception("Erro ao criar a nota");

            if (string.IsNullOrEmpty(req.Title))
                throw new Exception("Defina um valor para o título");

            if (req.Title.Length > 128)
                throw new Exception("O limite de caracteres do título é de 128");

            if (req.Title?.Length > 2048)
                throw new Exception("O limite de caracteres do conteúdo é de 2048");
        }

        private NoteDTO ToDTO(NoteEntity entity)
        {
            var dto = new NoteDTO(entity);
            dto.Title = entity.Title;
            dto.Content = entity.Content;
            return dto;
        }
    }
}
