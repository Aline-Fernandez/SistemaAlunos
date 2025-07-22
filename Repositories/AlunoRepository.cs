using System;
using System.Collections.Generic; // Para usar List<T>
using Microsoft.Data.SqlClient; // Para trabalhar com SQL Server
using SistemaAlunos.Data; // Para acessar a classe Conexao
using SistemaAlunos.Models; // Para acessar a classe Aluno

namespace SistemaAlunos.Repositories {
    public class AlunoRepository {

        public List<Aluno> ListarTodos() {

            List<Aluno> alunos = new List<Aluno>();
            //usando 'using' garante que a conexão será fechada e descartada corretamente
            using (SqlConnection conexao = Conexao.ObterConexao()) {

                // Comando SQL  para selecionar todos os alunos
                string query = "SELECT Id, Nome, DataNascimento, Curso FROM dbo.Alunos";
                using (SqlCommand comando = new SqlCommand(query, conexao)) {

                    conexao.Open(); // Abre a conexão com o banco de dados
                    using (SqlDataReader leitor = comando.ExecuteReader()) {

                        // Lê cada linha retornada pela consulta
                        while (leitor.Read()) {
                            Aluno aluno = new Aluno {
                                Id = (int)leitor["Id"],
                                Nome = leitor["Nome"].ToString(),
                                DataNascimento = (DateTime)leitor["DataNascimento"],
                                Curso = leitor["Curso"].ToString()
                            };
                            alunos.Add(aluno); //adiciona o aluno na lista

                        }
                    }
                }

            }
            return alunos; // Retorna a lista de alunos
        }

        // Método para adicionar um novo aluno ao banco de dados
        public void Adicionar(Aluno aluno) {
            using (SqlConnection conexao = Conexao.ObterConexao()) {

                // Aqui você deve usar "dbo.Alunos" se a tabela estiver no schema dbo
                string query = "INSERT INTO dbo.Alunos (Nome, DataNascimento, Curso) VALUES (@Nome, @DataNascimento, @Curso)";
                using (SqlCommand comando = new SqlCommand(query, conexao)) {

                    //Adiciona os parâmetros ao comando SQL
                    comando.Parameters.AddWithValue("@Nome", aluno.Nome);
                    comando.Parameters.AddWithValue("@DataNascimento", aluno.DataNascimento);
                    comando.Parameters.AddWithValue("@Curso", aluno.Curso);

                    conexao.Open(); //Abre conexao
                    comando.ExecuteNonQuery(); // Executa o comando inserção
                }
            }
        }

        // Método para atualizar um aluno existente no banco de dados
        public void Atualizar(Aluno aluno) {
            using (SqlConnection conexao = Conexao.ObterConexao()) {
                // Comando SQL para atualizar um aluno.
                // A atualização é feita com base no 'Id' do aluno.
                string query = "UPDATE dbo.Alunos SET Nome = @Nome, DataNascimento = @DataNascimento, Curso = @Curso WHERE Id = @Id";
                using (SqlCommand comando = new SqlCommand(query, conexao)) {
                    // Adiciona os valores do objeto Aluno como parâmetros do comando SQL
                    comando.Parameters.AddWithValue("@Nome", aluno.Nome);
                    comando.Parameters.AddWithValue("@DataNascimento", aluno.DataNascimento);
                    comando.Parameters.AddWithValue("@Curso", aluno.Curso);
                    comando.Parameters.AddWithValue("@Id", aluno.Id); // Essencial para saber qual aluno atualizar

                    conexao.Open(); // Abre a conexão
                    comando.ExecuteNonQuery(); // Executa o comando de atualização
                }
            }
        }

        // Método para excluir um aluno do banco de dados
        public void Excluir(int id) {
            using (SqlConnection conexao = Conexao.ObterConexao()) {
                // Comando SQL para excluir um aluno com base no 'Id'.
                string query = "DELETE FROM dbo.Alunos WHERE Id = @Id";
                using (SqlCommand comando = new SqlCommand(query, conexao)) {
                    // Adiciona o Id do aluno como parâmetro
                    comando.Parameters.AddWithValue("@Id", id);

                    conexao.Open(); // Abre a conexão
                    comando.ExecuteNonQuery(); // Executa o comando de exclusão
                }
            }
        }
    } // <-- Esta é a chave correta para fechar a classe AlunoRepository
}