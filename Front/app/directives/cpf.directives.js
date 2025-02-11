angular.
module('testeApp').directive('cpf', function(){
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
                if(transformedInput.length!==4 && transformedInput.length!==8 && transformedInput.length!==12){
                    if(!reg.test(transformedInput[transformedInput.length-1])){
                        transformedInput=transformedInput.slice(0,transformedInput.length-1);
                        modelCtrl.$setViewValue(transformedInput);
                        modelCtrl.$render();
                        return transformedInput;
                    }
                }

                if (inputValue.length===3){
                    transformedInput += '.'
                }
                if(inputValue.length===7){
                    transformedInput += '.'
                }
                if(inputValue.length===11){
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