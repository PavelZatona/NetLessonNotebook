using Notebook.Models;
using Notebook.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Services.Implementations
{
    /// <summary>
    /// Хранилище заметок в оперативке
    /// </summary>
    public class NotesMemoryStorageService : INotesStorageService
    {
        /// <summary>
        /// Хранилище заметок
        /// </summary>
        private List<Note> _notesInMemory = new List<Note>();

        public void AddNote(Note note)
        {
            _ = note ?? throw new ArgumentNullException(nameof(note), "Заметка должна быть задана");

            // Если в _notesInMemory есть хотя-бы один (Any()) член, удовлетворяющий условию:
            // n => n.Id == note.Id - это значит "перебрать все заметки, подкладывая их в n и проверять совпадает ли
            // n.Id с Id новой заметки"
            if (_notesInMemory.Any(n => n.Id == note.Id))
            {
                throw new ArgumentException($"Заметка с ID={note.Id} уже есть в хранилище!", nameof(note));
            }

            _notesInMemory.Add(note);
        }

        public void DeleteNoteById(Guid id)
        {
            _notesInMemory = _notesInMemory
                .Where(n => n.Id != id)
                .ToList();
        }

        public IReadOnlyCollection<Note> GetAllNotes()
        {
            return _notesInMemory;
        }

        public Note GetNoteById(Guid id)
        {
            return _notesInMemory
                .Single(n => n.Id == id); // Вернуть единственный элемент, удовлетворяющий условию. Если никто не удовлетворяет
            // или удовлетворяет несколько - выпадает
        }

        public void UpdateNoteContent(Guid id, string content)
        {
            var note = GetNoteById(id);

            note.Content = content;
            note.LastUpdateDate = DateTime.UtcNow;
        }
    }
}
