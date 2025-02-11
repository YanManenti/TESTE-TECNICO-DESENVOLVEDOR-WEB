angular.
module('home').
controller('HomeController',['$scope','$http','$location','Login',function($scope,$http,$location,Login){
    $scope.master = {};
    $scope.login = function(user) {
        $scope.master = angular.copy(user);
        Login.login(user);
    };
    $scope.reset = function() {
        $scope.user = angular.copy($scope.master);
    };
    $scope.reset();
}]);