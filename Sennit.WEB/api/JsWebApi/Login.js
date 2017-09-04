var app = angular.module('App', []);

var Objectos;

 var token = "";
 
 //var config = {
 //           params: data,
 //           headers: { 'Accept': 'application/json' }
 //};

 app.controller("CtrlLogin", ['$scope', '$http', '$location', '$window', function ($scope, $http, $location, $window) {

     //var myBlockUI = blockUI.instances.get('myBlockUI');

    Objectos = $scope;
   
    $scope.SigIn = function()
    {
        var path = window.location.origin;       
        var username = "Fulano";
        var password = "1234";

        //grant_type:password
        //Nome:Fulano 
        //Senha:1234
        //Content-Type:application/x-www-form-urlencoded

        $.ajax({
                type: "POST",
                url: path+"/token", //aqui deve ser o endereco do backend
                data: {
                    "grant_type": "password",
                    "username": username,
                    "password": password,
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                success: function (data) {
                    token = data.access_token;
                    window.location.href = 'http://localhost:54992/Home/Index'
                },
                error: function (data) {
                    window.location.href = 'http://localhost:54992/Home/Login'
                }
            });
        

        //$("#btn_get").click(function () {
        //    $.ajax({
        //        type: "GET",
        //        url: "http://localhost:55090/api/teste", //aqui deve ser o endereco do backend
        //        headers: {
        //            "Authorization": "Bearer " + token
        //        },
        //        success: function (data) {
        //            console.log(data);
        //        },
        //        error: function (data) {
        //            console.log($.parseJSON(data.responseText).Message);
        //        }
        //    });
        //});
    }
          
    $scope.Login = function () {         
           
             var path = window.location.origin;
          
             $http.post(path + '/api/webapi/login/getAccount', $scope.LoginAccount).then(function (data)
             {
                 var retorno = data;
                 if (data.data == null)
                 {
                     $scope.LoginAccount.User = "";
                     $scope.LoginAccount.Password = "";
                     $window.alert("Usuario não Cadastrado");
                     //alert("Usuario não Cadastrado");
                    
                         $("#register-form").delay(100).fadeIn(100);
                         $("#login-form").fadeOut(100);
                         $('#login-form-link').removeClass('active');
                         $('#recover-form-link').removeClass('active');
                         $(this).addClass('active');
                         e.preventDefault();
                   
                 }
                 else
                 {
                     $window.alert("Cadastro efetuado com sucesso,Usuario e senha enviados para seu e-mail");
                     window.location.href = 'http://localhost:54992/Home/Index';
                 }
            },
            function (error) {               
                alert(error.data)
            });

     }

    $scope.Cadastro = function ()
     {        
         var path = window.location.origin;
         var Cliente = $scope.Cliente;

         $http.post(path + '/api/webapi/login/Cadastrar', $scope.Cliente)
        .then(function (request)
        {            
            if (request.data == "OK") {
                $window.alert("Cadastro efetuado com sucesso,Usuario e senha enviados para seu e-mail");
                //alert("Cadastro efetuado com sucesso,Usuario e senha enviados para seu e-mail");
                window.location.href = 'http://localhost:54992/Home/Index';
            } else {
                alert(request.data)
            }
        },
        function (error) {
            alert(error.data);
        });
    }

    $scope.LoginS = function () {

        var path = window.location.origin;

        $http.post(path + '/api/webapi/login/getAccount', $scope.LoginAccount).then(function (data) {
            var retorno = data;
            if (data.data == null) {
                $scope.LoginAccount.User = "";
                $scope.LoginAccount.Password = "";
                $window.alert("Usuario não Cadastrado");
                //alert("Usuario não Cadastrado");

                $("#register-form").delay(100).fadeIn(100);
                $("#login-form").fadeOut(100);
                $('#login-form-link').removeClass('active');
                $('#recover-form-link').removeClass('active');
                $(this).addClass('active');
                e.preventDefault();

            }
            else {
                $window.alert("Cadastro efetuado com sucesso,Usuario e senha enviados para seu e-mail");
                window.location.href = 'http://localhost:54992/Home/Index';
            }
        },
       function (error) {
           alert(error.data)
       });

    }
   
}]);