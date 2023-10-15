using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Notebook.Services.Implementations.DAO
{
    /// <summary>
    /// Это заметка в том виде, в котором она будет храниться в базе
    /// </summary>
    public class NoteDbo
    {
        /// <summary>
        /// Уникальный идентификатор заметки
        /// </summary>
        [Key]
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
