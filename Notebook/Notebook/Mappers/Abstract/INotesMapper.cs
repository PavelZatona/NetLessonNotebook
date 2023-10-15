using Notebook.Models;
using Notebook.Services.Implementations.DAO;
using System.Collections.Generic;

namespace Notebook.Mappers.Abstract
{
    /// <summary>
    /// Этот интерфейс описывает маппер, переносящий данные между Note и NoteDbo
    /// </summary>
    public interface INotesMapper
    {
        /// <summary>
        /// Превращает заметку, пришедшую из базы (NoteDbo) в заметку, используемую в программе (Note)
        /// </summary>
        Note Map(NoteDbo note);

        /// <summary>
        /// Превращает заметку, используемую в программе (Note), в заметку, сохраняемую в базе (NoteDbo)
        /// </summary>
        NoteDbo Map(Note note);

        /// <summary>
        /// Коллекция заметок из базы -> коллекция заметок программы
        /// </summary>
        IReadOnlyCollection<Note> Map(IReadOnlyCollection<NoteDbo> notes);

        /// <summary>
        /// Коллекция заметок из программы -> коллекция заметок для базы
        /// </summary>
        IReadOnlyCollection<NoteDbo> Map(IReadOnlyCollection<Note> notes);
    }
}
