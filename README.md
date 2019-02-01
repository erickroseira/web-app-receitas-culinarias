# RECEITAS CULINÁRIAS

Projeto .NET Web API MVC que permite a criação e visualização de receitas culinárias. Além disto  o projeto provê as seguintes ações:

    - Criar uma receita:

        A criação de uma receita se trata de um form com os seguintes campos:

            - Nome

            - Porções (quantas pessoas a receita serve)

            - Calorias

            - Ingredientes (Uma lista dos ingredientes necessários)

            - Modo de preparo (Um texto de como preparar a receita)

    - Visualizar receitas:

        Esta tela deve listar todas as receitas inseridas e ver seus detalhes.

As APIs providas são:
              
    1 - POST /receita (inicializa uma receita)
    
    2 - GET /receitas (devolve todas as receitas em memória)

    3 - GET /receitas/{id} (devolve uma receita em memória por id)

    4 - GET /receitas/{id}/ingredientes (devolve os ingredientes de uma receita)

    5 - GET /receitas/ingredientes/{id} (devolve receitas que contenham o ingrediente definido por id)

    6 - GET /receitas/ingredientes (devolve todos os ingredientes utilizados em receitas)

    7 - GET /ingredientes (devolve os ingredientes disponíveis, ordenados alfabeticamente)
    

## PRIMEIROS PASSOS

As instruções abaixo servirão como um guia de como baixar o projeto em sua máquina local e rodá-lo para propósitos de desenvolvimento e/ou testes.

### Pré-requisitos

É necessário que você tenha o **.NET Framework** instalado em seu computador, bem como o Visual Studio. É necessário que no Visual Studio estejam instalados os recursos de **ASP.NET e desenvolvimento WEB**.

## EXECUÇÃO DO PROJETO

Abra o Projeto no Visual Studio, compile o projeto acessando **Compilação > Compilar Solução** ou com o atalho **Ctrl+Shif+B**. Em seguida clique em rodar o programa selecionando **Depurar > Iniciar Sem Depurar** ou por meio das teclas de Atalho **Ctrl+F5**. A base de dados com a qual o programa se comunica será criada automaticamente. A base de dados será então populada com 20 ingredientes. Devido o fato de o banco ser criado no momento do startup da aplicação bem como a inserção dos ingredientes é necessário aguardar alguns segundos (~5-15) para que as operações no banco sejam realizadas satisfatoriamente.


## EXECUÇÃO DOS TESTES UNITÁRIOS

O projeto possui testes unitários, construídos utilizando o framework de testes NUnit. Para rodá-los clique em Teste > Executar > Todos os testes ou pelas tecla de atalho **Ctrl+R, A**.

## POSSÍVEIS INFORMAÇÕES ÚTEIS:

O Projeto foi desenvolvido utilizando as seguintes tecnologias:

- Visual Studio 2017
- .NET Framework 4.6.
- Entity Framework 6.2.0
- NUnit 3.11.0
- AngularJS 
- SQL Server
