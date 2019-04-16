var express = require('express');
var app = express();
var bodyParser = require('body-parser');
var superagent = require('superagent');
var cheerio = require('cheerio');

// 创建 application/x-www-form-urlencoded 编码解析
var urlencodedParser = bodyParser.urlencoded({ extended: false })

app.use('/public', express.static('public'));

app.get('/', function(req, res) {
    res.sendFile(__dirname + "/" + "index.html");
})
app.get('/lang', function(req, res) {
    res.sendfile(__dirname + "/" + "lang.json");
})

app.post('/translate', urlencodedParser, function(req, res) {
    //通过地址读取地中中的页面文本
    var Url = 'https://translate.google.cn';
    superagent.get(Url)
        .end(function(err, response) {
            if (err) {
                return console.error(err);
            }
            var topicUrls = [];
            var $ = cheerio.load(response.text);
            console.log(response.body);
        })
        // 输出 JSON 格式
    var response = {
        "fr": req.body.fr,
        "lang": req.body.lang,
        "tolang": req.body.tolang
    };
    console.log(response);
    res.end(JSON.stringify(response));
})

var server = app.listen(8888, function() {

    var host = server.address().address
    var port = server.address().port

    console.log("应用实例，访问地址为 http://%s:%s", host, port)

})