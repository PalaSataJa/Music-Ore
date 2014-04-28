using GalaSoft.MvvmLight;
using MusicOre.Model;
using System.Collections.Generic;
using System.Linq;

namespace MusicOre.ViewModel
{
    public class Playlist : ObservableObject
    {
        private readonly List<FileEntry> Entries = new List<FileEntry>();

        private List<FileEntry> NowPlaying = new List<FileEntry>();

        private bool shuffle;

        public Playlist()
        {
            Shuffle = true;
        }

        public FileEntry Current
        {
            get { return this[Index]; }
        }

        public int Index { get; set; }

        public bool Shuffle
        {
            get { return shuffle; }
            set
            {
                shuffle = value;
                DoShuffle();
            }
        }

        public IEnumerable<FileEntry> UpNext
        {
            get { return NowPlaying.SkipWhile((entry, i) => i <= Index); }
        }

        public FileEntry this[int index]
        {
            get { return NowPlaying[index]; }
        }

        public void Add(FileEntry fileEntry)
        {
            Entries.Add(fileEntry);
            DoShuffle();
        }

        public void AllMusic()
        {
            Entries.AddRange(LibraryOperations.CurrentDeviceMediaEntries);
            Index = ThreadSafeRandom.ThisThreadsRandom.Next(0, Entries.Count - 1);
            DoShuffle();
        }

        public void Next()
        {
            if (Index == NowPlaying.Count - 1)
            {
                Index = 0;
            }
            else
            {
                Index++;
            }
        }

        public void PlayNow(FileEntry fileEntry)
        {
            Index = NowPlaying.IndexOf(fileEntry);
        }

        public void Previous()
        {
            if (Index == 0)
            {
                Index = NowPlaying.Count - 1;
            }
            else
            {
                Index--;
            }
        }

        public void Remove(FileEntry fileEntry)
        {
            Entries.Remove(fileEntry);
            DoShuffle();
        }

        private void DoShuffle()
        {
            if (Entries.Count == 0)
                return;
            FileEntry current = null;
            if (NowPlaying.Count != 0)
            {
                current = NowPlaying[Index];
            }
            NowPlaying = Shuffle ? Entries.Shuffle().ToList() : Entries;
            Index = current != null ? NowPlaying.IndexOf(current) : ThreadSafeRandom.ThisThreadsRandom.Next(0, NowPlaying.Count - 1);
        }
    }
}