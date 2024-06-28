using System.Data.SqlClient;

namespace ECommerceADO.DataBase
{
    public class DBConnect
    {

        //to create connection
        SqlConnection connect = new SqlConnection("Data Source=ADMIN\\DUCHUY;Initial Catalog=Hshop2023;Integrated Security=True;TrustServerCertificate=False;Encrypt=false");

        //to get connection
        public SqlConnection getConnecttion()
        {
                return connect;

        }

        //create a function to Open connection
        public void openConnection()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        //create a function to Close connection
        public void closeConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }

    }
}
