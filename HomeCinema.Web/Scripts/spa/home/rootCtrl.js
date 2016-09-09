(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);

    function rootCtrl($scope) {
        $scope.userData = {};
        $scope.userData.displayUserInfo = displayUserInfo;
        $scope.userData.logout = logout;

        function displayUserInfo() {

        }

        function logout() {

        }
    }
})(angular.module('homeCinema'));
