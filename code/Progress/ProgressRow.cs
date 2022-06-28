using System.Threading;
using Tools;

public class ProgressRow : Widget
{
	public string IconText;
	public string Title;
	public string BottomRight;
	public string BottomLeft;

	public float Current;
	public float Total;
	public float Progress => Current / Total;

	Button cancelButton;

	public ProgressRow( Widget parent ) : base( parent )
	{
		MinimumSize = 64;
		SetSizeMode( SizeMode.Default, SizeMode.CanGrow );
		Title = "";
		IconText = "schedule";

		cancelButton = new Button( null, "cancel", this );
		cancelButton.Visible = false;
	}

	public void SetTitle( string title )
	{
		Title = title;
		Update();
	}

	public void SetProgressText( string title )
	{
		BottomRight = title;
		Update();
	}

	public void SetSubtitle( string title )
	{
		BottomLeft = title;
		Update();
	}

	public void SetIcon( string title )
	{
		IconText = title;
		Update();
	}

	public void SetInfoText( string title )
	{
		BottomLeft = title;
		Update();
	}

	public void UpdateValues( float current, float total )
	{
		Current = current;
		Total = total;
		Update();
	}

	protected override void DoLayout()
	{
		base.DoLayout();

		if ( cancelButton != null )
		{
			//cancelButton.AdjustSize();

			var pos = Size - cancelButton.Size;
			pos.y /= 2.0f;
			pos.x -= 8.0f;

			cancelButton.Position = pos;
		}
	}

	internal void SetCancel( CancellationTokenSource cancel )
	{
		cancelButton.Cursor = CursorShape.Finger;
		cancelButton.Clicked = () => cancel.Cancel();
		cancelButton.Visible = true;

		Update();
	}
}
