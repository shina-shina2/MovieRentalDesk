namespace MovieRentalDesk;

// One button for each screen. Movie-Actor, Rental and Reports are empty for now.
public partial class MainMenuForm : Form
{
    public MainMenuForm()
    {
        InitializeComponent();
    }

    private void btnCustomer_Click(object sender, EventArgs e)
    {
        new CustomerForm().ShowDialog();
    }

    private void btnMovie_Click(object sender, EventArgs e)
    {
        new MovieForm().ShowDialog();
    }

    private void btnMovieActor_Click(object sender, EventArgs e)
    {
        new PlaceholderForm("Movie-Actor").ShowDialog();
    }

    private void btnRental_Click(object sender, EventArgs e)
    {
        new PlaceholderForm("Rental").ShowDialog();
    }

    private void btnReports_Click(object sender, EventArgs e)
    {
        new PlaceholderForm("Reports").ShowDialog();
    }
}
