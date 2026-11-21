namespace MovieRentalDesk;

partial class MainMenuForm
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
        btnCustomer = new Button();
        btnMovie = new Button();
        btnMovieActor = new Button();
        btnRental = new Button();
        btnReports = new Button();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitle.Location = new Point(24, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(195, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Employee Main Menu";
        //
        // btnCustomer
        //
        btnCustomer.Location = new Point(28, 64);
        btnCustomer.Name = "btnCustomer";
        btnCustomer.Size = new Size(240, 36);
        btnCustomer.TabIndex = 1;
        btnCustomer.Text = "Customer";
        btnCustomer.UseVisualStyleBackColor = true;
        btnCustomer.Click += btnCustomer_Click;
        //
        // btnMovie
        //
        btnMovie.Location = new Point(28, 108);
        btnMovie.Name = "btnMovie";
        btnMovie.Size = new Size(240, 36);
        btnMovie.TabIndex = 2;
        btnMovie.Text = "Movie";
        btnMovie.UseVisualStyleBackColor = true;
        btnMovie.Click += btnMovie_Click;
        //
        // btnMovieActor
        //
        btnMovieActor.Location = new Point(28, 152);
        btnMovieActor.Name = "btnMovieActor";
        btnMovieActor.Size = new Size(240, 36);
        btnMovieActor.TabIndex = 3;
        btnMovieActor.Text = "Movie-Actor";
        btnMovieActor.UseVisualStyleBackColor = true;
        btnMovieActor.Click += btnMovieActor_Click;
        //
        // btnRental
        //
        btnRental.Location = new Point(28, 196);
        btnRental.Name = "btnRental";
        btnRental.Size = new Size(240, 36);
        btnRental.TabIndex = 4;
        btnRental.Text = "Rental";
        btnRental.UseVisualStyleBackColor = true;
        btnRental.Click += btnRental_Click;
        //
        // btnReports
        //
        btnReports.Location = new Point(28, 240);
        btnReports.Name = "btnReports";
        btnReports.Size = new Size(240, 36);
        btnReports.TabIndex = 5;
        btnReports.Text = "Reports";
        btnReports.UseVisualStyleBackColor = true;
        btnReports.Click += btnReports_Click;
        //
        // MainMenuForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(298, 300);
        Controls.Add(btnReports);
        Controls.Add(btnRental);
        Controls.Add(btnMovieActor);
        Controls.Add(btnMovie);
        Controls.Add(btnCustomer);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "MainMenuForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Main Menu";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Button btnCustomer;
    private Button btnMovie;
    private Button btnMovieActor;
    private Button btnRental;
    private Button btnReports;
}
