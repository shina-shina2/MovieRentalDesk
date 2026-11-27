using System.Data;
using Microsoft.Data.SqlClient;

namespace MovieRentalDesk;

// Customer screen: shows all customers and lets an employee add one.
public partial class CustomerForm : Form
{
    public CustomerForm()
    {
        InitializeComponent();
        SetupCustomerManagement();
    }

    private void CustomerForm_Load(object sender, EventArgs e)
    {
        TryLoadCustomers();
    }

    private void TryLoadCustomers()
    {
        try { LoadCustomers(); }
        catch (SqlException ex) { MessageBox.Show("Could not load customers: " + ex.Message); }
    }

    private void LoadCustomers()
    {
        dgvCustomers.DataSource = Db.Query(
            "SELECT customer_id AS ID, first_name AS [First Name], last_name AS [Last Name], " +
            "       email AS Email, phone AS Phone, is_active AS Active " +
            "FROM Customer " +
            "WHERE first_name LIKE @search OR last_name LIKE @search OR email LIKE @search " +
            "ORDER BY customer_id",
            Db.P("@search", SqlDbType.NVarChar, 257, "%" + txtCustomerSearch.Text.Trim() + "%"));
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        string first = txtFirst.Text.Trim();
        string last = txtLast.Text.Trim();
        string email = txtEmail.Text.Trim();
        string phone = txtPhone.Text.Trim();

        // 1. Check the required fields first. Phone can be blank.
        if (first == "" || last == "" || email == "")
        {
            MessageBox.Show("First name, last name and email are required.");
            return;
        }
        if (first.Length > 100 || last.Length > 100 || email.Length > 255 || phone.Length > 20)
        {
            MessageBox.Show("Names can be up to 100 characters, email up to 255 and phone up to 20.");
            return;
        }

        // Phone is optional, so save NULL if it's blank.
        object phoneValue = DBNull.Value;
        if (phone != "")
            phoneValue = phone;

        // 2. Send the INSERT with parameters. is_active uses its default (1).
        try
        {
            Db.Execute(
                "INSERT INTO Customer (first_name, last_name, email, phone) " +
                "VALUES (@first, @last, @email, @phone)",
                Db.P("@first", SqlDbType.NVarChar, 100, first),
                Db.P("@last", SqlDbType.NVarChar, 100, last),
                Db.P("@email", SqlDbType.NVarChar, 255, email),
                Db.P("@phone", SqlDbType.VarChar, 20, phoneValue));

            MessageBox.Show($"Added {first} {last}.");
            ClearInputs();
            TryLoadCustomers();                  // 3. Refresh the grid.
        }
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        {
            // UQ_Customer_Email
            MessageBox.Show($"A customer with the email {email} already exists.");
        }


        catch (SqlException ex) when (ex.Number == 547)
        {
            MessageBox.Show("The database rejected one of the values.");
        }
        catch (SqlException ex)
        {
            MessageBox.Show(
                "Could not add customer: " + ex.Message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
    private TextBox txtCustomerSearch = null!;
    private Button btnCustomerSearch = null!;
    private Button btnUpdate = null!;
    private Button btnDeactivate = null!;
    private Label lblSelected = null!;
    private int? selectedCustomerId;

    private void SetupCustomerManagement()
    {
        Text = "Customer Management";
        ClientSize = new Size(900, 590);
        MinimumSize = new Size(900, 590);
        dgvCustomers.Location = new Point(16, 60);
        dgvCustomers.Size = new Size(868, 320);
        grpAdd.Location = new Point(16, 395);
        grpAdd.Size = new Size(868, 165);
        grpAdd.Text = "Add or edit a customer";
        btnAdd.Location = new Point(665, 25);
        btnAdd.Size = new Size(110, 58);

        var lblSearch = new Label { Text = "Search customers:", Location = new Point(16, 20), AutoSize = true };
        txtCustomerSearch = new TextBox { Name = "txtCustomerSearch", Location = new Point(135, 16), Size = new Size(340, 23) };
        btnCustomerSearch = new Button { Name = "btnCustomerSearch", Text = "Search", Location = new Point(485, 14), Size = new Size(95, 27) };
        btnCustomerSearch.Click += (_, _) => TryLoadCustomers();
        txtCustomerSearch.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) { TryLoadCustomers(); e.SuppressKeyPress = true; } };
        Controls.AddRange(new Control[] { lblSearch, txtCustomerSearch, btnCustomerSearch });

        lblSelected = new Label { Text = "Select a customer to edit", AutoSize = true, Location = new Point(16, 110) };
        btnUpdate = new Button { Name = "btnUpdate", Text = "Update selected", Location = new Point(360, 105), Size = new Size(135, 29) };
        btnDeactivate = new Button { Name = "btnDeactivate", Text = "Deactivate selected", Location = new Point(510, 105), Size = new Size(150, 29) };
        btnUpdate.Click += btnUpdate_Click;
        btnDeactivate.Click += btnDeactivate_Click;
        grpAdd.Controls.AddRange(new Control[] { lblSelected, btnUpdate, btnDeactivate });
        dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
    }

    private void dgvCustomers_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvCustomers.CurrentRow?.DataBoundItem is not DataRowView row) return;
        selectedCustomerId = Convert.ToInt32(row["ID"]);
        txtFirst.Text = row["First Name"].ToString();
        txtLast.Text = row["Last Name"].ToString();
        txtEmail.Text = row["Email"].ToString();
        txtPhone.Text = row["Phone"] == DBNull.Value ? "" : row["Phone"].ToString();
        lblSelected.Text = $"Editing customer ID: {selectedCustomerId}";
    }

    private bool GetValidatedInputs(out string first, out string last, out string email, out object phone)
    {
        first = txtFirst.Text.Trim();
        last = txtLast.Text.Trim();
        email = txtEmail.Text.Trim();
        string enteredPhone = txtPhone.Text.Trim();
        phone = enteredPhone == "" ? DBNull.Value : enteredPhone;
        if (first.Length == 0 || last.Length == 0 || email.Length == 0 ||
            first.Length > 100 || last.Length > 100 || email.Length > 255 || enteredPhone.Length > 20)
        {
            MessageBox.Show("Enter first name, last name and email. Check the field lengths too.");
            return false;
        }
        return true;
    }

    private void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (!selectedCustomerId.HasValue) { MessageBox.Show("Select a customer first."); return; }
        if (!GetValidatedInputs(out var first, out var last, out var email, out var phone)) return;
        try
        {
            int updated = Db.Execute(
                "UPDATE Customer SET first_name=@first, last_name=@last, email=@email, phone=@phone WHERE customer_id=@id",
                Db.P("@first", SqlDbType.NVarChar, 100, first),
                Db.P("@last", SqlDbType.NVarChar, 100, last),
                Db.P("@email", SqlDbType.NVarChar, 255, email),
                Db.P("@phone", SqlDbType.VarChar, 20, phone),
                Db.P("@id", SqlDbType.Int, 0, selectedCustomerId.Value));
            MessageBox.Show(updated == 1 ? "Customer updated." : "Customer not found.");
            TryLoadCustomers();
        }
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        { MessageBox.Show("That email is already used by another customer."); }
        catch (SqlException ex)
        { MessageBox.Show("Could not update customer: " + ex.Message); }
    }

    private void btnDeactivate_Click(object? sender, EventArgs e)
    {
        if (!selectedCustomerId.HasValue) { MessageBox.Show("Select a customer first."); return; }
        int id = selectedCustomerId.Value;
        if (MessageBox.Show($"Deactivate customer {id}? Rental history will be kept.",
            "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try
        {
            int updated = Db.Execute("UPDATE Customer SET is_active=0 WHERE customer_id=@id",
                Db.P("@id", SqlDbType.Int, 0, id));
            MessageBox.Show(updated == 1 ? "Customer deactivated. Rental history was kept." : "Customer not found.");
            TryLoadCustomers();
        }
        catch (SqlException ex)
        { MessageBox.Show("Could not deactivate customer: " + ex.Message); }
    }

    private void ClearInputs()
    {
        txtFirst.Clear(); txtLast.Clear(); txtEmail.Clear(); txtPhone.Clear();
        selectedCustomerId = null;
        dgvCustomers.ClearSelection();
        lblSelected.Text = "Select a customer to edit";
    }

}