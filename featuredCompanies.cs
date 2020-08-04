using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Resources;

public partial class firme : System.Web.UI.Page
{
    string connectionString;
    SqlConnection sqlConnection;
    SqlCommand command;
    SqlDataReader reader;

    //--------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        this.connectionString = ConfigurationSettings.AppSettings["connectionString"];
        System.Text.StringBuilder displayValues = new System.Text.StringBuilder();
        this.sqlConnection = new SqlConnection(connectionString);
        this.sqlConnection.Open();

        this.showIstaknuteFirme();

        this.sqlConnection.Close();

    }
    //--------------------------
    private void showIstaknuteFirme()
    {
        string dataTableName = "Hrbaza";

        string sql = "SELECT TOP 5 * FROM " + dataTableName + " WHERE PREZEN='D' ORDER BY newId()";
        this.command = new SqlCommand(sql, this.sqlConnection);

        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        sqlDataAdapter.SelectCommand = command;
        DataSet data = new DataSet();

        sqlDataAdapter.Fill(data, dataTableName);

        DataTable dataTable = data.Tables[dataTableName];
        string img = "";
        string hpURL = "";

        istaknute_firme.Text += "<table id='istaknute-firme'><tr><th colspan='2'>ISTAKNUTE FIRME</th></tr>";

        foreach (DataRow dataRow in dataTable.Rows)
        {
            img = "../logo/" + dataRow["SIFRA"] + ".gif";
            hpURL = "https://hranaipice.com/firma.aspx?f=" + dataRow["SIFRA"];
            istaknute_firme.Text += "<tr><td><img src='" + img +"' class='istaknute-firme-logo'></td>";
            istaknute_firme.Text += "<td><a href='" + hpURL + "' class='istaknute-firme-naziv'>" + dataRow["NAZIV"] + "</a><small><br/>";
            istaknute_firme.Text += "<small>" + dataRow["MESTO"]  + "<br/><a href='" + hpURL + "' class='istaknute-firme-naziv'>"  + dataRow["NET"] + "</a></small></td></tr>";
        }

        istaknute_firme.Text += "</table>";

    }
}
