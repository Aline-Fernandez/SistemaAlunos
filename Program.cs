using System;
using System.Collections.Generic;
using SistemaAlunos.Models;
using SistemaAlunos.Repositories; // Para acessar a classe AlunoRepository

namespace SistemaAlunos {
    class Program {
        static void Main(string[] args) {
            AlunoRepository alunoRepository = new AlunoRepository();
            int opcao;

            do {
                Console.WriteLine("\n--- Sistema de Gestão de Alunos ---");
                Console.WriteLine("1. Listar Alunos");
                Console.WriteLine("2. Adicionar Novo Aluno");
                Console.WriteLine("3. Sair");
                Console.Write("Escolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao)) {
                    switch (opcao) {
                        case 1:
                            ListarAlunos(alunoRepository);
                            break;
                        case 2:
                            AdicionarAluno(alunoRepository);
                            break;
                        case 3:
                            Console.WriteLine("Saindo do sistema. Até mais!");
                            break;
                        default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                    }
                }
                else {
                    Console.WriteLine("Entrada inválida. Por favor, digite um número.");
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();

            } while (opcao != 3);
        }

        static void ListarAlunos(AlunoRepository repository) {
            Console.WriteLine("\n--- Lista de Alunos ---");
            List<Aluno> alunos = repository.ListarTodos();

            if (alunos.Count == 0) {
                Console.WriteLine("Nenhum aluno cadastrado.");
                return;
            }

            foreach (var aluno in alunos) {
                Console.WriteLine($"ID: {aluno.Id}, Nome: {aluno.Nome}, Data Nasc.: {aluno.DataNascimento:dd/MM/yyyy}, Curso: {aluno.Curso}");
            }
        }

        static void AdicionarAluno(AlunoRepository repository) {
            Console.WriteLine("\n--- Adicionar Novo Aluno ---");
            Aluno novoAluno = new Aluno();

            Console.Write("Nome: ");
            novoAluno.Nome = Console.ReadLine();

            Console.Write("Data de Nascimento (DD/MM/AAAA): ");
            // Tenta converter a string para DateTime
            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataNascimento)) {
                novoAluno.DataNascimento = dataNascimento;
            }
            else {
                Console.WriteLine("Formato de data inválido. Usando data padrão (hoje).");
                novoAluno.DataNascimento = DateTime.Today; // Data padrão em caso de erro
            }

            Console.Write("Curso: ");
            novoAluno.Curso = Console.ReadLine();

            try {
                repository.Adicionar(novoAluno);
                Console.WriteLine("Aluno adicionado com sucesso!");
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro ao adicionar aluno: {ex.Message}");
                Console.WriteLine("Detalhes do erro: " + ex.ToString()); // Para ver o erro completo, útil em desenvolvimento
            }
        }
    }
}