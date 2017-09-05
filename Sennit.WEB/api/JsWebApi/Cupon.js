
var app = angular.module('App', ['blockUI']);
var Objectos;
var token = "";



app.controller("CtrlCupon", ['$scope', '$http', '$location', '$window','blockUI' ,'$timeout',
function ($scope, $http, $location, $window, blockUI, $timeout)
{

    blockUI.start("...LOADING...");

    $scope.GetToken = function () {
        $.ajax({
            type: "POST",
            url: "http://localhost:54992/oauth2/token",
            data:
            {
                "grant_type": "password",
                "username": "admin",
                "password": "admin",
                "client_id": "e84a2d13704647d18277966ec839d39e:e84a2d13704647d18277966ec839d39e"

            },
            success: function (data) {
                token = data.access_token;
                // window.location.href = 'http://localhost:54992/Home/Index'
            },
            error: function (data) {
                // window.location.href = 'http://localhost:54992/Home/Login'
            }
        });
    }
    $scope.GetToken();


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
    
    $scope.CadastroCuponPorClienteGet = function ()
    {
        blockUI.start("...LOADING...");
        var path = window.location.origin;
        var config = {
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
                "Authorization": "bearer " + token
            }
        };
        if ($scope.CUPONCADASTRO.CPF == null || $scope.CUPONCADASTRO.CODIGOCUPON == null) {
            blockUI.stop();
            blockUI.start("...UM DOS CAMPOS CPF/CODIGO DO CUPON ESTÃO VAZIOS ...");
            $timeout(function () {
                blockUI.stop();
            }, 3000);
        }
        else
        {
            $http.post(path + "/api/webapi/Cupon/CadastroCuponPorClienteGet", $scope.CUPONCADASTRO, config).then(function (response)
            {
                if (response.data.Clientes.length == 0){
                    blockUI.stop();
                    blockUI.start("...CPF NÃO CADASTRADO, SERA REDIRECIONADO PARA PAGINA DE CADASTRO ...");
                    $timeout(function () {
                        blockUI.stop();
                        window.location.href = 'http://localhost:54992/Home/Login';
                    }, 3000);
                }
                else if (response.data.cupons.length == 0) {
                    blockUI.stop();
                    blockUI.start("...CUPON INVALIDO ...");
                    $timeout(function () {
                        blockUI.stop();
                        //window.location.href = 'http://localhost:54992/Home/Login';
                    }, 3000);
                }
                else if (response.data.cupons[0].PREMIO_SORTEADO == false && response.data.cupons[0].CUPON_PREMIADO == true){
                    blockUI.start(" PARABENS SEU CUPON FOI SORTEADO "
                                + " Sr(a)" + response.data.Clientes[0].Nome + " "
                                + " seu premio é " + response.data.cupons[0].Premio);

                                  
                    $http.post(path + "/api/webapi/Cupon/CadastroCuponPorCliente", $scope.CUPONCADASTRO, config).then(function (response) {
                            var dadosCliente = response.data;

                            if (response.data.Id_usuario != null)
                            {                               
                                $timeout(function () {
                                    $scope.Clear();
                                    blockUI.stop();
                                }, 3000);
                            }
                            else
                            {
                                blockUI.stop();
                                blockUI.start("...ERROR INESPERADO...");
                                $timeout(function () {
                                    $scope.Clear();
                                    blockUI.stop();
                                }, 3000);
                            }
                        });
                    

                    blockUI.stop();
                    $timeout(function () {
                        blockUI.stop();
                        //window.location.href = 'http://localhost:54992/Home/Cupons';
                    },6000);

                    //$http.post(path + "/api/webapi/Cupon/CadastroCuponPorCliente", $scope.CUPONCADASTRO).then(function (response) {
                    //    var dadosCliente = response.data;

                    //    if (response.data.nome_usuario != null) {
                    //        blockUI.stop();
                    //        blockUI.start("...CADASTRO EFETUADO COM SUCESSO,BOA SORTE !!!...");
                    //        $timeout(function () {
                    //            window.location.href = 'http://localhost:54992/Home/Cupons';
                    //        }, 3000);
                    //    }
                    //    else {
                    //        blockUI.stop();
                    //        blockUI.start("...ERROR INESPERADO...");
                    //        $timeout(function () {
                    //            blockUI.stop();
                    //        }, 3000);
                    //    }
                    //});
                }
                else if (response.data.Clientes[0].QtdCuponsCadastrados == 5) {
                    blockUI.stop();
                    blockUI.start("...NUMERO TOTAL DE CUPONS CADASTRADOS ATINGIDO...");
                    $timeout(function () 
                    {
                        $scope.Clear();
                        blockUI.stop();
                        //window.location.href = 'http://localhost:54992/Home/Cupons';
                    }, 3000);
                }
                else if (response.data.cupons[0].PREMIO_SORTEADO == true) {
                    blockUI.stop();
                    blockUI.start("...CUPON JA FOI SORTEADO EM " + response.data.cupons[0].DataSorteado);
                    $timeout(function () {
                        $scope.Clear();
                        blockUI.stop();
                        //window.location.href = 'http://localhost:54992/Home/Cupons';
                    }, 3000);

                }
                else if (response.data.cupons[0].CUPON_PREMIADO == false) {
                    $http.post(path + "/api/webapi/Cupon/CadastroCuponPorCliente", $scope.CUPONCADASTRO, config).then(function (response) {
                        var dadosCliente = response.data;

                        if (response.data.Id_usuario != null)
                        {
                            blockUI.stop();
                            blockUI.start("...CADASTRO EFETUADO COM SUCESSO, BOA SORTE...");
                            $timeout(function () {
                                $scope.Clear();
                                blockUI.stop();
                            }, 3000);
                        }
                        else {
                            blockUI.stop();
                            blockUI.start("...ERROR INESPERADO...");
                            $timeout(function () {
                                $scope.Clear();
                                blockUI.stop();
                            }, 3000);
                        }
                    });
                }
                
            });
        }
    }

    $scope.Clear = function () {
        $scope.CUPONCADASTRO.CPF = "";
        $scope.CUPONCADASTRO.CODIGOCUPON = "";
    }

    $scope.CadastrarUse = function () {
        window.location.href = 'http://localhost:54992/Home/Login'
    }

    $scope.CadastroCuponPorCliente = function () {
       
        blockUI.start("...LOADING...");
        var path = window.location.origin;
        var config = {
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
                "Authorization": "bearer " + token
            }
        };

        var dadosCupon = "";
        var dadosCliente = "";

        $http.post(path + "/api/webapi/Cupon/CadastroCuponPorCliente", $scope.CUPONCADASTRO, config).then(function (response) {
            var dadosCliente = response.data;

            if (response.data == true) {
                blockUI.stop();
                blockUI.start("...CADASTRO EFETUADO COM SUCESSO...");
                $timeout(function () {
                    window.location.href = 'http://localhost:54992/Home/Cupons';
                }, 3000);
            }
            else {
                blockUI.stop();
                blockUI.start("...ERROR INESPERADO...");
                $timeout(function () {
                    blockUI.stop();
                }, 3000);
            }
        });      
    }

    $scope.CadastroCupon = function()
    {
        $scope.Cupon.CodigoCupon = getRandomSpan();

        blockUI.start("...LOADING...");
        var path = window.location.origin;
        var config = {
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
                "Authorization": "bearer " + token
            }
        };
        $http.post(path + "/api/webapi/Cupon/CadastroCupon", $scope.Cupon, config).then(function (response) {
           
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
                blockUI.stop();
                myBlockUI.start();
                $timeout(function () {
                    myBlockUI.stop();
                }, 3000);
            }
        });
    }

    function getRandomSpan() {
        return Math.floor((Math.random() * 100000) + 1);
    };    
}]);