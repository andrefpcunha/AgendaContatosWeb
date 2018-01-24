using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace AgendaWeb.Models
{
    public class UsuarioModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(30, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 5)]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [StringLength(6, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 3)]
        public string senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme")]
        [Compare("senha", ErrorMessage = "Senha e Confirma não são iguais")]
        [StringLength(6, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 3)]
        public string ConfirmaSenha { get; set; }


        public static UsuarioModel ValidarUsuario(string login, string senha)
        {
            var StringConnection = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = String.Format("select * from usuarios where email='{0}' and senha='{1}'", login, CriptoHelper.HashMD5(senha));
                SqlCommand cmd = new SqlCommand(sql, conn);

                UsuarioModel User = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                User = new UsuarioModel
                                {
                                    ID = (int)reader["Id"],
                                    nome = reader["nome"].ToString(),
                                    email = reader["email"].ToString(),
                                    senha = reader["senha"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                return User;
            }

        }

        public static UsuarioModel PegaIdUsuario(string nome)
        {
            var StringConnection = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = String.Format("select * from usuarios where email ='{0}'", nome);

                SqlCommand cmd = new SqlCommand(sql, conn);

                UsuarioModel User = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                User = new UsuarioModel
                                {
                                    ID = (int)reader["Id"],
                                    nome = reader["nome"].ToString(),
                                    email = reader["email"].ToString(),
                                    senha = reader["senha"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                return User;
            }
        }



        public class UsuarioRepositorio : Repositorio<UsuarioModel, int>
        {
            public override void Delete(UsuarioModel entity)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "DELETE Usuarios Where ID=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", entity.ID);
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
                    string sql = "DELETE Usuarios Where ID=@Id";
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

            public override List<UsuarioModel> GetAll()
            {
                string sql = "Select ID, nome, email, senha FROM Usuarios ORDER BY Nome";
                using (var conn = new SqlConnection(StringConnection))
                {
                    var cmd = new SqlCommand(sql, conn);
                    List<UsuarioModel> list = new List<UsuarioModel>();
                    UsuarioModel U = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                U = new UsuarioModel();
                                U.ID = (int)reader["Id"];
                                U.nome = reader["Nome"].ToString();
                                U.email = reader["Email"].ToString();
                                U.senha = reader["senha"].ToString();

                                list.Add(U);
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

            public override UsuarioModel GetById(int id)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "Select ID, nome, email, senha FROM Usuarios WHERE ID=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    UsuarioModel p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    p = new UsuarioModel();
                                    p.ID = (int)reader["Id"];
                                    p.nome = reader["Nome"].ToString();
                                    p.email = reader["Email"].ToString();
                                    p.senha = reader["Senha"].ToString();
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    return p;
                }
            }

            public override List<UsuarioModel> GetByName(string nome)
            {
                string sql = string.Format("SELECT ID, nome, email, senha " +
                                           "FROM [usuarios] " +
                                           "WHERE [email] = '{0}'", nome);

                using(var conn = new SqlConnection(StringConnection))
                {
                    var cmd = new SqlCommand(sql, conn);
                    List<UsuarioModel> list = new List<UsuarioModel>();
                    UsuarioModel U = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                U = new UsuarioModel();
                                U.ID = (int)reader["Id"];
                                U.nome = reader["Nome"].ToString();
                                U.email = reader["Email"].ToString();
                                U.senha = reader["senha"].ToString();

                                list.Add(U);
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

            public override void Save(UsuarioModel entity)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = String.Format("INSERT INTO Usuarios (Nome, Email, senha) VALUES ('{0}', '{1}', '{2}')", entity.nome, entity.email, CriptoHelper.HashMD5(entity.senha));
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

            public override void SaveByName(UsuarioModel entity, string name)
            {
                throw new NotImplementedException();
            }

            public override void Update(UsuarioModel entity)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "UPDATE Usuarios SET nome=@Nome, email=@Email, senha=@senha Where Id=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", entity.ID);
                    cmd.Parameters.AddWithValue("@Nome", entity.nome);
                    cmd.Parameters.AddWithValue("@Email", entity.email);
                    cmd.Parameters.AddWithValue("@senha", entity.senha);
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
}