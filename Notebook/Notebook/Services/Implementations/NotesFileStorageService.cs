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
    /// Хранилище заметок в одном файле
    /// </summary>
    public class NotesFileStorageService : INotesStorageService
    {
        public void AddNote(Note note)
        {
            throw new NotImplementedException("Добавление заметки в файл не реализовано!");
        }

        public void DeleteNoteById(Guid id)
        {
            throw new NotImplementedException("Удаление заметок из файла не реализовано!");
        }

        public IReadOnlyCollection<Note> GetAllNotes()
        {
            throw new NotImplementedException("Получение заметок из файла не реализовано!");
        }

        public Note GetNoteById(Guid id)
        {
            throw new NotImplementedException("Получение заметки из файла по ID не реализовано!");
        }
    }
}
