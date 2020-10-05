using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CRUDAgenda
{
    class Program
    {
        // Lista de objetos no documento txt
        static List<int> ListaID;
        static List<string> ListaNome;
        static List<int> ListaTelefone;
        static List<string> ListaEmail;

        // variaveis globais (fora das substrings)
        static string Nome;
        static int Telefone;
        static string Email;
        static int ID;
        static int ContadorContatos;
        static char NovoContato = 'S';
        static StreamWriter EscreverTxt;
        static StreamReader LerTxt;
        static string CaminhoDocumentoTxT = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Backup.txt";

        //usado em ArmazenarDocumento
        static int Contador = 0;
        static string[] VetorArquivo = { };

        static void Main(string[] args)
        {
            Menu();
        }
        // Essa parte ele vai ler o documendo Backup.txt
        static void ArmazenarDocumento()                                     
        {
            // Vamos ver se Backup.txt existe
            if (!File.Exists(CaminhoDocumentoTxT))
            {
                EscreverTxt = new StreamWriter(CaminhoDocumentoTxT);
                EscreverTxt.Dispose();
            }
            // vetores para ID, Nome, Telefone e Email
            ListaID = new List<int>();
            ListaNome = new List<string>();
            ListaTelefone = new List<int>();
            ListaEmail = new List<string>();

            // essa parte ele vai fazer a leitura do Backup.txt
            LerTxt = new StreamReader(CaminhoDocumentoTxT);
            while (!LerTxt.EndOfStream)
            {
                string LinhaNoTxt = LerTxt.ReadLine();
                if (LinhaNoTxt.Contains("-"))
                {
                    ListaID.Add(Contador);
                    Contador++;
                }
                if (LinhaNoTxt.Contains(':'))
                {VetorArquivo = LinhaNoTxt.Split(':');}
                if (LinhaNoTxt.Contains("Nome"))
                {ListaNome.Add(VetorArquivo[1].Trim());}
                if (LinhaNoTxt.Contains("Telefone"))
                {ListaTelefone.Add(int.Parse(VetorArquivo[1].Trim()));}
                if (LinhaNoTxt.Contains("Email"))
                {ListaEmail.Add(VetorArquivo[1].Trim());}
            }
            LerTxt.Dispose();
        }
        // vai escrever a informacao no arquivo txt que é o Backup
        static void EscreverDocumento()                                     
        {
            // Escreve dentro de Backup.txt e é a mensagem que é linda no campo Pesquisar();
            EscreverTxt = new StreamWriter(CaminhoDocumentoTxT, false);
            Console.Clear();
            for (int i = 0; i < ListaID.Count; i++)
            {
                EscreverTxt.WriteLine("-------------------------------");
                EscreverTxt.WriteLine("ID: " + ListaID[i]);
                EscreverTxt.WriteLine("Nome: " + ListaNome[i]);
                EscreverTxt.WriteLine("Telefone: " + ListaTelefone[i]);
                EscreverTxt.WriteLine("Email: " + ListaEmail[i]);
            }
            EscreverTxt.Dispose();
        }
        static void NovoCadastro() // novo cadastro caso tenha alteracao do cadastro
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("               NOVO CADASTRO           ");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("Pressione xx para voltar");
            Console.WriteLine("");

            Console.Write("Nome: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Nome = Console.ReadLine().ToUpper();                 
            Console.ForegroundColor = ConsoleColor.Gray;
            VoltarAoMenu();

            try
            {
                Console.Write("Telefone: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Telefone = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Gray;
                if (Telefone < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não é permitido números negativos");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ReadKey();
                    Console.Clear();
                    NovoCadastro();
                }
            }
            catch
            {
                MensagemDeErro();
                NovoCadastro();
            }
            Console.Write("Email: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Email = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            // dar uma ollhada
            ListaID.Insert(ID, ContadorContatos);
            ListaNome.Insert(ID, Nome);
            ListaTelefone.Insert(ID, Telefone);
            ListaEmail.Insert(ID, Email);
            ListaID.Clear();
            for (int i = 0; i < ListaNome.Count; i++)
            {
                ListaID.Add(i);
            }
            EscreverDocumento();
        }
        static void Cadastro()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("                CADASTRO            ");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("Pressione xx para voltar");
            Console.WriteLine("");

            while (NovoContato == 'S')  //Novo contato declarado como variavel global
            {
                Console.Write("NOME: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Nome = Console.ReadLine().ToUpper();
                Console.ForegroundColor = ConsoleColor.Gray;
                if (Nome == "XX" || Nome == "xx")
                {
                    break;
                }
                try
                {
                    Console.Write("TELEFONE: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Telefone = int.Parse(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                catch
                {
                    MensagemDeErro();
                    continue;
                }
                try
                {
                    Console.Write("EMAIL: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Email = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                catch
                {
                    MensagemDeErro();
                    continue;
                }
                Console.WriteLine("\nDeseja Adicionar mais algum contato? (S|N)");

                NovoContato = char.Parse(Console.ReadLine().ToUpper());
                if (NovoContato != 'S' && NovoContato != 'N')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Digite apenas S para Sim e N para NÃO");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("PRECIONE PARA CONTINUAR");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                ListaID.Add(ContadorContatos);
                ListaNome.Add(Nome);
                ListaTelefone.Add(Telefone);
                ListaEmail.Add(Email);
                ListaID.Clear();
                for (int i = 0; i < ListaNome.Count; i++)
                {
                    ListaID.Add(i);
                }
                ContadorContatos++;
                EscreverDocumento();
            }
        }
        static void Pesquisar()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("          LISTA DE CADASTRADOS         ");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("Pressione uma tecla para voltar");
            Console.WriteLine("");

            LerTxt = new StreamReader(CaminhoDocumentoTxT);
            while (!LerTxt.EndOfStream)
            {
                Console.WriteLine(LerTxt.ReadLine());
            }
            LerTxt.Dispose();
            Console.ReadKey();
        }
        static void Deletar()
        {
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("            REMOVER CADASTRO           ");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("Pressione xx para voltar");
            Console.WriteLine("");

            Console.Write("Nome do Usuário: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Nome = Console.ReadLine().ToUpper();
            Console.ForegroundColor = ConsoleColor.Gray;
            VoltarAoMenu();
            for (int i = 0; i < ListaID.Count; i++)
            {
                if (Nome == ListaNome[i])
                {
                    Console.WriteLine("------------------------------");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("ID do Contato: " + ListaID[i]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Nome: " + ListaNome[i]);
                    Console.WriteLine("Telefone: " + ListaTelefone[i]);
                    Console.WriteLine("Email: " + ListaEmail[i]);
                }
            }
            Console.Write("ID do Contato: ");
            {
                Console.ForegroundColor = ConsoleColor.Red;
                ID = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Gray;
                try
                {
                    if (ID < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nâo sâo permitidos numeros negativos");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção Invalida");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ReadKey();
                }
            }
            ListaID.RemoveAt(ID);
            ListaNome.RemoveAt(ID);
            ListaTelefone.RemoveAt(ID);
            ListaEmail.RemoveAt(ID);
            ListaID.Clear();
            for (int  i = 0;  i < ListaNome.Count;  i++)
            {
                ListaID.Add(i);
            }
            EscreverDocumento();
        }
        static void Alterar()
        {
            Console.Clear();
          
            Console.WriteLine("");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("            ALTERAR CADASTRO           ");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("Pressione xx para voltar");
            Console.WriteLine("");

            foreach (string item in ListaNome)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(item);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.Write("\nNome do Usuario Cadastrado: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Nome = Console.ReadLine().ToUpper();
            Console.ForegroundColor = ConsoleColor.Gray;
            VoltarAoMenu();

            for (int i = 0; i < ListaNome.Count; i++)
            {
                    if (Nome == ListaNome[i])
                    {
                        Console.WriteLine("═══════════════════════════════════════");
                        Console.WriteLine("           CONTATO CADASTRADO          ");
                        Console.WriteLine("═══════════════════════════════════════");
                        Console.WriteLine("ID: " + ListaID[i]);
                        Console.WriteLine("Nome: " + ListaNome[i]);
                        Console.WriteLine("Telefone: " + ListaTelefone[i]);
                        Console.WriteLine("Email: " + ListaEmail[i]);
                        Console.WriteLine("");
                    }
            }
            Console.Write("Escreva o ID do contato que deseja editar:  ");
            {
                try
                {
                Console.ForegroundColor = ConsoleColor.Green;
                ID = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Gray;
                    if (ID < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nâo sâo permitidos numeros negativos");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.ReadKey();
                        Console.Clear();
                        Alterar();
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção Inválida");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ReadKey();
                    Console.Clear();
                    Alterar();
                }
            }
            ListaID.RemoveAt(ID);
            ListaNome.RemoveAt(ID);
            ListaTelefone.RemoveAt(ID);
            ListaEmail.RemoveAt(ID);

            Console.Clear();
            NovoCadastro();
        }
        static void Menu()
        {
            ArmazenarDocumento();

            int opcao = 0;
            bool RepeticaoMenu = true;
            while (RepeticaoMenu)
            {
                // Menu
                #region
                Console.Clear();
                Console.WriteLine("╔═════════════════════════════════════════════════════╗");
                Console.WriteLine("   //////////////////// AGENDA  ////////////////////");
                Console.WriteLine("");
                Console.WriteLine("                 1 - ADICIONAR");
                Console.WriteLine("                 2 - ALTERAR");
                Console.WriteLine("                 3 - REMOVER");
                Console.WriteLine("                 4 - PESQUISAR");
                Console.WriteLine("                 0 - FIM");
                Console.WriteLine("╚═════════════════════════════════════════════════════╝");
                #endregion
                try
                {
                    Console.Write("Digite a Opcao Desejada: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    opcao = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                catch
                {
                    MensagemDeErro();
                    continue;
                }
                switch (opcao)
                {
                    case 1:
                        Cadastro();
                        continue;
                    case 2:
                        Alterar();
                        continue;
                    case 3:
                        Deletar();
                        continue;
                    case 4:
                        Pesquisar();
                        continue;
                    case 0:
                        RepeticaoMenu = false;
                        continue;
                    default:
                        MensagemDeErro();
                        continue;
                }
            }
        }
        // Subrotina para voltar a tela de menu sempre que o usuário apertar xx
        static void VoltarAoMenu()
        {
            if (Nome == "xx" || Nome == "XX")
            {
                Menu();
            }
        }
        static void MensagemDeErro()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Opção Inválida");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadKey(); Console.Clear();
        }
        #region Parte da Logo IBRATEC
        class EText
             {
                 int x, X, y, index, k, l;
                 string s;
                 ConsoleColor[] cl;
                 ConsoleColor cl1, cl2;
                 Random r;
                 int iColor, nColor;
                 public EText(string s, int x, int y)
                 {
                     this.x = x;
                     this.y = y;
                     X = x;
                     k = 0;
                     this.s = s;
                     l = s.Length;
                     index = l - 1;
                     cl = new ConsoleColor[] { ConsoleColor.Red };
                     nColor = cl.Length;
                     cl1 = ConsoleColor.Black;
                     cl2 = ConsoleColor.Blue;
                     r = new Random();
                     iColor = 0;
                 }
                 public void ve()
                 {

                     Console.SetCursorPosition(X, y);
                     for (int i = k; i < l; i++)
                     {
                         Console.ForegroundColor = cl1;
                         if (i == index)
                         {
                             Console.ForegroundColor = cl2;
                         }

                         Console.Write(s[i]);

                     }

                     if (index == k)
                     {
                         k++;
                         X++;
                         index = l;
                     }
                     if (k == l - 1)
                     {
                         k = 0;
                         X = x;
                         cl1 = cl2;
                         cl2 = cl[iColor];
                         iColor++;
                         if (iColor == nColor)
                         {
                             iColor = 0;
                         }
                     }
                     index--;

                 }

             }
                 static void ibratec()
                 {
                     Console.CursorVisible = false;
                     string[] str = new string[] {"██╗██████╗ ██████╗  █████╗ ████████╗███████╗ ██████╗",
                                                  "██║██╔══██╗██╔══██╗██╔══██╗╚══██╔══╝██╔════╝██╔════╝",
                                                  "██║██████╔╝██████╔╝███████║   ██║   █████╗  ██║     ",
                                                  "██║██╔══██╗██╔══██╗██╔══██║   ██║   ██╔══╝  ██║     ",
                                                  "██║██████╔╝██║  ██║██║  ██║   ██║   ███████╗╚██████╗",
                                                  "╚═╝╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚══════╝ ╚═════╝"};
                     int n = str.Length;

                     EText[] ET = new EText[n];
                     int x, y;
                     x = 15;
                     y = 8;
                     for (int i = 0; i < n; i++)
                     {
                         ET[i] = new EText(str[i], x, y + i);
                     }

                     while (true)
                     {
                         while (true)
                         {
                             foreach (EText et in ET)
                             {
                             et.ve();
                             }
                         }

                     }
                 }
        #endregion
    }
}


