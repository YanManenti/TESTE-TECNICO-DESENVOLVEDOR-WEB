angular.module('testeApp').factory('Login',function ($location,$localStorage,$http,Toast){

    let loggedMem = false;

    return{
        login: function(user){
            try {
                $http({
                    method: 'GET',
                    url: 'http://localhost:8080/api/admin/login?usuario=' + user.usuario + '&senha=' + user.senha,
                }).then(function successCallback(response) {
                    if(user.lembrar===true)
                    {
                        $localStorage.logged = true;
                        $localStorage.lembrar = user.lembrar;
                        $location.path('/usuarios');
                    }else{
                        loggedMem = true;
                        $location.path('/usuarios');
                    }
                }).catch(function errorCallback(response) {
                    Toast.showErrorToast(response.data);
                })
            }catch (e) {
                console.log(e);
            }
        },
        logout: function(){
            $localStorage.$reset();
            loggedMem = false;
            $location.path('/home');
        },
        status: function(){
            if($localStorage.lembrar===true && $localStorage.logged===false){
                $location.path('/home');
            }else if($localStorage.lembrar===undefined && loggedMem===false){
                $location.path('/home');
            }
            return !$localStorage.logged || !loggedMem;
        }
    }

})