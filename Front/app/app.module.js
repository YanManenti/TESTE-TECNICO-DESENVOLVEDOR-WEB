'use strict';

angular.module('testeApp', ['ngRoute','usuarios','home','criar','ngMaterial', 'ngMessages','ngStorage','smart-table']).config(function($mdThemingProvider) {
    $mdThemingProvider.theme('default')
        .primaryPalette('red')
        .accentPalette('red')
    $mdThemingProvider.theme("success-toast")
    $mdThemingProvider.theme("error-toast")
});