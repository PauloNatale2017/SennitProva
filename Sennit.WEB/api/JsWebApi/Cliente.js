var app = angular.module('App', ['blockUI']);
var Objectos;
var token = "";

app.controller("CtrlCliente", ['$scope', '$http', '$location', '$window','blockUI','$timeout',
function ($scope, $http, $location, $window, blockUI,$timeout)
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

    $scope.GetClientes = function ()
    {
        var path = window.location.origin;
        var config = {
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
                "Authorization": "bearer " + token
            }
        };
        $http.get(path + "/api/webapi/Cliente/GetClientes", config).then(function (response) {
            $timeout(function(){
                blockUI.stop();
                $scope.Cliente = response.data;
            },3000);
           
        });
    }
    $scope.GetClientes();
}]);