
using System.Data;
using Microsoft.Data.SqlClient;

namespace MovieRentalDesk;

// A read-only preview of a customer's account. Selecting an ID is not a login.
public sealed class CustomerDashboardForm : Form
{
    private readonly ComboBox cboCustomer = new();
    private readonly Label lblName = new();
    private readonly Label lblEmail = new();
    private readonly Label lblPhone = new();
    private readonly Label lblStatus = new();
    private readonly Label lblCharges = new();
    private readonly Label lblLinks = new();
    private readonly DataGridView dgvLinks = new();
    private readonly Button btnRefresh = new();

    public CustomerDashboardForm()
    {
        Text = "Customer Dashboard (demo view)";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(890, 540);
        MinimumSize = new Size(760, 480);
        BuildControls();
        Load += (_, _) => LoadCustomers();
    }

    private void BuildControls()
    {
        var heading = new Label
        {
            Text = "Customer Dashboard",
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 16)
        };

        var note = new Label
        {
            Text = "Demo only: select a customer ID (this is not authentication).",
            AutoSize = true,
            Location = new Point(20, 55)
        };

        var pickLabel = new Label
        {
            Text = "Customer:",
            AutoSize = true,
            Location = new Point(20, 97)
        };

        cboCustomer.Name = "cboCustomer";
        cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCustomer.Location = new Point(98, 92);
        cboCustomer.Size = new Size(350, 25);
        cboCustomer.SelectedIndexChanged += (_, _) => LoadDashboard();

        btnRefresh.Text = "Refresh";
        btnRefresh.Location = new Point(465, 91);
        btnRefresh.Size = new Size(96, 28);
        btnRefresh.Click += (_, _) => LoadDashboard();

        lblName.Location = new Point(20, 142);
        lblName.AutoSize = true;

        lblEmail.Location = new Point(20, 170);
        lblEmail.AutoSize = true;

        lblPhone.Location = new Point(20, 198);
        lblPhone.AutoSize = true;

        lblStatus.Location = new Point(20, 226);
        lblStatus.AutoSize = true;

        lblCharges.Location = new Point(475, 170);
        lblCharges.AutoSize = true;

        lblLinks.Location = new Point(475, 198);
        lblLinks.AutoSize = true;

        var rentalsHeading = new Label
        {
            Text = "My active streaming links",
            AutoSize = true,
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            Location = new Point(20, 265)
        };

        dgvLinks.Name = "dgvActiveRentals";
        dgvLinks.Location = new Point(20, 297);
        dgvLinks.Size = new Size(845, 225);
        dgvLinks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                          AnchorStyles.Left | AnchorStyles.Right;

        dgvLinks.ReadOnly = true;
        dgvLinks.AllowUserToAddRows = false;
        dgvLinks.AllowUserToDeleteRows = false;
        dgvLinks.MultiSelect = false;
        dgvLinks.RowHeadersVisible = false;
        dgvLinks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLinks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        Controls.AddRange(new Control[]
        {
            heading, note, pickLabel, cboCustomer, btnRefresh,
            lblName, lblEmail, lblPhone, lblStatus,
            lblCharges, lblLinks, rentalsHeading, dgvLinks
        });
    }

    private void LoadCustomers()
    {
        try
        {
            var customers = Db.Query(
                "SELECT customer_id, CONCAT(first_name, ' ', last_name, ' (ID ', customer_id, ')') AS display_name " +
                "FROM Customer ORDER BY customer_id");

            cboCustomer.DisplayMember = "display_name";
            cboCustomer.ValueMember = "customer_id";
            cboCustomer.DataSource = customers;

            if (customers.Rows.Count == 0)
                ClearDashboard();
        }
        catch (SqlException ex)
        {
            ShowError(ex);
        }
    }

    private void LoadDashboard()
    {
        if (cboCustomer.SelectedValue is not int customerId)
            return;

        try
        {
            DataTable info = Db.Query(
                "SELECT first_name, last_name, email, phone, is_active " +
                "FROM Customer WHERE customer_id=@id",
                Db.P("@id", SqlDbType.Int, 0, customerId));

            if (info.Rows.Count == 0)
            {
                ClearDashboard();
                return;
            }

            DataRow c = info.Rows[0];

            lblName.Text = $"Name: {c["first_name"]} {c["last_name"]}";
            lblEmail.Text = $"Email: {c["email"]}";
            lblPhone.Text = $"Phone: {(c["phone"] == DBNull.Value ? "Not provided" : c["phone"])}";
            lblStatus.Text = "Status: " +
                (Convert.ToBoolean(c["is_active"]) ? "Active" : "Inactive");

            DataTable totals = Db.Query(
                "SELECT CAST(COALESCE(SUM(charged_amount), 0) AS DECIMAL(10,2)) AS charges, " +
                "SUM(CASE WHEN expire_time > GETDATE() AND manually_expired_at IS NULL THEN 1 ELSE 0 END) AS active_links " +
                "FROM Rental WHERE customer_id=@id",
                Db.P("@id", SqlDbType.Int, 0, customerId));

            decimal charges =
                Convert.ToDecimal(totals.Rows[0]["charges"]);

            int active = totals.Rows[0]["active_links"] == DBNull.Value
                ? 0
                : Convert.ToInt32(totals.Rows[0]["active_links"]);

            lblCharges.Text = $"Total rental charges: {charges:C2}";
            lblLinks.Text = $"Active links: {active} / 5";

            dgvLinks.DataSource = Db.Query(
                "SELECT r.rental_id AS [Rental ID], m.title AS Movie, r.rental_at AS [Rented At], " +
                "r.expire_time AS [Expires At] " +
                "FROM Rental r JOIN Movie m ON m.movie_id=r.movie_id " +
                "WHERE r.customer_id=@id AND r.expire_time > GETDATE() " +
                "AND r.manually_expired_at IS NULL ORDER BY r.expire_time",
                Db.P("@id", SqlDbType.Int, 0, customerId));
        }
        catch (SqlException ex)
        {
            ShowError(ex);
        }
    }

    private void ClearDashboard()
    {
        lblName.Text = "No customer selected";
        lblEmail.Text = lblPhone.Text = lblStatus.Text =
            lblCharges.Text = lblLinks.Text = "";

        dgvLinks.DataSource = null;
    }

    private static void ShowError(SqlException ex)
    {
        MessageBox.Show(
            "Could not load customer information: " + ex.Message,
            "Database error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}
