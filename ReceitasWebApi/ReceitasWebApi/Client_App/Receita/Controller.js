
receitaApp.controller('receitaCtrl', function ($scope, receitaService) {

    //busca ingredientes no startup da aplicação
    buscarIngredientes();

    // configurações do angularjs-dropdown-multiselect
    $scope.settings = {
        checkBoxes: true,
        dynamicTitle: false,
        showUncheckAll: false,
        showCheckAll: false,
        externalIdProp: ''
    };

    //objeto que gravará os ingredientes da receita no form de inserção
    $scope.lista_ingr = [];

    // função que realiza a busca de ingredientes
    function buscarIngredientes() {

        var lista_de_Ingredientes = receitaService.getIngredientesDropDown();

        lista_de_Ingredientes.then(function (dados) {

            console.log(dados.data);
            $scope.Ingredientes = dados.data;


        },
            function () {
                alert("Erro ao adquirir Ingredientes!!");
            }
        );
    }    


    // método de listagem de todas as receitas
    $scope.listarReceitas = function () {

        var lista_de_receitas = receitaService.getReceitas();

        lista_de_receitas.then(function (dados) {

            $scope.Receitas = dados.data;

        },
            function () {
                alert("Erro");
            }
        );
            
    }


    //função responsável por adicionar Receita 
    $scope.adicionarReceita = function () {

        //checa se campos obrigatorios foram preenchidos
        if (!($scope.CamposFormularioAreEmpty())) {

            //fecha modal e esconde painel de warning
            $('.modal').modal('hide');
            $scope.ShowAlert = $scope.ShowAlert = false;

            //controi objeto receita
            var receita = {
                receitaId: $scope.receitaId,
                nome: $scope.nome,
                porcoes: $scope.porcoes,
                calorias: $scope.calorias,
                ingredientes: $scope.lista_ingr,
                modoPreparo: $scope.modoPreparo
            }

            
            var adicionaReceita = receitaService.addReceita(receita);

            //chama o servico que por sua vez efetua o post na uri desejada

            adicionaReceita.then(function (dados) {

                if (dados.data.success == true) {

                    alert("Receita adicionada com sucesso !!");
                    $scope.limpaCampos();

                } else {
                    alert("Receita não adicionada. Verifique se todos os campos foram preenchidos!!");
                }
            },
                function () {
                    alert("Erro! Não foi possível adicionar receita!!");
                }

            );

        } else {
            $scope.ShowAlert = $scope.ShowAlert = true;
        }
    }


    //função para limpar os inputs do form de inserção
    $scope.limpaCampos = function () {
        $scope.receitaId = '';
        $scope.nome = '';
        $scope.porcoes = '';
        $scope.calorias = '';
        $scope.ingredientes = '';
        $scope.modoPreparo = '';
        $scope.msg_alerta = '';
    }

    //função que verifica se os campos do formulario foram preenchidos
    $scope.CamposFormularioAreEmpty = function () {

        if (typeof $scope.lista_ingr === 'undefined' || $scope.lista_ingr.length === 0) {
            return true;
        }

        if (typeof $scope.nome === 'undefined' || $scope.nome.length === 0) {
            return true;
        }

        if (typeof $scope.porcoes === 'undefined' || $scope.porcoes.length === 0) {
            return true;
        }

        if (typeof $scope.calorias === 'undefined' || $scope.calorias.length === 0) {
            return true;
        }

        if (typeof $scope.modoPreparo === 'undefined' || $scope.modoPreparo.length === 0) {
            return true;
        }

        return false;
    }

    $scope.ShowAlert = false;
    $scope.IsVisible = false;

    //função responsável por mostrar/esconder a tabela/listagem de receitas
    $scope.showIngredientesTable = function (show) {
        $scope.IsVisible = $scope.IsVisible = show;
    }

});