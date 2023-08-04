using System;
using System.Collections.Generic;

Dictionary<int, (string nome, double preco, int quantidade)> acomodações = new Dictionary<int, (string, double, int)>();

Console.WriteLine(" Controle de pousada");

        // laços
        while (true)
        {
            Console.WriteLine(" Hospedes Cadastrados:");
            Console.WriteLine("|-----------------------");
            if (acomodações.Count == 0)
            {
                Console.WriteLine(" Não ha hospedes no momento");
                Console.WriteLine("|-----------------------");
            }
            else
            {
                foreach (var (acomodação, (nome, preco, quantidade)) in acomodações)
                {
                    Console.WriteLine(" Numero da Acomodacao: {0}", acomodação);
                    Console.WriteLine(" Nome do Cliente: {0}", nome);
                    Console.WriteLine(" Preco da Acomodacao: {0:F2}", preco);
                    Console.WriteLine(" Numero de Clientes: {0}", quantidade);
                    Console.WriteLine("|-----------------------");
                }
            }


            Console.WriteLine("SELECIONE UMA DAS OPCOES ABAIXO:");
            Console.WriteLine("[1] - Cadastrar Novo Cliente");
            Console.WriteLine("[2] - Adicionar mais Clientes a Acomodacao");
            Console.WriteLine("[3] - Realizar Saida de Cliente");
            Console.WriteLine("[4] - Listar Acomodacoes");
            Console.WriteLine("[5] - Remover Acomodação do Sistema");
            Console.WriteLine("[0] - Sair");

            int escolher = int.Parse(Console.ReadLine());

            // bloco de comando para adições

            switch (escolher)
            {
                case 1:

                    Console.WriteLine("Entrada de Cliente na Acomodacao");
                    Console.WriteLine("Digite o Numero da Acomodacao:");
                    int numero = int.Parse(Console.ReadLine());
                    while (acomodações.ContainsKey(numero))
                    {
                        Console.WriteLine("Ja existe Acomodacao cadastrada com o Numero {0}. Por favor, informe outro dado");
                        numero = int.Parse(Console.ReadLine());
                    }

                    Console.WriteLine("Digite o Nome do Cliente:");
                    string nomeCliente = Console.ReadLine();
                    Console.WriteLine("Digite o preco da Diaria da Acomodacao:");
                    double preco = double.Parse(Console.ReadLine());
                    Console.WriteLine("Digite a Quantidade de Clientes;");
                    int quantidade = int.Parse(Console.ReadLine());
                    acomodações[numero] = (nomeCliente, preco, quantidade);
                    Console.WriteLine(" O Cliente | [0] | entrou na Acomodacao com sucesso! ");
                    break;


                case 2:

                    Console.WriteLine("Entrada de outro Cliente na Acomodacao");
                    Console.WriteLine("Digite o Numero da Acomodacao:");
                    int numAcomodação = int.Parse(Console.ReadLine());
                    if (acomodações.ContainsKey(numAcomodação))
                    {
                        Console.WriteLine("Digite a quantidade de Clientes a serem adicionados");
                        int qtd = int.Parse(Console.ReadLine());
                        var (Nome, Preço, Quantidade) = acomodações[numAcomodação];
                        Quantidade += qtd;
                        for (int i = 0; i < qtd; i++)
                        {
                            Console.WriteLine($"Digite o Nome do [i + 1] hospede");
                            string nomCliente = Console.ReadLine();
                            Nome += $", {nomCliente}";
                        }
                        acomodações[numAcomodação] = (Nome, Preço, Quantidade);
                        Console.WriteLine("{0} cliente(s) foram adicionados a acomodacao {1}", qtd, numAcomodação);
                    }
                    else
                    {
                        Console.WriteLine("Nao foi encontrada Acomodacao com o numero {0}", numAcomodação);
                    }
                    break;

                case 3:
                    Console.WriteLine("Saida de Cliente");
                    Console.WriteLine("Digite o Numero da Acomodacao do Cliente:");
                    numero = int.Parse(Console.ReadLine());
                    if (!acomodações.ContainsKey(numero))
                    {
                        Console.WriteLine("O Cliente com o Numero de Acomodacao | {0} | Nao foi localizado!", numero);
                    }
                    else
                    {
                        var (nomeAtual, precoAtual, quantidadeAtual) = acomodações[numero];
                        Console.WriteLine("Cliente Encontrado, Pode Prosseguir");
                        Console.WriteLine("Digite a Quantidade de CLientes que Sairam da Acomodacao:");
                        int saida = int.Parse(Console.ReadLine());
                        if (quantidadeAtual - saida < 0)
                        {
                            Console.WriteLine("Quantidade de Clientes Negativa, favor Corrigir");
                            break;
                        }
                        Console.WriteLine("Digite o nome(s) dos Clientes que saíram da Acomodacao:");
                        List<string> nomes = new List<string>();
                        for (int i = 0; i < saida; i++)
                        {
                            Console.WriteLine($"Nome da {i + 1} cliente:");
                            string nome = Console.ReadLine();
                            if (!nomeAtual.Contains(nome))
                            {
                                Console.WriteLine("O nome nao foi localizado na nossa lista de Clientes");
                                i--;
                                continue;
                            }
                            nomes.Add(nome);
                        }
                        string nomesConcatenados = string.Join(",", nomes);
                        Console.WriteLine("{0} Os Clientes {1} sairam da Acomodacao", saida);
                        acomodações[numero] = (nomeAtual, precoAtual, quantidadeAtual - saida);

                        if (quantidadeAtual - saida == 0)
                        {
                            Console.WriteLine("Todos os CLientes sairam da acomodacao {0}");
                            acomodações.Remove(numero);
                        }
                        else
                        {
                            foreach (string nome in nomes)
                            {
                                nomeAtual = string.Join(", ", nomeAtual.Split(", ").Where(n => n != nome));
                            }
                            acomodações[numero] = (nomeAtual, precoAtual, quantidadeAtual - saida);
                            Console.WriteLine("Os CLientes {0} saíram da acomodacao {1}.", nomesConcatenados, numero);
                        }
                    }
                    break;

                case 4:
                    Console.WriteLine("LISTA DAS ACOMODACOES NO SISTEMA");
                    Console.WriteLine("|-----------------------|");
                    if (acomodações.Count == 0)
                    {
                        Console.WriteLine(" NAO HA ACOMODACOES NO SISTEMA");
                        Console.WriteLine("|-----------------------|");
                    }
                    else
                    {
                        foreach (var (acomodação, nome) in acomodações)
                        {
                            Console.WriteLine(" Numero da Acomodacao: {0}", acomodação);
                            Console.WriteLine(" Nome do Cliente: {0}", nome);
                            Console.WriteLine("|-----------------------|");
                        }
                    }
                    break;

                case 5:
                    Console.WriteLine("Remover Acomodação do Sistema");
                    Console.WriteLine(" Digite o Numero da Acomodação:");
                    numero = int.Parse(Console.ReadLine());
                    if (!acomodações.ContainsKey(numero))
                    {
                        Console.WriteLine(" A Acomodação de numero {0} não foi Encontrada", numero);
                    }
                    else
                    {
                        acomodações.Remove(numero);
                        Console.WriteLine("A Acomodação de Numero {0} foi retirada!", numero);
                    }
                    break;

            }
        }