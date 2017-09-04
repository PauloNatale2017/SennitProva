
var app = angular.module('App', ['blockUI']);
var Objectos;
var token = "";



app.controller("CtrlCupon", ['$scope', '$http', '$location', '$window','blockUI' ,'$timeout',
function ($scope, $http, $location, $window, blockUI, $timeout)
{
    blockUI.start("...LOADING...");
    $scope.GetCupons = function ()
    {
        var path = window.location.origin;
        $http.get(path + "/api/webapi/Cupon/GetCupons").then(function (response)
        {
            if (response.data == null)
            {
                blockUI.stop();
                blockUI.start("...NÃO A CUPONS CADASTRADOS...");
                $timeout(function () {
                    blockUI.stop();
                }, 3000);
            }
            else
            {                
                $timeout(function () {
                    $scope.Cupons = response.data;
                    blockUI.stop();
                }, 3000);
            }
           
       });
    }
    $scope.GetCupons();

    $scope.CadastroCupon = function()
    {
        $scope.Cupon.CodigoCupon = getRandomSpan();

        blockUI.start("...LOADING...");
        var path = window.location.origin;
        $http.post(path + "/api/webapi/Cupon/CadastroCupon", $scope.Cupon).then(function (response) {
           
            if (response.data == true)
            {
                blockUI.stop();
                blockUI.start("...CADASTRO EFETUADO COM SUCESSO...");
                $timeout(function () {                    
                    window.location.href = 'http://localhost:54992/Home/Cupons';                    
                }, 3000);
            }
            else
            {
                blockUI.start("...ERRO INESPERADO...");
                $timeout(function () {
                    blockUI.stop();
                }, 3000);
            }
        });
    }
    function getRandomSpan() {
        return Math.floor((Math.random() * 100000) + 1);
    };


  
    
}]);