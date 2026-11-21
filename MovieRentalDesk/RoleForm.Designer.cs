namespace MovieRentalDesk;

partial class RoleForm
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
        lblTitle = new Label();
        lblRole = new Label();
        cboRole = new ComboBox();
        btnOpen = new Button();
        btnTest = new Button();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitle.Location = new Point(24, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(170, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Movie Rental Desk";
        //
        // lblRole
        //
        lblRole.AutoSize = true;
        lblRole.Location = new Point(24, 66);
        lblRole.Name = "lblRole";
        lblRole.Size = new Size(87, 15);
        lblRole.TabIndex = 1;
        lblRole.Text = "Choose a role:";
        //
        // cboRole
        //
        cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRole.FormattingEnabled = true;
        cboRole.Items.AddRange(new object[] { "Employee", "Customer" });
        cboRole.Location = new Point(130, 62);
        cboRole.Name = "cboRole";
        cboRole.Size = new Size(180, 23);
        cboRole.TabIndex = 2;
        //
        // btnOpen
        //
        btnOpen.Location = new Point(130, 100);
        btnOpen.Name = "btnOpen";
        btnOpen.Size = new Size(85, 28);
        btnOpen.TabIndex = 3;
        btnOpen.Text = "Open";
        btnOpen.UseVisualStyleBackColor = true;
        btnOpen.Click += btnOpen_Click;
        //
        // btnTest
        //
        btnTest.Location = new Point(225, 100);
        btnTest.Name = "btnTest";
        btnTest.Size = new Size(120, 28);
        btnTest.TabIndex = 4;
        btnTest.Text = "Test connection";
        btnTest.UseVisualStyleBackColor = true;
        btnTest.Click += btnTest_Click;
        //
        // RoleForm
        //
        AcceptButton = btnOpen;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(374, 151);
        Controls.Add(btnTest);
        Controls.Add(btnOpen);
        Controls.Add(cboRole);
        Controls.Add(lblRole);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "RoleForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Movie Rental - Team 06";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblRole;
    private ComboBox cboRole;
    private Button btnOpen;
    private Button btnTest;
}
