using note_api.Domain.DTOs;
using note_api.Entities;
using note_api.Repositories;
using static Dapper.SqlMapper;

namespace note_api.Services
{
    public class NoteService
    {
        private NoteRepository _noteRepository;
        public NoteService(NoteRepository noteRepository) { 
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
            var entity = new NoteEntity();
            entity.Content = req.Content;

            return await _noteRepository.Insert(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _noteRepository.Delete(id);
        }
    }
}
