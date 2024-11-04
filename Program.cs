using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TV
{
    public void on() => Console.WriteLine("TV: ON");
public void off() => Console.WriteLine("TV: Off");
    public void SetChannel(int channel) => Console.WriteLine($"TV: Канал установлен на {channel}");

    internal void Off()
    {
        throw new NotImplementedException();
    }

    internal void On()
    {
        throw new NotImplementedException();
    }
}
class  AudioSystem
{
    public void on() => Console.WriteLine("AudioSystem:On");
    public void off() => Console.WriteLine("Audiosystem: off");
    public void SetVolume(int volume) => Console.WriteLine("Audiosystema: gromkost na {volume");

    internal void Off()
    {
        throw new NotImplementedException();
    }

    internal void On()
    {
        throw new NotImplementedException();
    }
}
class DVDPlayer
{
    public void Play() => Console.WriteLine("DVD проигрыватель: Воспроизведение");
    public void Pause() => Console.WriteLine("DVD проигрыватель: Пауза");
    public void Stop() => Console.WriteLine("DVD проигрыватель: Остановлено");
}

class GameConsole
{
    public void On() => Console.WriteLine("Игровая консоль: Включена");
    public void PlayGame(string game) => Console.WriteLine($"Игровая консоль: Запуск игры '{game}'");
}

// Фасад HomeTheaterFacade
class HomeTheaterFacade
{
    private readonly TV _tv;
    private readonly AudioSystem _audioSystem;
    private readonly DVDPlayer _dvdPlayer;
    private readonly GameConsole _gameConsole;

    public HomeTheaterFacade(TV tv, AudioSystem audioSystem, DVDPlayer dvdPlayer, GameConsole gameConsole)
    {
        _tv = tv;
        _audioSystem = audioSystem;
        _dvdPlayer = dvdPlayer;
        _gameConsole = gameConsole;
    }

    public void WatchMovie()
    {
        Console.WriteLine("\nПодготовка к просмотру фильма:");
        _tv.On();
        _audioSystem.On();
        _audioSystem.SetVolume(50);
        _dvdPlayer.Play();
    }

    public void StopMovie()
    {
        Console.WriteLine("\nЗавершение просмотра фильма:");
        _dvdPlayer.Stop();
        _tv.Off();
        _audioSystem.Off();
    }

    public void PlayGame(string game)
    {
        Console.WriteLine("\nЗапуск игровой консоли:");
        _tv.On();
        _audioSystem.On();
        _audioSystem.SetVolume(30);
        _gameConsole.On();
        _gameConsole.PlayGame(game);
    }

    public void ListenToMusic()
    {
        Console.WriteLine("\nПодготовка к прослушиванию музыки:");
        _tv.On();
        _audioSystem.On();
        _audioSystem.SetVolume(40);
    }

    public void TurnOff()
    {
        Console.WriteLine("\nВыключение всей системы:");
        _tv.Off();
        _audioSystem.Off();
        _dvdPlayer.Stop();
        _gameConsole.On();
    }
}

abstract class FileSystemComponent
{
    public abstract void Display(int indent = 0);
    public abstract int GetSize();
}

class File : FileSystemComponent
{
    private readonly string _name;
    private readonly int _size;

    public File(string name, int size)
    {
        _name = name;
        _size = size;
    }

    public override void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent * 2) + $"Файл: {_name} (размер: {_size}MB)");
    }

    public override int GetSize() => _size;
}


class Directory : FileSystemComponent
{
    private readonly string _name;
    private readonly List<FileSystemComponent> _children = new List<FileSystemComponent>();

    public Directory(string name)
    {
        _name = name;
    }

    public void Add(FileSystemComponent component)
    {
        if (!_children.Contains(component))
            _children.Add(component);
    }

    public void Remove(FileSystemComponent component)
    {
        if (_children.Contains(component))
            _children.Remove(component);
    }

    public override void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent * 2) + $"Папка: {_name}");
        foreach (var child in _children)
        {
            child.Display(indent + 1);
        }
    }

    public override int GetSize()
    {
        int totalSize = 0;
        foreach (var child in _children)
        {
            totalSize += child.GetSize();
        }
        return totalSize;
    }
}


class Program
{
    static void Main()
    {

        var tv = new TV();
        var audioSystem = new AudioSystem();
        var dvdPlayer = new DVDPlayer();
        var gameConsole = new GameConsole();

        var homeTheater = new HomeTheaterFacade(tv, audioSystem, dvdPlayer, gameConsole);

        homeTheater.WatchMovie();
        homeTheater.StopMovie();
        homeTheater.PlayGame("Super Mario");
        homeTheater.ListenToMusic();
        homeTheater.TurnOff();


        var file1 = new File("file1.txt", 10);
        var file2 = new File("file2.txt", 20);
        var file3 = new File("file3.mp3", 100);

        var folder1 = new Directory("Documents");
        var folder2 = new Directory("Music");
        var rootFolder = new Directory("Root");

        folder1.Add(file1);
        folder1.Add(file2);
        folder2.Add(file3);
        rootFolder.Add(folder1);
        rootFolder.Add(folder2);

        Console.WriteLine("\nСодержимое файловой системы:");
        rootFolder.Display();
        Console.WriteLine($"\nОбщий размер файловой системы: {rootFolder.GetSize()}MB");
    }
}

