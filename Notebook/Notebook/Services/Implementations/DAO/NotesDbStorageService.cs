using Notebook.Mappers.Abstract;
using Notebook.Models;
using Notebook.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Services.Implementations.DAO
{
    public class NotesDbStorageService : INotesStorageService
    {
        /// <summary>
        /// Это маппер заметок Note <-> NoteDbo
        /// </summary>
        private readonly INotesMapper _notesMapper;

        /// <summary>
        /// Это сама база
        /// </summary>
        private readonly MainDbContext _dbContext = new MainDbContext();

        public NotesDbStorageService
        (
            INotesMapper notesMapper
        )
        {
            _notesMapper = notesMapper;
        }

        public void AddNote(Note note)
        {
            var noteDbo = _notesMapper.Map(note);

            noteDbo.Id = Guid.Empty;

            _dbContext.Notes.Add(noteDbo); // Тут мы говорим "сейчас мы будем вставлять строку noteDbo в базу"
            _dbContext.SaveChanges(); // Тут мы собственно сохраняем данные

            // Теперь у noteDbo ID уже не пустой - в нём оказался ID, сгенерированный базой
            note.Id = noteDbo.Id;
        }

        public void DeleteNoteById(Guid id)
        {
            var note = _dbContext
                .Notes
                .Single(n => n.Id == id);

            _dbContext.Notes.Remove(note); // Помечаем найденную заметку к удалению
            _dbContext.SaveChanges(); // Выполняем удаление
        }

        public IReadOnlyCollection<Note> GetAllNotes()
        {
            var notes = _dbContext
                .Notes
                .OrderByDescending(n => n.LastUpdateDate)
                .ToList();

            return _notesMapper.Map(notes);
        }

        public Note GetNoteById(Guid id)
        {
            var note = _dbContext
                .Notes
                .Single(n => n.Id == id);

            return _notesMapper.Map(note);
        }

        public void UpdateNoteContent(Guid id, string content)
        {
            var note = _dbContext
                .Notes
                .Single(n => n.Id == id);

            note.Content = content;
            note.LastUpdateDate = DateTime.UtcNow;

            _dbContext.SaveChanges(); // Выполняем Update
        }
    }
}
