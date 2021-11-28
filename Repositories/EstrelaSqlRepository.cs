using CatalogoOficinas.Entities;
using Microsoft.Extensions.Configuration;
using CatalogoOficinas.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoOficinas.Repositories
{
    public class EstrelaSqlServerRepository : IEstrelaRepository
    {

        private readonly SqlConnection sqlConnection;

        public EstrelaSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Oficinas"));
        }
 

        public async Task<List<Estrela>> Obter(int pagina, int quantidade)
        {
            var estrelas = new List<Estrela>();

            var comando = $"select * from Estrela order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";
            
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                estrelas.Add(new Estrela
                {
                    Id = (Guid)sqlDataReader["Id"],
                    IdOficina = (string)sqlDataReader["idOficina"],
                    IdUsuario = (string)sqlDataReader["idUsuarios"],
                    Estrelas = (Double)sqlDataReader["QuantidadeEstrelas"]
                });
            }

            await sqlConnection.CloseAsync();

            return estrelas;
        }

        public async Task<Estrela> Obter(Guid id)
        {
            Estrela estrela = null;

            var comando = $"select * from Estrela where Id = '{id}'";


            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                estrela = new Estrela
                {
                    Id = (Guid)sqlDataReader["Id"],
                    IdOficina = (string)sqlDataReader["idOficina"],
                    IdUsuario = (string)sqlDataReader["idUsuarios"],
                    Estrelas = (Double)sqlDataReader["QuantidadeEstrelas"]
                };
            }
            await sqlConnection.CloseAsync();

            return estrela;
        }

        public async Task<List<Estrela>> Obter(string idOficina, string idUsuario)
        {
            var estrelas = new List<Estrela>();

            //var comando = $"select * from Estrela where idOficina = '{idOficina}' and idUsuarios = '{idUsuario}'";
            var comando = $" select * from Estrela where idOficina = convert(nvarchar(36), '{idOficina}') and idUsuarios = convert(nvarchar(36), '{idUsuario}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                estrelas.Add(new Estrela
                {
                    Id = (Guid)sqlDataReader["Id"],
                    IdOficina = (string)sqlDataReader["idOficina"],
                    IdUsuario = (string)sqlDataReader["idUsuarios"],
                    Estrelas = (Double)sqlDataReader["QuantidadeEstrelas"]
                });
            }

            await sqlConnection.CloseAsync();

            return estrelas;
        }

        public async Task Inserir(Estrela estrela)
        {
            var comando = $"insert Estrela (Id, idOficina, idUsuarios, QuantidadeEstrelas) values ('{estrela.Id}','{estrela.IdOficina}','{estrela.IdUsuario}','{estrela.Estrelas}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Estrela estrela)
        {
            var comando = $"update Estrela set QuantidadeEstrelas = '{estrela.Estrelas}' where Id ='{estrela.Id}'";
            
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            
            var comando = $"delete from Estrela where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();

            //System.InvalidOperationException: The connection was not closed. The connection's current state is open.
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
