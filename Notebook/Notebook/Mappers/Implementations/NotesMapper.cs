using Notebook.Mappers.Abstract;
using Notebook.Models;
using Notebook.Services.Implementations.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Mappers.Implementations
{
    public class NotesMapper : INotesMapper
    {
        public Note Map(NoteDbo note)
        {
            if (note == null)
            {
                return null;
            }

            return new Note()
            {
                Id = note.Id,
                LastUpdateDate = note.LastUpdateDate,
                Title = note.Title,
                Content = note.Content
            };
        }

        public NoteDbo Map(Note note)
        {
            if (note == null)
            {
                return null;
            }

            return new NoteDbo()
            {
                Id = note.Id,
                LastUpdateDate = note.LastUpdateDate,
                Title = note.Title,
                Content = note.Content
            };
        }

        public IReadOnlyCollection<Note> Map(IReadOnlyCollection<NoteDbo> notes)
        {
            if (notes == null)
            {
                return null;
            }

            return notes
                .Select(n => Map(n))
                .ToList();
        }

        public IReadOnlyCollection<NoteDbo> Map(IReadOnlyCollection<Note> notes)
        {
            if (notes == null)
            {
                return null;
            }

            return notes
                .Select(n => Map(n))
                .ToList();
        }
    }
}
