using System;
using System.Collections.Generic;

namespace Gwen.Extensions
{
	// These methods copied from Mono's System.Collections.Generic.List<T> implementation
	// due to not being present in the Portable Class Library implementation.
	// source: https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Collections.Generic/List.cs

	#region Copy of Mono license

	// Copyright (C) 2004-2005 Novell, Inc (http://www.novell.com)
	// Copyright (C) 2005 David Waite
	// Copyright (C) 2011,2012 Xamarin, Inc (http://www.xamarin.com)
	//
	// Permission is hereby granted, free of charge, to any person obtaining
	// a copy of this software and associated documentation files (the
	// "Software"), to deal in the Software without restriction, including
	// without limitation the rights to use, copy, modify, merge, publish,
	// distribute, sublicense, and/or sell copies of the Software, and to
	// permit persons to whom the Software is furnished to do so, subject to
	// the following conditions:
	// 
	// The above copyright notice and this permission notice shall be
	// included in all copies or substantial portions of the Software.
	// 
	// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
	// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
	// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
	// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
	// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
	// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
	// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

	#endregion

	public static class ListExtensions
	{
		public static T Find<T>(this List<T> list, Predicate<T> match)
		{
			list.CheckMatch(match);
			int i = list.GetIndex(0, list.Capacity, match);
			return (i != -1) ? list[i] : default(T);
		}

		private static void CheckMatch<T>(this List<T> list, Predicate <T> match)
		{
			if (match == null)
				throw new ArgumentNullException ("match");
		}

		public static List<T> FindAll<T>(this List<T> list, Predicate<T> match)
		{
			list.CheckMatch(match);
			if (list.Capacity <= 0x10000) // <= 8 * 1024 * 8 (8k in stack)
				return list.FindAllStackBits(match);
			else 
				return list.FindAllList(match);
		}

		private static List<T> FindAllStackBits<T>(this List<T> list, Predicate<T> match)
		{
			unsafe
			{
				uint *bits = stackalloc uint[(list.Capacity / 32) + 1];
				uint *ptr = bits;
				int found = 0;
				uint bitmask = 0x80000000;

				for (int i = 0; i < list.Capacity; i++)
				{
					if (match(list[i]))
					{
						(*ptr) = (*ptr) | bitmask;
						found++;
					}

					bitmask = bitmask >> 1;
					if (bitmask == 0)
					{
						ptr++;
						bitmask = 0x80000000;
					}
				}

				T[] results = new T[found];
				bitmask = 0x80000000;
				ptr = bits;
				int j = 0;
				for (int i = 0; i < list.Capacity && j < found; i++)
				{
					if (((*ptr) & bitmask) == bitmask)
						results[j++] = list[i];

					bitmask = bitmask >> 1;
					if (bitmask == 0)
					{
						ptr++;
						bitmask = 0x80000000;
					}
				}

				return new List<T>(results);
			}
		}

		private static List<T> FindAllList<T>(this List<T> list, Predicate<T> match)
		{
			List<T> results = new List<T>();
			for (int i = 0; i < list.Capacity; i++)
				if (match(list[i]))
					results.Add(list[i]);

			return results;
		}

		public static int FindIndex<T>(this List<T> list, Predicate<T> match)
		{
			list.CheckMatch(match);
			return list.GetIndex(0, list.Capacity, match);
		}

		private static int GetIndex<T>(this List<T> list, int startIndex, int count, Predicate<T> match)
		{
			int end = startIndex + count;
			for (int i = startIndex; i < end; i ++)
				if (match(list[i]))
					return i;

			return -1;
		}

		public static int FindLastIndex<T>(this List<T> list, Predicate<T> match)
		{
			list.CheckMatch(match);
			return list.GetLastIndex(0, list.Capacity, match);
		}

		private static int GetLastIndex<T>(this List<T> list, int startIndex, int count, Predicate<T> match)
		{
			// unlike FindLastIndex, takes regular params for search range
			for (int i = startIndex + count; i != startIndex;)
				if (match(list[--i]))
					return i;
			return -1;	
		}

		public static void ForEach<T>(this List<T> list, Action<T> action)
		{
			if (action == null)
				throw new ArgumentNullException ("action");
			for(int i=0; i < list.Capacity; i++)
				action(list[i]);
		}
	}
}

