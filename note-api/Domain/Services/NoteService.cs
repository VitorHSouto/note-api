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

        public async Task<List<NoteEntity>> ListAll()
        {
            return await _noteRepository.ListAll();
        }

        public async Task<NoteEntity> GetById(Guid id)
        {
            return await _noteRepository.GetById(id);
        }

        public async Task<NoteEntity> Save(CreateNoteRequestDTO req)
        {
            ValidateCreateNoteRequestDTO(req);

            var entity = new NoteEntity();
            entity.Title = req.Title;
            entity.Content = req.Content;

            return await _noteRepository.Insert(entity);
        }

        public async Task<NoteEntity> Update(Guid id, CreateNoteRequestDTO req)
        {
            var entity = await GetById(id);
            if (entity == null)
                throw new Exception("Não foi possível encontrar essa anotação.");

            ValidateCreateNoteRequestDTO(req);

            entity.Title = req.Title;
            entity.Content = req.Content;

            return await _noteRepository.Update(entity);
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
    }
}
