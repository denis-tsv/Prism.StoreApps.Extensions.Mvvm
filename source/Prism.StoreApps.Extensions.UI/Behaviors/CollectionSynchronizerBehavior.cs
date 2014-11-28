using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Xaml.Interactivity;

namespace Prism.StoreApps.Extensions.UI.Behaviors
{
    public class CollectionSynchronizerBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty IsTwoWayProperty = DependencyProperty.Register("IsTwoWay", typeof(bool), typeof(CollectionSynchronizerBehavior), new PropertyMetadata(false, OnIsTwoWayChanged));
        public static readonly DependencyProperty SourceCollectionProperty = DependencyProperty.Register("SourceCollection", typeof(IList), typeof(CollectionSynchronizerBehavior), new PropertyMetadata(null, OnSourceCollectionChanged));
        public static readonly DependencyProperty TargetCollectionPropertyNameProperty = DependencyProperty.Register("TargetCollectionPropertyName", typeof(string), typeof(CollectionSynchronizerBehavior), new PropertyMetadata(null, OnTargetCollectionPropertyNameChanged));

        private bool _selfUpdate;
        private IList<object> _targetCollection;
        private INotifyCollectionChanged _sourceCollection;

        public bool IsTwoWay
        {
            get { return (bool)GetValue(IsTwoWayProperty); }
            set { SetValue(IsTwoWayProperty, value); }
        }

        public IList SourceCollection
        {
            get { return (IList)GetValue(SourceCollectionProperty); }
            set { SetValue(SourceCollectionProperty, value); }
        }

        public string TargetCollectionPropertyName
        {
            get { return (string)GetValue(TargetCollectionPropertyNameProperty); }
            set { SetValue(TargetCollectionPropertyNameProperty, value); }
        }

        private void OnTargetCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SourceCollection == null || _selfUpdate)
            {
                return;
            }

            Debug.Assert(IsTwoWay);
            _selfUpdate = true;
            try
            {
                if (e.AddedItems != null)
                {
                    foreach (object obj in e.AddedItems)
                    {
                        SourceCollection.Add(obj);
                    }
                }
                if (e.RemovedItems != null)
                {
                    foreach (object obj in e.RemovedItems)
                    {
                        SourceCollection.Remove(obj);
                    }
                }
            }
            finally
            {
                _selfUpdate = false;
            }
        }

        private void AttachTargetCollectionListener()
        {
            var selector = AssociatedObject as Selector;
            if (selector != null)
                selector.SelectionChanged += OnTargetCollectionChanged;
        }

        private void AttachSourceCollectionListener()
        {
            if (_sourceCollection != null)
            {
                _sourceCollection.CollectionChanged += OnSourceCollectionChanged;
            }
        }

        private void DetachTargetCollectionListener()
        {
            var selector = AssociatedObject as Selector;
            if (selector != null)
                selector.SelectionChanged -= OnTargetCollectionChanged;
        }

        private void DetachSourceCollectionListener()
        {
            if (_sourceCollection != null)
            {
                _sourceCollection.CollectionChanged -= OnSourceCollectionChanged;
            }
        }

        private void UpdateTargetCollection()
        {
            if (IsTwoWay)
            {
                DetachTargetCollectionListener();
            }

            if (!string.IsNullOrEmpty(TargetCollectionPropertyName) && AssociatedObject != null)
            {
                PropertyInfo propertyInfo = AssociatedObject.GetType().GetRuntimeProperty(TargetCollectionPropertyName);

                if (propertyInfo == null)
                {
                    throw new InvalidOperationException(string.Format("{0} doesn't contain accessible {1} property", AssociatedObject, TargetCollectionPropertyName));
                }

                if (propertyInfo.CanWrite && propertyInfo.SetMethod != null)
                {
                    throw new InvalidOperationException(string.Format("Property {1} of {0} is writeable. Use binding instead", AssociatedObject, TargetCollectionPropertyName));
                }

                object value = propertyInfo.GetValue(AssociatedObject, null);

                if (value == null)
                {
                    throw new InvalidOperationException(string.Format("{0} has nothing in {1} property, no binding is possible", AssociatedObject, TargetCollectionPropertyName));
                }

                _targetCollection = value as IList<object>;

                if (_targetCollection == null)
                {
                    throw new InvalidOperationException(string.Format("Assosiated {1} collection {0} doesn't implement IList, no binding is possible", value, TargetCollectionPropertyName));
                }

                if (SourceCollection != null)
                {
                    _targetCollection.Clear();
                    InsertItems(_targetCollection, 0, SourceCollection);
                }
            }
            else
            {
                _targetCollection = null;
            }

            if (IsTwoWay)
            {
                AttachTargetCollectionListener();
            }
        }

        private void InsertItems(IList<object> target, int startIndex, IEnumerable items)
        {
            items.Cast<object>()
                .Select((item, index) => new { Item = item, Index = startIndex + index })
                .ForEach(desc => target.Insert(desc.Index, desc.Item));
        }

        private void RemoveItems(IList<object> target, int startIndex, int count)
        {
            Enumerable.Repeat(startIndex, count)
                .ForEach(target.RemoveAt);
        }

        private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_targetCollection == null || _selfUpdate)
            {
                return;
            }

            _selfUpdate = true;

            try
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        InsertItems(_targetCollection, e.NewStartingIndex, e.NewItems);
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        RemoveItems(_targetCollection, e.OldStartingIndex, e.OldItems.Count);
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        throw new NotImplementedException("NotifyCollectionChangedAction.Replace is not supported");

                    case NotifyCollectionChangedAction.Reset:
                        _targetCollection.Clear();

                        Debug.Assert(sender is IEnumerable);
                        InsertItems(_targetCollection, 0, (IEnumerable)sender);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            finally
            {
                _selfUpdate = false;
            }
        }

        private static void OnIsTwoWayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.Assert(d is CollectionSynchronizerBehavior);
            var behavior = (CollectionSynchronizerBehavior)d;

            if ((bool)e.NewValue)
            {
                behavior.AttachTargetCollectionListener();
            }
            else
            {
                behavior.DetachTargetCollectionListener();
            }
        }

        private static void OnSourceCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.Assert(d is CollectionSynchronizerBehavior);
            var behavior = (CollectionSynchronizerBehavior)d;

            behavior.DetachSourceCollectionListener();

            var newValueAsNotifier = e.NewValue as INotifyCollectionChanged;
            if (newValueAsNotifier != null)
            {
                behavior._sourceCollection = newValueAsNotifier;
                behavior.AttachSourceCollectionListener();
                behavior.OnSourceCollectionChanged(e.NewValue, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private static void OnTargetCollectionPropertyNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.Assert(d is CollectionSynchronizerBehavior);
            ((CollectionSynchronizerBehavior)d).UpdateTargetCollection();
        }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
            UpdateTargetCollection();
        }

        public void Detach()
        {
            TargetCollectionPropertyName = null;
            DetachSourceCollectionListener();
            UpdateTargetCollection();
        }

        public DependencyObject AssociatedObject { get; private set; }
    }
}
