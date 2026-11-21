using System.Data;
using Microsoft.Data.SqlClient;

namespace MovieRentalDesk;

// Movie screen: search by title and filter by type.
public partial class MovieForm : Form
{
    public MovieForm()
    {
        InitializeComponent();
    }

    private void MovieForm_Load(object sender, EventArgs e)
    {
        // The first row means no filter. The types come from the Movie table.
        cboType.DisplayMember = "label";
        cboType.ValueMember = "movie_type";
        cboType.DataSource = Db.Query(
            "SELECT '' AS movie_type, '(All types)' AS label " +
            "UNION SELECT DISTINCT movie_type, movie_type FROM Movie " +
            "ORDER BY movie_type");

        LoadMovies();
    }

    private void LoadMovies()
    {
        dgvMovies.DataSource = Db.Query(
            "SELECT movie_id AS ID, title AS Title, movie_type AS Type, " +
            "       YEAR(release_year) AS [Release Year], is_available AS Available " +
            "FROM Movie " +
            "WHERE title LIKE @pattern AND (@type = '' OR movie_type = @type) " +
            "ORDER BY title",
            Db.P("@pattern", SqlDbType.NVarChar, 102,
                 "%" + txtSearch.Text.Trim() + "%"),
            Db.P("@type", SqlDbType.VarChar, 20,
                 cboType.SelectedValue ?? ""));
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        LoadMovies();
    }
}
