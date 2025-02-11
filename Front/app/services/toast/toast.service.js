angular.module('testeApp').factory('Toast',function ($mdToast){

return{
    showSuccessToast: function (message) {
        $mdToast.show(
            $mdToast.simple().textContent(message).theme("success-toast")
                .position('bottom right')
                .hideDelay(3000)
        )},
    showErrorToast: function (message) {
        $mdToast.show(
            $mdToast.simple().textContent(message).theme("error-toast")
                .position('bottom right')
                .hideDelay(3000)
        )},
    }
})