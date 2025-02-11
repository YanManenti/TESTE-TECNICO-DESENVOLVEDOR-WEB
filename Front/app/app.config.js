'use strict';

angular.
module('testeApp').
config(['$routeProvider',
    function config($routeProvider) {
        $routeProvider.
        when('/usuarios', {
            template: '<usuarios></usuarios>',
            controller: 'UsuariosController'
        }).
        when('/home',{
            template: '<home></home>',
            controller: 'HomeController'
        }).
        otherwise({redirectTo: '/home'});
    }
]);