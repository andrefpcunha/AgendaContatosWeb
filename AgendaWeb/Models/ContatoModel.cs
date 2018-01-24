using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AgendaWeb.Models
{
    public class ContatoModel
    {
        public int ID { get; set; }
        
        [Display(Name = "Nome")]
        public string nome { get; set; }
        
        [Display(Name = "Telefone")]
        public string telefone { get; set; }

        public int CodUsuario { get; internal set; }
    }

    public class ContatoRepositorio : Repositorio<ContatoModel, int>
    {
        public override List<ContatoModel> GetByName(string nome)
        {
            string sql = string.Format("SELECT [c].[ID], [c].[nome], [c].[telefone], [c].[CodUsuario] " +
                                           "FROM [contatos] c, [usuarios] u " +
                                           "WHERE [c].[CodUsuario] = [u].[ID] " +
                                           "AND [c].excluido = 0 AND [u].[email] = '{0}' order by c.nome", nome);

            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                List<ContatoModel> list = new List<ContatoModel>();
                ContatoModel C = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            C = new ContatoModel
                            {
                                ID = (int)reader["ID"],
                                nome = reader["nome"].ToString(),
                                telefone = reader["telefone"].ToString(),
                                CodUsuario = (int)reader["CodUsuario"]
                            };

                            list.Add(C);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return list;
            }
        }

        public override void Delete(ContatoModel entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = String.Format("UPDATE [dbo].[contatos] " +
                                           "SET[excluido] = 1 " +
                                           "WHERE ID = {0} ", entity.ID);
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public override void DeleteById(int id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = String.Format("DELETE Contatos Where Id= {0}", id);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        
        public override List<ContatoModel> GetAll()
        {
            string sql = "SELECT id, nome, telefone FROM contatos order by Nome";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                List<ContatoModel> list = new List<ContatoModel>();
                ContatoModel C = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            C = new ContatoModel();
                            C.ID = (int)reader["ID"];
                            C.nome = reader["nome"].ToString();
                            C.telefone = reader["telefone"].ToString();

                            list.Add(C);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return list;
            }
        }
        
        public override ContatoModel GetById(int id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = string.Format("SELECT [c].[ID], [c].[nome], [c].[telefone] FROM [contatos] c WHERE [c].[ID] = {0}",id);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                ContatoModel c = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                c = new ContatoModel
                                {
                                    ID = (int)reader["Id"],
                                    nome = reader["nome"].ToString(),
                                    telefone = reader["telefone"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return c;
            }
        }

        public override void Save(ContatoModel entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = String.Format("INSERT INTO Contatos (Nome, telefone, CodUsuario) "+
                                           "VALUES ('{0}', '{1}', {2})", entity.nome, entity.telefone, entity.CodUsuario);

                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public override void Update(ContatoModel entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = String.Format("UPDATE Contatos SET Nome='{0}', telefone='{1}' Where Id = {2}", entity.nome, entity.telefone, entity.ID);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", entity.ID);
                cmd.Parameters.AddWithValue("@Nome", entity.nome);
                cmd.Parameters.AddWithValue("@telefone", entity.telefone);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public override void SaveByName(ContatoModel entity, string name)
        {
            var Usuario = UsuarioModel.PegaIdUsuario(name);

            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = String.Format("INSERT INTO Contatos (Nome, telefone, CodUsuario, excluido) " +
                                           "VALUES ('{0}', '{1}', {2}, 0)", entity.nome, entity.telefone, Usuario.ID);

                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}