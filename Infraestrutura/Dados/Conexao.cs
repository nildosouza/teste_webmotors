using System.Data.SqlClient;

namespace Dados
{
    public class Conexao
    {
        public static SqlConnection GetConnection(string strConnString)
        {
            return new SqlConnection(strConnString);
        }
    }
}
