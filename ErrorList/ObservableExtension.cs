using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ErrorList {

    public static class ObservableExtension {

        #region replace

        public static void ReplaceAll<T>(this ObservableCollection<T> source, IEnumerable<T> items) {
            source.Clear();
            foreach (var dir in items) {
                source.Add(dir);
            }
        }

        #endregion replace

        #region add sorted

        public static void AddSorted<T>(this ObservableCollection<T> source, T item)
            where T : IComparable<T> {
            source.AddSorted(item, x => x);
        }

        public static void AddSorted<TSource, TKey>(this ObservableCollection<TSource> source, TSource item, Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey> {
            source.AddSorted(item, keySelector, new Comparer<TKey>());
        }

        public static void AddSorted<TSource, TKey>(this ObservableCollection<TSource> source, TSource item, Func<TSource, TKey> keySelector, IComparer<TKey> comparer) {
            source.addRangeSorted(new TSource[] { item }, keySelector, comparer, false);
        }

        public static void AddSortedDescending<T>(this ObservableCollection<T> source, T item)
            where T : IComparable<T> {
            source.AddSortedDescending(item, x => x);
        }

        public static void AddSortedDescending<TSource, TKey>(this ObservableCollection<TSource> source, TSource item, Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey> {
            source.AddSortedDescending(item, keySelector, new Comparer<TKey>());
        }

        public static void AddSortedDescending<TSource, TKey>(this ObservableCollection<TSource> source, TSource item, Func<TSource, TKey> keySelector, IComparer<TKey> comparer) {
            source.addRangeSorted(new TSource[] { item }, keySelector, comparer, true);
        }

        #endregion add sorted

        #region add range sorted

        public static void AddRangeSorted<T>(this ObservableCollection<T> source, IEnumerable<T> items)
            where T : IComparable<T> {
            source.AddRangeSorted(items, x => x);
        }

        public static void AddRangeSorted<TSource, TKey>(this ObservableCollection<TSource> source, IEnumerable<TSource> items, Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey> {
            source.AddRangeSorted(items, keySelector, new Comparer<TKey>());
        }

        public static void AddRangeSorted<TSource, TKey>(this ObservableCollection<TSource> source, IEnumerable<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer) {
            source.addRangeSorted(items, keySelector, comparer, false);
        }

        public static void AddRangeSortedDescending<T>(this ObservableCollection<T> source, IEnumerable<T> items)
            where T : IComparable<T> {
            source.AddRangeSortedDescending(items, x => x);
        }

        public static void AddRangeSortedDescending<TSource, TKey>(this ObservableCollection<TSource> source, IEnumerable<TSource> items, Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey> {
            source.AddRangeSortedDescending(items, keySelector, new Comparer<TKey>());
        }

        public static void AddRangeSortedDescending<TSource, TKey>(this ObservableCollection<TSource> source, IEnumerable<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer) {
            source.addRangeSorted(items, keySelector, comparer, true);
        }

        #endregion add range sorted

        #region internal

        private static void addRangeSorted<TSource, TKey>(this ObservableCollection<TSource> source, IEnumerable<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending) {
            foreach (var item in items) {
                var added = false;
                for (int i = 0; i < source.Count; ++i) {
                    var r = comparer.Compare(keySelector(source[i]), keySelector(item));
                    if ((descending && r < 0) || (!descending && r > 0)) {
                        source.Insert(i, item);
                        added = true;
                        break;
                    }
                }
                if (!added) {
                    source.Add(item);
                }
            }
        }

        #endregion internal

        #region comparer

        private class Comparer<T> : IComparer<T>
            where T : IComparable<T> {

            public int Compare(T x, T y) {
                return x.CompareTo(y);
            }
        }

        #endregion comparer
    }
}