namespace MovieRentalDesk;

partial class CustomerForm
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
        dgvCustomers = new DataGridView();
        grpAdd = new GroupBox();
        lblFirst = new Label();
        txtFirst = new TextBox();
        lblLast = new Label();
        txtLast = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblPhone = new Label();
        txtPhone = new TextBox();
        btnAdd = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
        grpAdd.SuspendLayout();
        SuspendLayout();
        //
        // dgvCustomers
        //
        dgvCustomers.AllowUserToAddRows = false;
        dgvCustomers.AllowUserToDeleteRows = false;
        dgvCustomers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvCustomers.Location = new Point(16, 16);
        dgvCustomers.MultiSelect = false;
        dgvCustomers.Name = "dgvCustomers";
        dgvCustomers.ReadOnly = true;
        dgvCustomers.RowHeadersVisible = false;
        dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCustomers.Size = new Size(752, 260);
        dgvCustomers.TabIndex = 0;
        //
        // grpAdd
        //
        grpAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpAdd.Controls.Add(lblFirst);
        grpAdd.Controls.Add(txtFirst);
        grpAdd.Controls.Add(lblLast);
        grpAdd.Controls.Add(txtLast);
        grpAdd.Controls.Add(lblEmail);
        grpAdd.Controls.Add(txtEmail);
        grpAdd.Controls.Add(lblPhone);
        grpAdd.Controls.Add(txtPhone);
        grpAdd.Controls.Add(btnAdd);
        grpAdd.Location = new Point(16, 308);
        grpAdd.Name = "grpAdd";
        grpAdd.Size = new Size(752, 120);
        grpAdd.TabIndex = 2;
        grpAdd.TabStop = false;
        grpAdd.Text = "Add a customer";
        //
        // lblFirst
        //
        lblFirst.AutoSize = true;
        lblFirst.Location = new Point(16, 30);
        lblFirst.Name = "lblFirst";
        lblFirst.Size = new Size(70, 15);
        lblFirst.TabIndex = 0;
        lblFirst.Text = "First name*:";
        //
        // txtFirst
        //
        txtFirst.Location = new Point(100, 26);
        txtFirst.MaxLength = 100;
        txtFirst.Name = "txtFirst";
        txtFirst.Size = new Size(220, 23);
        txtFirst.TabIndex = 1;
        //
        // lblLast
        //
        lblLast.AutoSize = true;
        lblLast.Location = new Point(344, 30);
        lblLast.Name = "lblLast";
        lblLast.Size = new Size(69, 15);
        lblLast.TabIndex = 2;
        lblLast.Text = "Last name*:";
        //
        // txtLast
        //
        txtLast.Location = new Point(424, 26);
        txtLast.MaxLength = 100;
        txtLast.Name = "txtLast";
        txtLast.Size = new Size(220, 23);
        txtLast.TabIndex = 3;
        //
        // lblEmail
        //
        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(16, 66);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(44, 15);
        lblEmail.TabIndex = 4;
        lblEmail.Text = "Email*:";
        //
        // txtEmail
        //
        txtEmail.Location = new Point(100, 62);
        txtEmail.MaxLength = 255;
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(220, 23);
        txtEmail.TabIndex = 5;
        //
        // lblPhone
        //
        lblPhone.AutoSize = true;
        lblPhone.Location = new Point(344, 66);
        lblPhone.Name = "lblPhone";
        lblPhone.Size = new Size(44, 15);
        lblPhone.TabIndex = 6;
        lblPhone.Text = "Phone:";
        //
        // txtPhone
        //
        txtPhone.Location = new Point(424, 62);
        txtPhone.MaxLength = 20;
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new Size(220, 23);
        txtPhone.TabIndex = 7;
        //
        // btnAdd
        //
        btnAdd.Location = new Point(660, 26);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(80, 59);
        btnAdd.TabIndex = 8;
        btnAdd.Text = "Add";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += btnAdd_Click;
        //
        // CustomerForm
        //
        AcceptButton = btnAdd;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(784, 441);
        Controls.Add(grpAdd);
        Controls.Add(dgvCustomers);
        MinimumSize = new Size(800, 400);
        Name = "CustomerForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Customer";
        Load += CustomerForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
        grpAdd.ResumeLayout(false);
        grpAdd.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DataGridView dgvCustomers;
    private GroupBox grpAdd;
    private Label lblFirst;
    private TextBox txtFirst;
    private Label lblLast;
    private TextBox txtLast;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblPhone;
    private TextBox txtPhone;
    private Button btnAdd;
}
