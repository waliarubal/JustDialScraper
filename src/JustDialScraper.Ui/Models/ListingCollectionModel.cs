using DynamicData.Binding;
using JustDialScraper.Common.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace JustDialScraper.Ui.Models
{
    public class ListingCollectionModel : ModelBase, IObservableCollection<ListingModel>
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        readonly List<ListingModel> _items;

        public ListingCollectionModel()
        {
            _items = new List<ListingModel>();
        }

        #region properties

        public ListingModel this[int index] 
        { 
            get => _items[index]; 
            set => _items[index] = value; 
        }

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        #endregion

        void RaiseCollectionChanged(NotifyCollectionChangedAction action)
        {
            var handler = CollectionChanged;
            if (handler != null)
            {
                var arguments = new NotifyCollectionChangedEventArgs(action);
                handler.Invoke(this, arguments);
            }
        }

        public int IndexOf(ListingModel item)
        {
            return _items.IndexOf(item);
        }

        public bool Contains(ListingModel item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(ListingModel[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public void Add(ListingModel item)
        {
            _items.Add(item);
            RaiseCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        public void Clear()
        {
            _items.Clear();
            RaiseCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public void Insert(int index, ListingModel item)
        {
            _items.Insert(index, item);
            RaiseCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        public void Load(IEnumerable<ListingModel> items)
        {
            _items.AddRange(items);
            RaiseCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        public void Move(int oldIndex, int newIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ListingModel item)
        {
            var isRemoved = _items.Remove(item);
            RaiseCollectionChanged(NotifyCollectionChangedAction.Remove);
            return isRemoved;
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
            RaiseCollectionChanged(NotifyCollectionChangedAction.Remove);
        }

        public IDisposable SuspendCount()
        {
            throw new NotImplementedException();
        }

        public IDisposable SuspendNotifications()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ListingModel> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
