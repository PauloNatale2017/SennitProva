var app = angular.module('App', ['blockUI']);
var Objectos;
var token = "";

app.controller("CtrlCliente", ['$scope', '$http', '$location', '$window','blockUI','$timeout',
function ($scope, $http, $location, $window, blockUI,$timeout)
{
    blockUI.start("...LOADING...");
    $scope.GetClientes = function ()
    {
        var path = window.location.origin;
        $http.get(path + "/api/webapi/Cliente/GetClientes").then(function (response) {
            $timeout(function(){
                blockUI.stop();
                $scope.Cliente = response.data;
            },3000);
           
        });
    }
    $scope.GetClientes();
}]);