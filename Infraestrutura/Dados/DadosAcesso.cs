using System;
using System.Data;
using System.Data.SqlClient;

namespace Dados
{
    public class DadosAcesso : IDisposable
    {

        private readonly SqlConnection _conexao;        

        public DadosAcesso(SqlConnection conexao)
        {
            _conexao = conexao;
        }

        public DataTable ExecuteQuery(string strQuery)
        {
            try
            {
                DataTable dt = new DataTable("dados");

                SqlCommand sqlCommand = new SqlCommand(strQuery, _conexao);
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                dt.Load(dataReader);
                sqlCommand.Connection.Close();                
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int ExecuteNonQuery(string strQuery)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand(strQuery, _conexao);
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                int ret = sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
                return ret;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }        

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
