var System = require('systemjs');

System.import('../common_modules/calc.js').then(function(calc) {
    console.log(calc.add(4,5));
});