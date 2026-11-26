using Microsoft.Data.SqlClient;

namespace MovieRentalDesk;

// This just picks which screen to open. It's not a real login.
public partial class RoleForm : Form
{
    public RoleForm()
    {
        InitializeComponent();
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
        if (cboRole.Text == "Employee")
            new MainMenuForm().ShowDialog();
        else if (cboRole.Text == "Customer")
            new CustomerDashboardForm().ShowDialog();   // customer view, not the employee menu
        else
            MessageBox.Show("Choose a role first.");
    }

    private void btnTest_Click(object sender, EventArgs e)
    {
        try
        {
            object n = Db.Scalar("SELECT COUNT(*) FROM Movie");
            MessageBox.Show($"Connected. The Movie table has {n} rows.");
        }
        catch (SqlException ex)
        {
            MessageBox.Show("Could not connect: " + ex.Message);
        }
    }
}
