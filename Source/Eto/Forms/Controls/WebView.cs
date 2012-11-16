using System;
using System.IO;

namespace Eto.Forms
{
	public interface IWebView : IControl
	{
		Uri Url { get; set; }

		void LoadHtml (string html, Uri baseUri);
		
		void GoBack ();
		
		bool CanGoBack { get; }

		void GoForward ();
		
		bool CanGoForward { get; }
		
		void Stop ();
		
		void Reload ();
		
		string DocumentTitle { get; }
		
		string ExecuteScript (string script);

		void ShowPrintDialog();
	}
	
	public class WebViewLoadedEventArgs : EventArgs
	{
		public Uri Uri { get; private set; }
		
		public WebViewLoadedEventArgs (Uri uri)
		{
			this.Uri = uri;
		}
	}
	
	public class WebViewLoadingEventArgs : WebViewLoadedEventArgs
	{
		public bool Cancel { get; set; }
		
		public bool IsMainFrame { get; set; }
		
		public WebViewLoadingEventArgs (Uri uri, bool isMainFrame)
			: base(uri)
		{
			this.IsMainFrame = isMainFrame;
		}
	}
	
	public class WebViewTitleEventArgs : EventArgs
	{
		public string Title { get; private set; }
		
		public WebViewTitleEventArgs (string title)
		{
			this.Title = title;
		}
	}
	
	public class WebViewNewWindowEventArgs : WebViewLoadingEventArgs
	{
		public string NewWindowName { get; private set; }
		
		public WebViewNewWindowEventArgs (Uri uri, string newWindowName)
			: base (uri, false)
		{
			this.NewWindowName = newWindowName;
		}
	}
	
	public class WebView : Control
	{
		IWebView handler;
		
		#region Events

        #region Navigated
        public const string NavigatedEvent = "WebView.Navigated";
        EventHandler<WebViewLoadedEventArgs> navigated;

        public event EventHandler<WebViewLoadedEventArgs> Navigated
        {
            add
            {
                HandleEvent(NavigatedEvent);
                navigated += value;
            }
            remove { navigated -= value; }
        }

        public virtual void OnNavigated(WebViewLoadedEventArgs e)
        {
            if (navigated != null)
                navigated(this, e);
        }

        #endregion

        public const string DocumentLoadedEvent = "WebView.DocumentLoaded";
		EventHandler<WebViewLoadedEventArgs> documentLoaded;

		public event EventHandler<WebViewLoadedEventArgs> DocumentLoaded {
			add { 
				HandleEvent (DocumentLoadedEvent);
				documentLoaded += value;
			}
			remove { documentLoaded -= value; }
		}
		
		public virtual void OnDocumentLoaded (WebViewLoadedEventArgs e)
		{
			if (documentLoaded != null)
				documentLoaded (this, e);
		}
		
		public const string DocumentLoadingEvent = "WebView.DocumentLoading";
		EventHandler<WebViewLoadingEventArgs> documentLoading;

		public event EventHandler<WebViewLoadingEventArgs> DocumentLoading {
			add { 
				HandleEvent (DocumentLoadingEvent);
				documentLoading += value;
			}
			remove { documentLoading -= value; }
		}
		
		public virtual void OnDocumentLoading (WebViewLoadingEventArgs e)
		{
			if (documentLoading != null)
				documentLoading (this, e);
		}

		public const string DocumentTitleChangedEvent = "WebView.DocumentTitleChanged";
		EventHandler<WebViewTitleEventArgs> documentTitleChanged;

		public event EventHandler<WebViewTitleEventArgs> DocumentTitleChanged {
			add { 
				HandleEvent (DocumentTitleChangedEvent);
				documentTitleChanged += value;
			}
			remove { documentTitleChanged -= value; }
		}
		
		public const string OpenNewWindowEvent = "WebView.OpenNewWindow";

		EventHandler<WebViewNewWindowEventArgs> _OpenNewWindow;

		public event EventHandler<WebViewNewWindowEventArgs> OpenNewWindow {
			add {
				HandleEvent (OpenNewWindowEvent);
				_OpenNewWindow += value;
			}
			remove { _OpenNewWindow -= value; }
		}

		public virtual void OnOpenNewWindow (WebViewNewWindowEventArgs e)
		{
			if (_OpenNewWindow != null)
				_OpenNewWindow (this, e);
		}
		
		
		public virtual void OnDocumentTitleChanged (WebViewTitleEventArgs e)
		{
			if (documentTitleChanged != null)
				documentTitleChanged (this, e);
		}
		
		#endregion
		
		public WebView ()
			: this (Generator.Current)
		{
		}
		
		public WebView (Generator generator)
			: this (generator, typeof(IWebView))
		{
		}
		
		protected WebView (Generator generator, Type type, bool initialize = true)
			: base (generator, type, initialize)
		{
			handler = (IWebView)Handler;
		}
		
		public void GoBack ()
		{
			handler.GoBack ();
		}
		
		public bool CanGoBack {
			get{ return handler.CanGoBack; }
		}
		
		public void GoForward ()
		{
			handler.GoForward ();
		}
		
		public bool CanGoForward {
			get { return handler.CanGoForward; }
		}

		public Uri Url {
			get { return handler.Url; }
			set { handler.Url = value; }
		}
		
		public void Stop ()
		{
			handler.Stop ();
		}
		
		public void Reload ()
		{
			handler.Reload ();
		}
		
		public string ExecuteScript (string script)
		{
			return handler.ExecuteScript (script);
		}

		public string DocumentTitle {
			get { return handler.DocumentTitle; }
		}

		public void LoadHtml (Stream stream, Uri baseUri = null)
		{
			using (var reader = new StreamReader(stream)) {
				handler.LoadHtml (reader.ReadToEnd (), baseUri);
			}
		}
		
		public void LoadHtml (string html, Uri baseUri = null)
		{
			handler.LoadHtml (html, baseUri);
		}

        public void ShowPrintDialog ()
        {
            handler.ShowPrintDialog();
        }
	}
}

