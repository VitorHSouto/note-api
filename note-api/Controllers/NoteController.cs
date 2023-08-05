using Microsoft.AspNetCore.Mvc;
using note_api.Domain.Entities;
using note_api.Domain.Services;
using note_api.DTOs.Core;

namespace note_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private NoteService _noteService;
        public NoteController(NoteService noteService)
        {   
            _noteService = noteService;
        }

        [HttpGet("")]
        public async Task<List<NoteEntity>> ListAll()
        {
            return await _noteService.ListAll();
        }

        [HttpGet("{id}")]
        public async Task<NoteEntity> Get(Guid id)
        {
            return await _noteService.GetById(id);
        }

        [HttpPost("")]
        public async Task<NoteEntity> Save(CreateNoteRequestDTO req)
        {
            return await _noteService.Save(req);
        }

        [HttpPut("{id}")]
        public async Task<NoteEntity> Update(Guid id, CreateNoteRequestDTO req)
        {
            return await _noteService.Update(id, req);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return await _noteService.Delete(id);
        }
    }
}
