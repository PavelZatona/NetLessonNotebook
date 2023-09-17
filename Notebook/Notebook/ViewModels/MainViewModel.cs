﻿using Avalonia.Collections;
using Notebook.Models;
using Notebook.Services.Abstract;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace Notebook.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Bound properties

    #region Notes

    private AvaloniaList<Note> _notes = new AvaloniaList<Note>();

    public AvaloniaList<Note> Notes
    {
        get => _notes;
        set
        {
            this.RaiseAndSetIfChanged(ref _notes, value);
        }
    }

    #endregion

    #region New note title

    private string _newNoteTitle;

    public string NewNoteTitle
    {
        get => _newNoteTitle;
        set => this.RaiseAndSetIfChanged(ref _newNoteTitle, value);
    }

    #endregion

    #region Selected note index

    private int _selectedNoteIndex;

    public int SelectedNoteIndex
    {
        get => _selectedNoteIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedNoteIndex, value);
    }

    #endregion

    #endregion

    #region Commands

    /// <summary>
    /// Команда для добавления заметки
    /// </summary>
    public ReactiveCommand<Unit, Unit> AddNoteCommand { get; }

    /// <summary>
    /// Команда удаления заметки
    /// </summary>
    public ReactiveCommand<Unit, Unit> DeleteNoteCommand { get; }

    #endregion

    /// <summary>
    /// Экземпляр (объект) хранилища заметок. Мы не знаем, какое это конкретно хранилище,
    /// знаем лишь что оно реализует интерфейс INotesStorageService
    /// </summary>
    private readonly INotesStorageService _notesStorageService;

    public MainViewModel
    (
        // В конструкторе мы говорим "хотим себе хранилище заметок"
        INotesStorageService notesStorageService
    )
    {
        _notesStorageService = notesStorageService; // Сохраняем ссылку на хранилище заметок, переданное в конструктор

        #region Commands to methods binding

        AddNoteCommand = ReactiveCommand.Create(AddNote);
        DeleteNoteCommand = ReactiveCommand.Create(DeleteNote);

        #endregion

        // Загрузка существующих заметок из хранилища при запуске программы
        Notes.AddRange(_notesStorageService.GetAllNotes());

        /*_notesStorageService.AddNote(new Note()
        {
            Id = Guid.NewGuid(),
            LastUpdateDate = DateTime.UtcNow,
            Title = "Тестовая заметка",
            Content = "Тестовое содержимое"
        });*/
    }

    /// <summary>
    /// Метод добавляет новую заметку по нажатию на кнопку
    /// </summary>
    private void AddNote()
    {
        // Создаём тестовую заметку и засовываем её в хранилище
        _notesStorageService.AddNote(new Note()
        {
            Id = Guid.NewGuid(),
            LastUpdateDate = DateTime.UtcNow,
            Title = NewNoteTitle,
            Content = ""
        });

        // Очищаем список заметок на экране и перезапрашиваем его из хранилища (т.к. в нём появилась новая заметка)
        Notes.Clear();
        Notes.AddRange(_notesStorageService.GetAllNotes());
    }

    // Метод удаления выделенной заметки
    private void DeleteNote()
    {
        if (SelectedNoteIndex == -1)
        {
            return;
        }

        if (Notes.Count == 0)
        {
            return;
        }

        // Получаем GUID заметки, которую надо удалить
        var idToDelete = Notes[SelectedNoteIndex].Id;

        _notesStorageService.DeleteNoteById(idToDelete);

        Notes.Clear();
        Notes.AddRange(_notesStorageService.GetAllNotes());
    }
}
