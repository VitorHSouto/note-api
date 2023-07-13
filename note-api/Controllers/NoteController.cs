using Microsoft.AspNetCore.Mvc;
using note_api.Domain.DTOs;
using note_api.Entities;
using note_api.Services;

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
        public async Task<NoteEntity> ListAll(Guid id)
        {
            return await _noteService.GetById(id);
        }

        [HttpPost("")]
        public async Task<NoteEntity> Get(CreateNoteRequestDTO req)
        {
            return await _noteService.Save(req);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return await _noteService.Delete(id);
        }
    }
}
