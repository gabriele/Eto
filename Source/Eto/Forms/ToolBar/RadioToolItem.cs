using System;

namespace Eto.Forms
{
	/// <summary>
	/// Tool item that can be toggled on or off.
	/// </summary>
	/// <remarks>
	/// Note that there is only a single group of radio buttons allowed on a toolbar.
	/// </remarks>
	[Handler(typeof(RadioToolItem.IHandler))]
	public class RadioToolItem : ToolItem
	{
		new IHandler Handler { get { return (IHandler)base.Handler; } }

		/// <summary>
		/// Occurs when the <see cref="Checked"/> property is changed.
		/// </summary>
		public event EventHandler<EventArgs> CheckedChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="Eto.Forms.RadioToolItem"/> class.
		/// </summary>
		public RadioToolItem()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Eto.Forms.RadioToolItem"/> class with the specified <paramref name="command"/>.
		/// </summary>
		/// <param name="command">Command for the tool item.</param>
		public RadioToolItem(CheckCommand command)
			: base(command)
		{
			Checked = command.Checked;
			command.CheckedChanged += (sender, e) => Checked = command.Checked;
			CheckedChanged += (sender, e) => command.Checked = Checked;
			Handler.CreateFromCommand(command);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Eto.Forms.RadioToolItem"/> class.
		/// </summary>
		/// <param name="generator">Generator.</param>
		[Obsolete("Use default constructor instead")]
		public RadioToolItem(Generator generator)
			: base(generator, typeof(IHandler))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Eto.Forms.RadioToolItem"/> class.
		/// </summary>
		/// <param name="command">Command.</param>
		/// <param name="generator">Generator.</param>
		[Obsolete("Use RadioToolItem(CheckCommand) instead")]
		public RadioToolItem(CheckCommand command, Generator generator = null)
			: base(command, generator, typeof(IHandler))
		{
			Checked = command.Checked;
			command.CheckedChanged += (sender, e) => Checked = command.Checked;
			Click += (sender, e) => command.Checked = Checked;
			Handler.CreateFromCommand(command);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this item is checked.
		/// </summary>
		/// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
		public bool Checked
		{
			get { return Handler.Checked; }
			set { Handler.Checked = value; }
		}

		/// <summary>
		/// Raises the <see cref="CheckedChanged"/> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		public void OnCheckedChanged(EventArgs e)
		{
			if (CheckedChanged != null)
				CheckedChanged(this, e);
		}

		/// <summary>
		/// Handler for the <see cref="RadioToolItem"/>.
		/// </summary>
		public new interface IHandler : ToolItem.IHandler
		{
			/// <summary>
			/// Gets or sets a value indicating whether this item is checked.
			/// </summary>
			/// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
			bool Checked { get; set; }
		}
	}
}
