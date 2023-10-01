using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Notebook.Models
{
    /// <summary>
    /// Список всех заметок (его мы сериализуем в файловом хранилище)
    /// </summary>
    public class NotesList
    {
        /// <summary>
        /// Список заметок
        /// </summary>
        [JsonPropertyName("Notes")]
        public List<Note> Notes { get; set; }
    }
}
