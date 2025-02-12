

angular.
module('usuarios').
controller('UsuariosController',['$scope','$location','$http','$interval','$mdDialog','Toast','Login',function($scope,$location,$http,$interval,$mdDialog,Toast,Login){
    $scope.$on('$routeChangeSuccess', function(){
        Login.status();
    })

    $scope.logout = Login.logout;

    $scope.usuarios = [];

    $scope.refresh = function() {
        $http({
            method: 'GET',
            url: 'http://localhost:8080/api/usuario/buscartodos'
        }).then(function successCallback(response) {
            $scope.usuarios=response.data;
        }
        ).catch(function errorCallback(response) {
            Toast.showErrorToast(response.data.detail);
        })
    }

    $scope.showAdvanced = function (ev) {
        $mdDialog.show({
            controller: CriarController,
            templateUrl: 'app/components/criar/criar.template.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
        }).then(function (hide) {
            $scope.refresh();
        }, function () {
            $scope.status = 'You cancelled the dialog.';
        });
    };

    const CriarController = function($scope, $mdDialog){
        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };


        $scope.cadastrar = function (user) {
            $scope.master = angular.copy(user);
            $http({
                method: 'POST',
                url: 'http://localhost:8080/api/usuario/criar',
                data: {
                    ...user
                }
            }).then(function successCallback(response) {
                console.log(response);
                    Toast.showSuccessToast(response.data);
                    $scope.hide();
                }
            ).catch(function errorCallback(response) {
                if(response.status === 400){
                    if(!response.data.title){
                        Toast.showErrorToast(response.data);
                    }
                }
                if(response.status === 500){
                    Toast.showErrorToast(response.data.detail);
                }
            })
        };
    }

    $scope.refresh();

}]);