using System;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        [JsonPropertyName("lastUpdateDate")]
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Содержимое заметки
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
