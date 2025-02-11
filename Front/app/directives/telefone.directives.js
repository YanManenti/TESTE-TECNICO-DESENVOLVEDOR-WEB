// angular.
// module('testeApp').
// filter('telefone', function() {
//     return function(input) {
//         return '(' + input.slice(0, 2) + ') ' + input.slice(2, 5) + '-' + input.slice(7, 4);
//     };
// });

angular.
module('testeApp').directive('telefone', function(){
    return {
        require: 'ngModel',
        link: function(scope, element, attrs, modelCtrl) {

            var reg = /^[0-9]*$/;

            modelCtrl.$parsers.push(function (inputValue) {

                var transformedInput = inputValue;

                if(transformedInput.length > 14){
                    transformedInput=transformedInput.slice(0,transformedInput.length-1);
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                    return transformedInput;
                }

                if(transformedInput.length!==2 && transformedInput.length!==4 && transformedInput.length!==10){
                    if(!reg.test(transformedInput[transformedInput.length-1])){
                        transformedInput=transformedInput.slice(0,transformedInput.length-1);
                        modelCtrl.$setViewValue(transformedInput);
                        modelCtrl.$render();
                        return transformedInput;
                    }
                }

                if (inputValue.length===1){
                    transformedInput = '('+transformedInput;
                }
                if(inputValue.length===3){
                    transformedInput += ')'
                }
                if(inputValue.length===9){
                    transformedInput += '-'
                }

                if (transformedInput!==inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });
        }
    };
});