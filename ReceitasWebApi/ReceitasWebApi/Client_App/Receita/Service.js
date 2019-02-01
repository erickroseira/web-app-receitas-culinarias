receitaApp.service('receitaService', function ($http) {


    //Método que devolve todas as receitas em memória
    this.getReceitas = function () {
        return $http.get('/receitas');
    }

    //Método que inicializa uma receita
    this.addReceita = function (receita) {
        var request = $http({
            method: 'post',
            url: '/receita',
            data: receita
        });

        return request;
    }


    //Método que devolve todas as receitas em memória especificamente no formato do angularjs-dropdown-multiselect
    this.getIngredientesDropDown = function () {
        return $http.get('/ingredientesNgDropDown');
    }

});
