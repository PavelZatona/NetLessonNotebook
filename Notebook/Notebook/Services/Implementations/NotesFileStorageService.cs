using DynamicData;
using Notebook.Models;
using Notebook.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notebook.Services.Implementations
{
    /// <summary>
    /// Хранилище заметок в одном файле
    /// </summary>
    public class NotesFileStorageService : INotesStorageService
    {
        /// <summary>
        /// В этот файл (путь относителен от exe) мы будем сохранять заметки
        /// </summary>
        private const string NotesFileName = "Notes.json";

        public void AddNote(Note note)
        {
            // Прочитали заметки из файла
            var currentNotes = DeserializeNotes();

            if (currentNotes.Notes.Any(n => n.Id == note.Id))
            {
                throw new ArgumentException($"Заметка с ID={note.Id} уже есть в хранилище!", nameof(note));
            }

            // Добавили заметку (к коллекции в оперативной памяти)
            currentNotes.Notes.Add(note);

            // Сохраняем заметки обратно в файл
            SerializeNotes(currentNotes);
        }

        public void DeleteNoteById(Guid id)
        {
            // Прочитали заметки из файла
            var currentNotes = DeserializeNotes();

            // Удалили
            currentNotes.Notes = currentNotes.Notes
                .Where(n => n.Id != id)
                .ToList();

            // Сохраняем заметки обратно в файл
            SerializeNotes(currentNotes);
        }

        public IReadOnlyCollection<Note> GetAllNotes()
        {
            return DeserializeNotes().Notes;
        }

        public Note GetNoteById(Guid id)
        {
            // Прочитали заметки из файла
            var currentNotes = DeserializeNotes();

            return currentNotes
                .Notes
                .Single(n => n.Id == id);
        }

        public void UpdateNoteContent(Guid id, string content)
        {
            // Прочитали заметки из файла
            var currentNotes = DeserializeNotes();

            // Обновили
            var note = currentNotes
                .Notes
                .Single(n => n.Id == id);

            note.Content = content;
            note.LastUpdateDate = DateTime.UtcNow;

            // Сохраняем заметки обратно в файл
            SerializeNotes(currentNotes);
        }

        private NotesList DeserializeNotes()
        {
            if (!File.Exists(NotesFileName))
            {
                return new NotesList()
                {
                    Notes = new List<Note>()
                };
            }

            var jsonString = File.ReadAllText(NotesFileName);

            return JsonSerializer.Deserialize<NotesList>(jsonString);
        }

        private void SerializeNotes(NotesList notesList)
        {
            var jsonString = JsonSerializer.Serialize(notesList);

            File.WriteAllText(NotesFileName, jsonString);
        }
    }
}
