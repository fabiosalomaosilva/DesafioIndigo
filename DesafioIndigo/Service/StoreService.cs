using DesafioIndigo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DesafioIndigo.Service
{
    public class StoreService
    {
        string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=data;Trusted_Connection=True;MultipleActiveResultSets=true";

        public async Task SaveAsync(Consulta consulta)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Insert into Consultas (Logradouro,Bairro,Estado,Municipio,NumeropCep,DataPesquisa,Erro) Values(@Logradouro, @Bairro, @Estado, @Municipio, @NumeropCep, @DataPesquisa, @Erro)";
                 SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Logradouro", consulta.Logradouro);
                cmd.Parameters.AddWithValue("@Bairro", consulta.Bairro);
                cmd.Parameters.AddWithValue("@Estado", consulta.Estado);
                cmd.Parameters.AddWithValue("@Municipio", consulta.Municipio);
                cmd.Parameters.AddWithValue("@NumeropCep", consulta.NumeropCep);
                cmd.Parameters.AddWithValue("@DataPesquisa", consulta.DataPesquisa);
                cmd.Parameters.AddWithValue("@Erro", consulta.Erro);
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
        }

        public async Task<List<Consulta>> GatAll()
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
                    consulta.NumeropCep = rdr["NumeropCep"].ToString();
                    consulta.DataPesquisa = Convert.ToDateTime(rdr["DataPesquisa"]);
                    consultas.Add(consulta);
                }
            }
            return consultas;
        }
    }
}
