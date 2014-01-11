using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using GalaSoft.MvvmLight;

namespace MusicOre.ViewModel
{
	public class Playlist : ObservableObject
	{
		public void Add(FileEntry fileEntry)
		{
			Entries.Add(fileEntry);
		}

		public void Remove(FileEntry fileEntry)
		{
			Entries.Remove(fileEntry);
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

		public void Shuffle(bool turnOn)
		{
			var current = NowPlaying[Index];
			NowPlaying = turnOn ? Entries.Shuffle().ToList() : Entries;
			Index = NowPlaying.IndexOf(current);
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

		public void PlayNow(FileEntry fileEntry)
		{
			Index = NowPlaying.IndexOf(fileEntry);
		}

		public IEnumerable<FileEntry> UpNext
		{
			get { return NowPlaying.SkipWhile((entry, i) => i <= Index); }
		}

		public List<FileEntry> NowPlaying = new List<FileEntry>();
		public List<FileEntry> Entries = new List<FileEntry>();

		public int Index { get; set; }


		public FileEntry this[int index]
		{
			get { return NowPlaying[index]; }
		}
		public FileEntry Current
		{
			get { return this[Index]; }
		}

	}


	public static class ThreadSafeRandom
	{
		[ThreadStatic]
		private static Random Local;

		public static Random ThisThreadsRandom
		{
			get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
		}
	}

	static class MyExtensions
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			T[] elements = source.ToArray();
			for (int i = elements.Length - 1; i >= 0; i--)
			{
				// Swap element "i" with a random earlier element it (or itself)
				// ... except we don't really need to swap it fully, as we can
				// return it immediately, and afterwards it's irrelevant.
				int swapIndex = ThreadSafeRandom.ThisThreadsRandom.Next(i + 1);
				yield return elements[swapIndex];
				elements[swapIndex] = elements[i];
			}
		}
	}
}
