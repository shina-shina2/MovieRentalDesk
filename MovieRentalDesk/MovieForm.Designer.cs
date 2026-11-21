namespace MovieRentalDesk;

partial class MovieForm
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
        lblSearch = new Label();
        txtSearch = new TextBox();
        lblType = new Label();
        cboType = new ComboBox();
        btnSearch = new Button();
        dgvMovies = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)dgvMovies).BeginInit();
        SuspendLayout();
        //
        // lblSearch
        //
        lblSearch.AutoSize = true;
        lblSearch.Location = new Point(16, 20);
        lblSearch.Name = "lblSearch";
        lblSearch.Size = new Size(32, 15);
        lblSearch.TabIndex = 0;
        lblSearch.Text = "Title:";
        //
        // txtSearch
        //
        txtSearch.Location = new Point(56, 16);
        txtSearch.MaxLength = 100;
        txtSearch.Name = "txtSearch";
        txtSearch.Size = new Size(200, 23);
        txtSearch.TabIndex = 1;
        //
        // lblType
        //
        lblType.AutoSize = true;
        lblType.Location = new Point(272, 20);
        lblType.Name = "lblType";
        lblType.Size = new Size(34, 15);
        lblType.TabIndex = 2;
        lblType.Text = "Type:";
        //
        // cboType
        //
        cboType.DropDownStyle = ComboBoxStyle.DropDownList;
        cboType.FormattingEnabled = true;
        cboType.Location = new Point(312, 16);
        cboType.Name = "cboType";
        cboType.Size = new Size(140, 23);
        cboType.TabIndex = 3;
        //
        // btnSearch
        //
        btnSearch.Location = new Point(468, 15);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(80, 25);
        btnSearch.TabIndex = 4;
        btnSearch.Text = "Search";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        //
        // dgvMovies
        //
        dgvMovies.AllowUserToAddRows = false;
        dgvMovies.AllowUserToDeleteRows = false;
        dgvMovies.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvMovies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMovies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvMovies.Location = new Point(16, 52);
        dgvMovies.MultiSelect = false;
        dgvMovies.Name = "dgvMovies";
        dgvMovies.ReadOnly = true;
        dgvMovies.RowHeadersVisible = false;
        dgvMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMovies.Size = new Size(652, 330);
        dgvMovies.TabIndex = 6;
        //
        // MovieForm
        //
        AcceptButton = btnSearch;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(684, 421);
        Controls.Add(dgvMovies);
        Controls.Add(btnSearch);
        Controls.Add(cboType);
        Controls.Add(lblType);
        Controls.Add(txtSearch);
        Controls.Add(lblSearch);
        MinimumSize = new Size(700, 300);
        Name = "MovieForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Movie";
        Load += MovieForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvMovies).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblSearch;
    private TextBox txtSearch;
    private Label lblType;
    private ComboBox cboType;
    private Button btnSearch;
    private DataGridView dgvMovies;
}
