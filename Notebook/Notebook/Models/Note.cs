using System;

namespace Notebook.Models
{
    /// <summary>
    /// Это одна заметка
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Уникальный идентификатор заметки
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Содержимое заметки
        /// </summary>
        public string Content { get; set; }
    }
}
