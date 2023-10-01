using Notebook.Models;
using System;
using System.Collections.Generic;

namespace Notebook.Services.Abstract
{
    /// <summary>
    /// Сервис для хранения заметок
    /// </summary>
    public interface INotesStorageService
    {
        /// <summary>
        /// Получить список всех заметок
        /// </summary>
        IReadOnlyCollection<Note> GetAllNotes();

        /// <summary>
        /// Добавить заметку
        /// </summary>
        void AddNote(Note note);

        /// <summary>
        /// Метод получает заметку по ID, если такой заметки нет - кидает исключение
        /// </summary>
        Note GetNoteById(Guid id);

        /// <summary>
        /// Удалить заметку с таким-то ID, если такой заметки нет - ничего не делает
        /// </summary>
        void DeleteNoteById(Guid id);

        /// <summary>
        /// Обновить содержимое заметки
        /// </summary>
        void UpdateNoteContent(Guid id, string content);
    }
}
