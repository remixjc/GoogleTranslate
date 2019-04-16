var express = require('express');
var app = express();

app.get('/', function(req, res) {
    res.send('Hello Express!');
})

var server = app.listen(8888, function() {
    var host = server.address().address;
    var port = server.address().port;

    console.log('应用实例的地址为http://%s:%s', host, port);
})