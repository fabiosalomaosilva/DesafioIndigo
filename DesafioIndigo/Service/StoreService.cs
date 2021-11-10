using DesafioIndigo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DesafioIndigo.Service
{
    /// <summary>
    /// Para o presente desafio foi utilizado o ADO.NET pois foi exigido a demonstração de scripts SQL. 
    /// Portanto, não foi utilizado Entity Framework
    /// </summary>
    public class StoreService : IStoreService
    {
        //Connection string está dentro do código em virtude da exigência de local fácil a ser trocada
        string connectionString = @"Data Source=localhost\;Initial Catalog=DesafioIndigo;Persist Security Info=True;User ID=sa;Password=teste.123";

        //Salva a consulta
        public async Task SaveAsync(Consulta consulta)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Insert into Consultas (Logradouro,Bairro,Estado,Municipio,NumeroCep,DataPesquisa,Erro) Values(@Logradouro, @Bairro, @Estado, @Municipio, @NumeroCep, @DataPesquisa, @Erro)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Logradouro", ((object)consulta.Logradouro) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Bairro", ((object)consulta.Bairro) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", ((object)consulta.Estado) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Municipio", ((object)consulta.Municipio) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NumeroCep", ((object)consulta.NumeroCep) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DataPesquisa", ((object)consulta.DataPesquisa) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Erro", ((object)consulta.Erro) ?? DBNull.Value);
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
        }

        //Lista as consultas
        public async Task<List<Consulta>> GatAllAsync()
        {
            var consultas = new List<Consulta>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Consultas";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    var consulta = new Consulta();
                    consulta.Id = Convert.ToInt32(rdr["Id"]);
                    consulta.Logradouro = rdr["Logradouro"].ToString();
                    consulta.Bairro = rdr["Bairro"].ToString();
                    consulta.Estado = rdr["Estado"].ToString();
                    consulta.Municipio = rdr["Municipio"].ToString();
                    consulta.NumeroCep = rdr["NumeroCep"].ToString();
                    consulta.DataPesquisa = Convert.ToDateTime(rdr["DataPesquisa"]);
                    consulta.Erro = rdr["Erro"].ToString();
                    consultas.Add(consulta);
                }
            }
            return consultas;
        }
    }
}
