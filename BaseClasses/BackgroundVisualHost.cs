﻿using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Abraham.Heidebrecht.Controls
{
    public delegate Visual CreateContentFunction();

    public class BackgroundVisualHost : FrameworkElement
    {
        #region Private Fields
        private ThreadedVisualHelper _threadedHelper = null;
        private HostVisual _hostVisual = null; 
        #endregion

        #region IsContentShowingProperty
        /// <summary>
        /// Identifies the IsContentShowing dependency property.
        /// </summary>
        public static readonly DependencyProperty IsContentShowingProperty = DependencyProperty.Register(
            "IsContentShowing",
            typeof(bool),
            typeof(BackgroundVisualHost),
            new FrameworkPropertyMetadata(false, OnIsContentShowingChanged));

        /// <summary>
        /// Gets or sets if the content is being displayed.
        /// </summary>
        public bool IsContentShowing
        {
            get { return (bool)GetValue(IsContentShowingProperty); }
            set { SetValue(IsContentShowingProperty, value); }
        }

        static void OnIsContentShowingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BackgroundVisualHost bvh = (BackgroundVisualHost)d;

            if (bvh.CreateContent != null)
            {
                if ((bool)e.NewValue)
                {
                    bvh.CreateContentHelper();
                }
                else
                {
                    bvh.HideContentHelper();
                }
            }
        }
        #endregion

        #region CreateContent Property
        /// <summary>
        /// Identifies the CreateContent dependency property.
        /// </summary>
        public static readonly DependencyProperty CreateContentProperty = DependencyProperty.Register(
            "CreateContent",
            typeof(CreateContentFunction),
            typeof(BackgroundVisualHost),
            new FrameworkPropertyMetadata(OnCreateContentChanged));

        /// <summary>
        /// Gets or sets the function used to create the visual to display in a background thread.
        /// </summary>
        public CreateContentFunction CreateContent
        {
            get { return (CreateContentFunction)GetValue(CreateContentProperty); }
            set { SetValue(CreateContentProperty, value); }
        }

        static void OnCreateContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BackgroundVisualHost bvh = (BackgroundVisualHost)d;

            if (bvh.IsContentShowing)
            {
                bvh.HideContentHelper();
                if (e.NewValue != null)
                    bvh.CreateContentHelper();
            }
        } 
        #endregion

        protected override int VisualChildrenCount
        {
            get { return _hostVisual != null ? 1 : 0; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (_hostVisual != null && index == 0)
                return _hostVisual;

            throw new IndexOutOfRangeException("index");
        }

        protected override System.Collections.IEnumerator LogicalChildren
        {
            get 
            {
                if (_hostVisual != null)
                    yield return _hostVisual;
            }
        }

        private void CreateContentHelper()
        {
            _threadedHelper = new ThreadedVisualHelper(CreateContent, SafeInvalidateMeasure);
            _hostVisual = _threadedHelper.HostVisual;
        }

        private void SafeInvalidateMeasure()
        {
            Dispatcher.BeginInvoke(new Action(InvalidateMeasure), DispatcherPriority.Loaded);
        }

        private void HideContentHelper()
        {
            if (_threadedHelper != null)
            {
                _threadedHelper.Exit();
                _threadedHelper = null;
                InvalidateMeasure();
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (_threadedHelper != null)
                return _threadedHelper.DesiredSize;

            return base.MeasureOverride(availableSize);
        }

        private class ThreadedVisualHelper
        {
            private readonly HostVisual _hostVisual = null;
            private readonly AutoResetEvent _sync = 
                new AutoResetEvent(false);
            private readonly CreateContentFunction _createContent;
            private readonly Action _invalidateMeasure;

            public HostVisual HostVisual { get { return _hostVisual; } }
            public Size DesiredSize { get; private set; }
            private Dispatcher Dispatcher { get; set; }

            public ThreadedVisualHelper(
                CreateContentFunction createContent, 
                Action invalidateMeasure)
            {
                _hostVisual = new HostVisual();
                _createContent = createContent;
                _invalidateMeasure = invalidateMeasure;

                Thread backgroundUi = new Thread(CreateAndShowContent);
                backgroundUi.SetApartmentState(ApartmentState.STA);
                backgroundUi.Name = "BackgroundVisualHostThread";
                backgroundUi.IsBackground = true;
                backgroundUi.Start();

                _sync.WaitOne();
            }

            public void Exit()
            {
                Dispatcher.BeginInvokeShutdown(DispatcherPriority.Send);
            }

            private void CreateAndShowContent()
            {
                Dispatcher = Dispatcher.CurrentDispatcher;
                VisualTargetPresentationSource source = 
                    new VisualTargetPresentationSource(_hostVisual);
                _sync.Set();
                source.RootVisual = _createContent();
                DesiredSize = source.DesiredSize;
                _invalidateMeasure();

                Dispatcher.Run();
                source.Dispose();
            }
        }
    }
}
