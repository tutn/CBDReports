//var app = angular.module("app", []);
//var app = angular.module("app", ['trNgGrid', 'ngFileUpload']);
app.controller("sidebarController", sidebarController);
app.$inject = ["$scope", "cbdService"];

function sidebarController($scope, cbdService) {
    $scope.Nodes = [];

    $scope.init = function (model) {
        //$scope.getNodes();
        $scope.Nodes = angular.copy(model.Nodes);
    }

    //$scope.getNodes = function () {
    //    //ShowLoader();
    //    cbdService.GetNodes().then(function (d) {
    //        // Success
    //        //HideLoader();
    //        $scope.Nodes = d.data.Data;
    //    }, function (status) {
    //        //HideLoader();
    //        //DisplayServerErrorMessage(status.data); // Failed
    //    });
    //}
}

//app.factory('cbdService', ["$http", "baseUrl", function ($http, baseUrl) {
//    var fac = {};
//    fac.GetNodes = function () {
//        return $http.get(baseUrl + '/api/Page/GetNodes');
//    }
//    return fac;
//}]);
