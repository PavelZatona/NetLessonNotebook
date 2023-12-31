﻿using DynamicData;
using Notebook.Models;
using Notebook.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
        private const string NotesFileName = "Notes.json.gz";

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

/*        private NotesList DeserializeNotes()
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
        }*/

        private NotesList DeserializeNotes()
        {
            if (!File.Exists(NotesFileName))
            {
                return new NotesList()
                {
                    Notes = new List<Note>()
                };
            }

            string jsonString;

            using (var fileStream = File.Open(NotesFileName, FileMode.Open))
            {
                using (var decompressor = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    var streamReader = new StreamReader(decompressor, new UnicodeEncoding());
                    jsonString = streamReader.ReadToEnd();
                }
            }

            return JsonSerializer.Deserialize<NotesList>(jsonString);
        }

        /*private void SerializeNotes(NotesList notesList)
        {
            var jsonString = JsonSerializer.Serialize(notesList);

            //File.WriteAllText(NotesFileName, jsonString);

            using (var memStream = new MemoryStream())
            {
                var streamWriter = new StreamWriter(memStream, new UnicodeEncoding());

                // Собственно пишем строку (в memory stream)
                streamWriter.Write(jsonString);
                streamWriter.Flush();
                memStream.Seek(0, SeekOrigin.Begin);

                // Пишем получившуюся memory stream в файл
                using (var fileStream = File.Open(NotesFileName, FileMode.Create))
                {
                    memStream.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
        }*/

        private void SerializeNotes(NotesList notesList)
        {
            var jsonString = JsonSerializer.Serialize(notesList);

            using (var fileStream = File.Open(NotesFileName, FileMode.Create))
            {
                using (var compressor = new GZipStream(fileStream, CompressionLevel.SmallestSize))
                {
                    var streamWriter = new StreamWriter(compressor, new UnicodeEncoding());

                    streamWriter.Write(jsonString); // Здесь мы пишем строку в стрим
                    streamWriter.Flush(); // Сброс буфера
                }
            }
        }
    }
}
