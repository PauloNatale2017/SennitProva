
var app = angular.module('App', ['blockUI']);

var Objectos;

var token = "";

app.controller("CtrlLogin", ['$scope', '$http', '$location', '$window','blockUI' ,'$timeout',
function ($scope, $http, $location, $window, blockUI, $timeout)
{
     //var myBlockUI = blockUI.instances.get('myBlockUI');


    blockUI.start("LOADIN...");
    $timeout(function () {
        blockUI.stop();
    }, 4000);
    Objectos = $scope;


   

    $scope.GetToken = function () {
        
        $.ajax({
                type: "POST",
                url: "http://localhost:54992/oauth2/token", 
                data:
                {
                    "grant_type": "password",
                    "username": "admin",
                    "password": "admin",
                    "client_id":"e84a2d13704647d18277966ec839d39e:e84a2d13704647d18277966ec839d39e"
                   
                },
                success: function (data)
                {
                    token = data.access_token;
                   // window.location.href = 'http://localhost:54992/Home/Index'
                },
                error: function (data) {
                   // window.location.href = 'http://localhost:54992/Home/Login'
                }
            });
    }
    $scope.GetToken();


    $scope.CadastrarUse = function () {
        window.location.href = 'http://localhost:54992/Home/CUPONSCLIENTES';
    }
               
    $scope.Login = function () {         
           
             blockUI.start("LOADIN...");
             var path = window.location.origin;
             var config = {
                 headers: {
                     "Accept": "application/json",
                     "Content-Type": "application/json",
                     "Authorization": "bearer " + token
                 }
             };

             if ($scope.LoginAccount == undefined || $scope.LoginAccount.User == undefined || $scope.LoginAccount.Password == undefined)
             {
                 blockUI.stop();
                 blockUI.start("USUARIO e SENHA DEVEM SER PREENCHIDOS...");
                 $timeout(function () {
                     blockUI.stop();
                     blockUI.stop();
                 }, 3000);
             }
             else {
                 $http.post(path + '/api/webapi/login/getAccount', $scope.LoginAccount, config)
                 .then(function (data)
                 {
                     var retorno = data.data;
                     if (data.data == null)
                     {

                         $scope.LoginAccount.User = "";
                         $scope.LoginAccount.Password = "";
                         
                         blockUI.start("Usuario não Cadastrado");

                         $("#register-form").delay(100).fadeIn(100);
                         $("#login-form").fadeOut(100);
                         $('#login-form-link').removeClass('active');
                         $('#recover-form-link').removeClass('active');
                         $(this).addClass('active');
                         e.preventDefault();
                         blockUI.stop();
                         $timeout(function () {
                             blockUI.stop();
                         }, 3000);

                     }
                     else {
                         if (retorno.access == "user") {
                             blockUI.start("Login efetuado com sucesso, boa sorte...");
                             $timeout(function () {
                                 blockUI.stop();
                                 window.location.href = 'http://localhost:54992/Home/CUPONSCLIENTES';
                             }, 3000);
                         } else {
                             blockUI.start("...Login efetuado com sucesso, Bem vindo ...");
                             $timeout(function () {
                                 blockUI.stop();
                                 window.location.href = 'http://localhost:54992/Home/Index?ID=Admin';
                             }, 3000);

                         }

                     }
                 }
                      , function (error) {
                          blockUI.stop();
                          blockUI.start("...Erro ao efetuar login,tente novamente...");
                          $timeout(function () {
                              blockUI.stop();
                          }, 3000);
                      });
             }
     }

    $scope.Cadastro = function ()
     {        
         var path = window.location.origin;
         var Cliente = $scope.Cliente;
         var config = {
             headers: {
                 "Accept": "application/json",
                 "Content-Type": "application/json",
                 "Authorization": "bearer " + token
             }
         };
         if ($scope.Cliente == undefined || Cliente.Nome == undefined || Cliente.Email == undefined || Cliente.CPF == undefined ||  Cliente.telefone == undefined)
         {
             blockUI.stop();
             blockUI.start("TODOS OS CAMPOS SÃO OBRIGATORIOS E DEVEM SER PREENCHIDOS...");
             $timeout(function () {
                 blockUI.stop();
             }, 3000);
         }
         else {

             $http.post(path + '/api/webapi/login/Cadastrar', $scope.Cliente, config)
            .then(function (request)
            {
                if (request.data == "OK")
                {
                    blockUI.start("Cadastro efetuado com sucesso...");
                    $timeout(function () {
                        blockUI.stop();
                        window.location.href = 'http://localhost:54992/Home/Login';
                    }, 3000);
                    
                }
                else
                {
                    $timeout(function () {
                        blockUI.stop();
                    }, 3000);
                    alert(request.data)
                }
            },
            function (error) {
                alert(error.data);
            });
         }
    }

    $scope.LoginS = function () {

        var path = window.location.origin;

        var config = {
            headers:{
                "Accept":"application/json",
                "Content-Type":"application/json",
                "Authorization": "bearer " + token
            }                      
        };

        $http.post(path + '/api/webapi/login/getAccount', $scope.LoginAccount, config).then(function (data) {
            var retorno = data;
            if (data.data == null)
            {
                $scope.LoginAccount.User = "";
                $scope.LoginAccount.Password = "";
                blockUI.start("Usuario não Cadastrado");
                //alert("Usuario não Cadastrado");

                $("#register-form").delay(100).fadeIn(100);
                $("#login-form").fadeOut(100);
                $('#login-form-link').removeClass('active');
                $('#recover-form-link').removeClass('active');
                $(this).addClass('active');
                e.preventDefault();
                blockUI.stop();
                $timeout(function () {
                    blockUI.stop();
                }, 3000);
            }
            else {
                blockUI.start("Cadastro efetuado com sucesso,Usuario e senha enviados para seu e-mail");
                window.location.href = 'http://localhost:54992/Home/Index';
            }
        },
       function (error) {
           alert(error.data)
       });

    }
   
}]);