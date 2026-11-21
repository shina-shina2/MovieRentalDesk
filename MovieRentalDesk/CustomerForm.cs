using System.Data;
using Microsoft.Data.SqlClient;

namespace MovieRentalDesk;

// Customer screen: shows all customers and lets an employee add one.
public partial class CustomerForm : Form
{
    public CustomerForm()
    {
        InitializeComponent();
    }

    private void CustomerForm_Load(object sender, EventArgs e)
    {
        LoadCustomers();
    }

    private void LoadCustomers()
    {
        dgvCustomers.DataSource = Db.Query(
            "SELECT customer_id AS ID, first_name AS [First Name], last_name AS [Last Name], " +
            "       email AS Email, phone AS Phone, is_active AS Active " +
            "FROM Customer " +
            "ORDER BY customer_id");
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
            LoadCustomers();                     // 3. Refresh the grid.
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
    }
}
