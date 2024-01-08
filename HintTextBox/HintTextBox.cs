using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Jun.UI {
	public class HintTextBox : TextBox {
		public readonly DependencyProperty HintProperty =
			DependencyProperty.Register(nameof(Hint), typeof(string),
				typeof(HintTextBox), new PropertyMetadata(""));

		public readonly DependencyProperty HintForegroundProperty =
			DependencyProperty.Register(nameof(HintForeground), typeof(Brush),
				typeof(HintTextBox), new PropertyMetadata(Brushes.LightGray));

		public readonly DependencyProperty HintBackgroundProperty =
			DependencyProperty.Register(nameof(HintBackground), typeof(Brush),
				typeof(HintTextBox), new PropertyMetadata(null));

		public Brush HintForeground {
			get { return (Brush)GetValue(HintForegroundProperty); }
			set { SetValue(HintForegroundProperty, value); }
		}

		public Brush HintBackground {
			get { return (Brush)GetValue(HintBackgroundProperty); }
			set { SetValue(HintBackgroundProperty, value); }
		}

		public string Hint {
			get { return (string)GetValue(HintProperty); }
			set { SetValue(HintProperty, value); }
		}

		private readonly VisualBrush _hintBackground = new VisualBrush();
		private Brush _background;

		protected override void OnInitialized(EventArgs e)
		{
			_background = Background;
			if (HintBackground == null) { HintBackground = Background; }

			var _hintVisual = new Label();
			_hintVisual.Content = new TextBlock()
			{
				Text = Hint,
				FontStyle = base.FontStyle,
				FontSize = base.FontSize,
				Background = Brushes.Transparent,
				Padding = new Thickness()
				{
					Left = 2, Right = 2
				}
			};
			_hintVisual.Foreground = HintForeground;
			_hintVisual.Background = HintBackground;

			_hintBackground.Visual = _hintVisual;

			_hintBackground.AlignmentX = AlignmentX.Left;
			_hintBackground.AlignmentY = AlignmentY.Center;
			_hintBackground.Stretch = Stretch.None;

			if (Text.Length < 1) { Background = _hintBackground; }
			else { Background = _background; }

			base.OnInitialized(e);
		}

		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
		{
			_hintBackground.Visual.SetCurrentValue(Label.WidthProperty, ActualWidth);
			base.OnRenderSizeChanged(sizeInfo);
		}

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			if (Text.Length < 1) { Background = _hintBackground; }
			else { Background = _background; }
			base.OnTextChanged(e);
		}

	}
}
