namespace MovieRentalDesk;

partial class PlaceholderForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        lblInfo = new Label();
        SuspendLayout();
        //
        // lblInfo
        //
        lblInfo.Dock = DockStyle.Fill;
        lblInfo.Location = new Point(0, 0);
        lblInfo.Name = "lblInfo";
        lblInfo.Size = new Size(434, 161);
        lblInfo.TabIndex = 0;
        lblInfo.Text = "Coming in Lab 11.";
        lblInfo.TextAlign = ContentAlignment.MiddleCenter;
        //
        // PlaceholderForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(434, 161);
        Controls.Add(lblInfo);
        Name = "PlaceholderForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Placeholder";
        ResumeLayout(false);
    }

    #endregion

    private Label lblInfo;
}
